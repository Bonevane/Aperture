using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySwoosh : MonoBehaviour
{
    public void PlaySwooshSound(string name)
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play(name);
    }
}
