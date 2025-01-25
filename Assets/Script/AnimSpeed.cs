using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpeed : MonoBehaviour
{
    public Animator anim;
    public float speed;

    void Start()
    {
        anim.speed = speed;
    }
}
