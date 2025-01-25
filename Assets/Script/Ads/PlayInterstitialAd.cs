using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayInterstitialAd : MonoBehaviour
{
    private AdsManager adsManager;

    private void Awake()
    {
        adsManager = GameObject.FindGameObjectWithTag("Ad Manager").GetComponent<AdsManager>();
    }

    public void ShowInterstitialAd()
    {
        int num = UnityEngine.Random.Range(0, 100);
        Debug.Log(num);
        if (num > 30)
        {
            adsManager = GameObject.FindGameObjectWithTag("Ad Manager").GetComponent<AdsManager>();
            adsManager.interstitialAds.ShowInterstitialAd();
        }
    }
}
