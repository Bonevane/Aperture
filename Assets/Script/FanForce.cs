using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanForce : MonoBehaviour
{
    public float forceX;
    public float forceY;
    Vector2 force;

    // Start is called before the first frame update
    void Start()
    {
        force = new Vector2(forceX, forceY);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") || collision.CompareTag("OBall") || collision.CompareTag("BBall"))
        {
            collision.attachedRigidbody.AddForce(force, ForceMode2D.Force);

        }
    }
}
