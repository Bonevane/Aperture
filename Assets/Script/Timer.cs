using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject musicManager;
    public static float elapsedTime;
    public static int[] goldTimes;
    private const int LEVELS = 30;

    void Start()
    {
        if(GameObject.FindGameObjectsWithTag("MusicPlayer").Length == 0)
            Instantiate(musicManager);

        elapsedTime = 0;
        goldTimes = new int[LEVELS] { 30, 30, 10, 15, 15, 10, 10, 10, 10, 10, 12, 15, 15, 18, 18, 20, 15, 18, 15, 10, 15, 15, 15, 15, 12, 15, 15, 15, 18, 18};
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
    }
}
