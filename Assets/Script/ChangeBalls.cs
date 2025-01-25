using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeBalls : MonoBehaviour
{
    public GameObject OBall;
    public GameObject BBall;
    public GameObject Ball;
    private bool OBallExists;
    private bool BBallExists;
    private bool BallExists;
    private bool AllExist;
    private bool AllExistPrev;
    private bool playerLeft;
    private bool tryAgain;
    
    private string currentBall;
    private bool ballInSpawn;

    [Tooltip("Starting Ball:   0 -> Ball, 1 -> BBall, 2 -> OBall")]
    [Range(0, 2)] public int startingBall;

    private void Awake()
    {
        switch (startingBall)
        {
            case 0:
                Instantiate(Ball, transform);
                ballInSpawn = true;
                currentBall = "Ball";
                break;
            case 1:
                Instantiate(BBall, transform);
                ballInSpawn = true;
                currentBall = "BBall";
                break;
            case 2:
                Instantiate(OBall, transform);
                ballInSpawn = true;
                currentBall = "OBall";
                break;
        }
    }

    private void FixedUpdate()
    {
        if(!ballInSpawn)
            changeBalls();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(currentBall) && !collision.GetComponent<SpriteRenderer>().enabled)
        {
            ballInSpawn = false;

            if (currentBall == "Ball")
                playerLeft = true;

            changeBalls();
        }

        collision.GetComponent<SpriteRenderer>().enabled = true;

        
    }

    public void changeBalls()
    {

        switch (startingBall)
        {
            case 0:
                ballInSpawn = true;
                break;
            case 1:
                if (playerLeft)
                {
                    Instantiate(BBall, transform);
                    currentBall = "BBall";
                    ballInSpawn = true;
                }
                else
                {
                    Instantiate(Ball, transform);
                    currentBall = "Ball";
                    ballInSpawn = true;
                }
                break;
            case 2:
                if (playerLeft)
                {
                    if (currentBall == "OBall")
                    {
                        Instantiate(BBall, transform);
                        currentBall = "BBall";
                        ballInSpawn = true;
                    }
                    else
                    {
                        Instantiate(OBall, transform);
                        currentBall = "OBall";
                        ballInSpawn = true;
                    }
                }
                else
                {
                    if (currentBall == "OBall")
                    {
                        Instantiate(BBall, transform);
                        currentBall = "BBall";
                        ballInSpawn = true;
                    }
                    else if (currentBall == "BBall")
                    {
                        Instantiate(Ball, transform);
                        currentBall = "Ball";
                        ballInSpawn = true;
                    }
                    else
                    {
                        Instantiate(OBall, transform);
                        currentBall = "OBall";
                        ballInSpawn = true;
                    }
                }
                break;
        }
    }
}