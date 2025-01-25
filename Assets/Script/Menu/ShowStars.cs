using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowStars : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI congrats;
    [SerializeField] GameObject gold;
    [SerializeField] GameObject silver;
    [SerializeField] GameObject bronze;
    private float finalTime;

    void Start()
    {
        finalTime = Timer.elapsedTime;
        int acquiredStars = 1;

        if (finalTime < Timer.goldTimes[SceneManager.GetActiveScene().buildIndex - 1])
        {
            acquiredStars = 3;
            congrats.text = "Great Job!";
        }
        else if (finalTime < Timer.goldTimes[SceneManager.GetActiveScene().buildIndex - 1] * 2f)
        {
            acquiredStars = 2;
            congrats.text = "Almost there! You can do it!";
        }

        if (acquiredStars >= 3)
        {
            StartCoroutine(Wait(1.5f, gold));
        }

        if (acquiredStars >= 2)
        {
            StartCoroutine(Wait(1f, silver));

        }

        if (acquiredStars >= 1)
        {
            StartCoroutine(Wait(0.5f, bronze));
        }


        if (acquiredStars > PlayerPrefs.GetInt("Stars" + SceneManager.GetActiveScene().buildIndex.ToString(), 0))
            PlayerPrefs.SetInt("Stars" + SceneManager.GetActiveScene().buildIndex.ToString(), acquiredStars);
    }

    
    IEnumerator Wait(float sec, GameObject obj)
    {
        yield return new WaitForSecondsRealtime(sec);

        obj.SetActive(true);

        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play(obj.name);
    }
}
