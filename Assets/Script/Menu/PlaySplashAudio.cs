using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.SceneManagement;

public class PlaySplashAudio : MonoBehaviour
{
    [SerializeField] GameObject transition; 

    public void PlayShutterSoft(string name)
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play(name);
    }

    public void PlayShutterHard(string name)
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play(name);
    }

    public void SwitchToMenu()
    {
        StartCoroutine(LoadNextLevel("Menu"));
    }

    IEnumerator LoadNextLevel(string name)
    {
        transition.GetComponent<Animator>().SetBool("SplashEnd", true);

        yield return new WaitForSeconds(0.7f);

        SceneManager.LoadScene(name);
    }
}
