using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRug : MonoBehaviour
{
    public PhysicsMaterial2D Ball;
    public PhysicsMaterial2D Slime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PhysicsMaterial2D>())
        collision.GetComponent<PhysicMaterial>().bounciness = Slime.bounciness; 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PhysicsMaterial2D>())
            collision.GetComponent<PhysicMaterial>().bounciness = Ball.bounciness;
    }
    public void setBounceHigh()
    {
        Ball.bounciness = 1;
    }
    public void setBounceLow()
    {
        Ball.bounciness = 0.2f;
    }
}
