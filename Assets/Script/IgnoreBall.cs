using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreBall : MonoBehaviour
{
    void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 8);
    }

}
