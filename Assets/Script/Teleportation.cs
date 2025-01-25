using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using RDG;

public class Teleportation : MonoBehaviour
{
    public GameObject PortalO;
    public GameObject PortalB;
    public GameObject Ball;
    public GameObject BBall;
    public GameObject OBall;
    public GameObject Spawn;
    
    public Rigidbody2D rb;
    public Transform BPos;
    public Transform OPos;
    public PhysicsMaterial2D ball;
    public PhysicsMaterial2D bounce;

    public float time;
    bool isCollidedPortal = false;
    bool isCollidedPortalB = false;
    bool isCollidedPortalO = false;

    void Start()
    {
        Physics2D.queriesHitTriggers = true;
        Physics2D.IgnoreLayerCollision(6, 6);
        Physics2D.IgnoreLayerCollision(3, 6);

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (time > 0) time--;

        if (GameObject.FindGameObjectWithTag("PortalO"))
        {
            PortalO = GameObject.FindGameObjectWithTag("PortalO");
            OPos = GameObject.FindGameObjectWithTag("PortalO").transform;
        }

        if (GameObject.FindGameObjectWithTag("PortalB"))
        {
            PortalB = GameObject.FindGameObjectWithTag("PortalB");
            BPos = GameObject.FindGameObjectWithTag("PortalB").transform;
        }

        if (rb.velocity.x >= 30)
            rb.velocity = new Vector2(30, rb.velocity.y);

        if (rb.velocity.y >= 30)
            rb.velocity = new Vector2(rb.velocity.x, 30);

        if (rb.velocity.x <= -30)
            rb.velocity = new Vector2(-30, rb.velocity.y);

        if (rb.velocity.y <= -30)
            rb.velocity = new Vector2(rb.velocity.x, -30);

        if (isCollidedPortal)
            rb.sharedMaterial = bounce;
        else
            rb.sharedMaterial = ball;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Ball") && !collision.gameObject.GetComponent<Collider2D>().CompareTag("PortalO") && !collision.gameObject.GetComponent<Collider2D>().CompareTag("PortalB"))
            if (!GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().IsPlaying("Collision"))
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("Collision");


        if (PlayerPrefs.GetInt("Vibrate", 1) == 1)
            Vibration.Vibrate(100, 100);

        if (collision.gameObject.GetComponent<Collider2D>().CompareTag("PortalBounce"))
            return;

        if (collision.gameObject.GetComponent<Collider2D>().CompareTag("PortalO") || collision.gameObject.GetComponent<Collider2D>().CompareTag("PortalB"))
            isCollidedPortal = true;

        if (gameObject.CompareTag("BBall") && !isCollidedPortal)
        {
            if (!GameObject.FindGameObjectWithTag("PortalB"))
                PortalB = Instantiate(PortalB, collision.contacts[0].point, collision.transform.localRotation);

            PortalB.transform.position = collision.contacts[0].point;
            PortalB.transform.rotation = collision.transform.localRotation;

            BPos = PortalB.transform;

            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("PortalBPlace");
            Destroy(gameObject);
        }

        if (gameObject.CompareTag("OBall") && !isCollidedPortal)
        {
            if (!GameObject.FindGameObjectWithTag("PortalO"))
                PortalO = Instantiate(PortalO, collision.contacts[0].point, collision.transform.localRotation);

            PortalO.transform.position = collision.contacts[0].point;
            PortalO.transform.rotation = collision.transform.localRotation;
  
            OPos = PortalO.transform;
 
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("PortalOPlace");
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PortalO") || collision.CompareTag("PortalB"))
            isCollidedPortal = false;

        if (collision.CompareTag("PortalO"))
            isCollidedPortalO = false;

        if (collision.CompareTag("PortalB"))
            isCollidedPortalB = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (GameObject.FindGameObjectWithTag("PortalO") && GameObject.FindGameObjectWithTag("PortalB") && !gameObject.CompareTag("BBall") && !gameObject.CompareTag("OBall"))
        {
            if (collision.CompareTag("PortalB") && !isCollidedPortalB && time <= 0)
            {
                transform.position = OPos.transform.position;

                Vector3 enterVelocity = PortalB.transform.InverseTransformDirection(rb.velocity);
                Vector3 exitVelocity = PortalO.transform.TransformDirection(enterVelocity);
                Debug.Log(exitVelocity);
                Debug.DrawRay(new Vector3(PortalO.transform.position.x + 0.06f, PortalO.transform.position.y + 0.03f, 0), exitVelocity, Color.white, 1);
                Debug.DrawRay(new Vector3(PortalO.transform.position.x - 0.06f, PortalO.transform.position.y - 0.03f, 0), exitVelocity, Color.white, 1);
  
                rb.velocity = exitVelocity;
                time = 15;

                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("PortalEnter");
                isCollidedPortalO = true;
            }

            if (collision.CompareTag("PortalO") && !isCollidedPortalO && time <= 0)
            { 
                TrailRenderer tr = GetComponent<TrailRenderer>();
 
                transform.position = BPos.transform.position;

                    
                Vector3 enterVelocity = PortalO.transform.InverseTransformDirection(rb.velocity);   
                Vector3 exitVelocity = PortalB.transform.TransformDirection(enterVelocity);
                
                rb.velocity = exitVelocity;
                time = 15;

                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("PortalEnter");
                isCollidedPortalB = true;
            }
        }

        if (collision.CompareTag("PortalO") || collision.CompareTag("PortalB"))
            isCollidedPortal = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnTriggerEnter2D(collision);
    }
}
