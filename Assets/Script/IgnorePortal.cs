using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePortal : MonoBehaviour
{
    void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 7);
    }

}
