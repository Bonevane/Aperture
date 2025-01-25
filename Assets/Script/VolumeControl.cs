using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    private void Awake()
    {
        if(CompareTag("MusicSlider"))
            GetComponent<Slider>().value = PlayerPrefs.GetFloat("MasterVolume", 0.7f);
        else
            GetComponent<Slider>().value = PlayerPrefs.GetFloat("MasterVolumeSFX", 1);
    }

    public void UpdateVolume()
    {
        if (CompareTag("MusicSlider")){
            GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<AudioManager>().UpdateCurrentPlayingVolume();
            PlayerPrefs.SetFloat("MasterVolume", GetComponent<Slider>().value);
        }
        else
            PlayerPrefs.SetFloat("MasterVolumeSFX", GetComponent<Slider>().value);
    }
}
