using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    int currentLevel;
    public GameObject winScreen;
    public Animator transition;
    public float transitionTime = 1.0f;
    private const int LEVELS = 30;
    

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (GameObject.FindGameObjectWithTag("SwipeAnim"))
        {
            transition = GameObject.FindGameObjectWithTag("SwipeAnim").GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = 60;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            UnlockNewLevel();
            winScreen.SetActive(true);
            Destroy(collision.gameObject);
        }
    }

    void UnlockNewLevel()
    {
        //PlayerPrefs.SetInt("UnlockedLevel", SceneManager.GetActiveScene().buildIndex);
        //Debug.Log(PlayerPrefs.GetInt("ReachedIndex"));
        //Debug.Log(PlayerPrefs.GetInt("UnlockedLevel"));

        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex") && SceneManager.GetActiveScene().buildIndex < LEVELS)
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }

        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("Finish");
    }

    public void playGame()
    {
        if (PlayerPrefs.GetInt("UnlockedLevel") >= LEVELS)
            StartCoroutine(LoadNextLevel(30));
        else
            StartCoroutine(LoadNextLevel(PlayerPrefs.GetInt("UnlockedLevel", 1)));
    }

    public void OpenLevel(int levelID)
    {
         StartCoroutine(LoadNextLevel(levelID));
    }

    public void NextLevel()
    {
        StartCoroutine(LoadNextLevel(currentLevel + 1));
    }

    IEnumerator LoadNextLevel(int currentLevel)
    {
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("Level " + (currentLevel));
    }

}
