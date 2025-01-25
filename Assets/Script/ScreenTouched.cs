using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTouched : MonoBehaviour
{
    public Animator anim;
    
    void Update()
    {
        if(Input.touchCount > 0)
            anim.SetBool("Clicked", true);
        else
            anim.SetBool("Clicked", false);
    }
}
