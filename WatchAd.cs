using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class WatchAd : MonoBehaviour{
    void Awake(){
        #if UNITY_IPHONE
            Advertisement.Initialize ("4263020", false);
        #endif
        #if UNITY_ANDROID
            Advertisement.Initialize ("4263021", false);
        #endif
    }

    public void ShowAd(){
        
        if (Advertisement.IsReady() && PlayerPrefs.GetInt("PlayAds") == 0) {
            Debug.Log("Play Ad");
            Advertisement.Show();
        }      
    } 
}