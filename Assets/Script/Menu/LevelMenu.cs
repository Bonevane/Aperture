using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButtons;
    public Color starColor;

    private void Awake()
    {
        ButtonsToArray();
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if(unlockedLevel > buttons.Length)
            unlockedLevel = buttons.Length;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;

            for(int j = 1; j < buttons[i].gameObject.transform.childCount ; j++)
            {
                if(PlayerPrefs.GetInt("Stars" + (i+1).ToString(), 0) >= j)
                    buttons[i].gameObject.transform.GetChild(j).GetComponent<Image>().color = starColor;
            }
        }
    }

    public void OpenLevel(int levelID)
    {
        string level = "Level " + levelID;
        SceneManager.LoadScene(level);
    }

    void ButtonsToArray()
    {
        int childCount = levelButtons.transform.childCount;
        buttons = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).GetComponent<Button>();
        }

    }
}
