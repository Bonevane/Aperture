using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelTransition : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.0f;
    
    void ClosePanel()
    {
        StartCoroutine(ClosePanelRoutine());
    }

    IEnumerator ClosePanelRoutine()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
    }
}
