using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBG : MonoBehaviour
{
    [SerializeField] GameObject bg1;
    [SerializeField] GameObject bg2;
    [SerializeField] GameObject bg3;

    void Start()
    {
        if(PlayerPrefs.GetInt("UnlockedLevel") >= 12)
        {
            bg1.SetActive(true); bg2.SetActive(false); bg3.SetActive(false);
        }

        if(PlayerPrefs.GetInt("UnlockedLevel") >= 22)
        {
            bg1.SetActive(false); bg2.SetActive(true); bg3.SetActive(false);
        }
    }
}
