using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string androidGameID;
    [SerializeField] private string iosGameID;
    [SerializeField] private bool isTesting;


    private string gameID;

    public void OnInitializationComplete()
    {
        Debug.Log("Ad Initialized!");

    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Ad NOT Initialized!");

    }

    private void Awake()
    {
        gameID = "5715499";

        #if UNITY_IOS
                    gameID = iosGameID;
        #elif UNITY_ANDROID
               gameID = androidGameID;
        #endif

        Advertisement.Initialize(gameID, isTesting, this);


        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameID, isTesting, this);
        }
    }
}
