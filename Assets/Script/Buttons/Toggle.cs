using RDG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour
{
    public Animator animator;

    private void Update()
    {
        if (PlayerPrefs.GetInt("Vibrate", 1) == 1)
            animator.SetBool("isOn", true);
        else
            animator.SetBool("isOn", false);
    }

    public void UpdateToggle()
    {
        animator.SetBool("isOn", !animator.GetBool("isOn"));

        if (animator.GetBool("isOn")){
            PlayerPrefs.SetInt("Vibrate", 1);
            Handheld.Vibrate();
        }
        else
            PlayerPrefs.SetInt("Vibrate", 0);

        Debug.Log(PlayerPrefs.GetInt("Vibrate"));
    }
}
