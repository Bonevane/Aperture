using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public Animator transition;
    public Animator transition2;
    public float transitionTime = 1.0f;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("SwipeAnim"))
        {
            transition = GameObject.FindGameObjectWithTag("SwipeAnim").GetComponent<Animator>();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        //StartCoroutine(OpenPanelRoutine());
        Time.timeScale = 0;
    }

    public void Home()
    {
        StartCoroutine(LoadNextLevel("Menu"));
        Time.timeScale = 1;
    }

    public void Options()
    {

    }

    public void Resume()
    {
        StartCoroutine(ClosePanelRoutine());
        Time.timeScale = 1;
    }

    public void Restart()
    {
        StartCoroutine(LoadNextLevel("Level " + (SceneManager.GetActiveScene().buildIndex)));
        Time.timeScale = 1;
    }


    IEnumerator LoadNextLevel(string name)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") || collision.CompareTag("BBall") || collision.CompareTag("OBall"))
        {
            Restart();
        }
    }

    public void ClosePanel()
    {
        StartCoroutine(ClosePanelRoutine());
    }

    IEnumerator ClosePanelRoutine()
    {
        transition2.SetTrigger("End");

        yield return new WaitForSecondsRealtime(transitionTime);
    
        pauseMenu.SetActive(false);

    }

}
