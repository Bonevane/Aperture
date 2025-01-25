using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidUnitID;
    [SerializeField] private string iosUnitID;
    private string adUnitID;

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Ad Loaded!");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Ad NOT Loaded!");

    }

    private void Awake()
    {
        adUnitID = "Interstitial_Android";

        #if UNITY_IOS
                adUnitID = iosUnitID;
        #elif UNITY_ANDROID
                adUnitID = androidUnitID;
        #endif
    }

    private void Start()
    {
        
    }

    public void LoadInterstitialAd()
    {
        Advertisement.Load(adUnitID, this);
    }

    public void ShowInterstitialAd()
    {
        Advertisement.Show(adUnitID, this);
        LoadInterstitialAd();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Ad NOT Shown!");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Time.timeScale = 0;
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Ad Clicked!");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Time.timeScale = 1;
    }
}
