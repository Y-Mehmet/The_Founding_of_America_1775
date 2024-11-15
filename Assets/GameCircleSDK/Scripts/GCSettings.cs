using System;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace GameCircleSDK
{
    [Serializable]
    [CreateAssetMenu(fileName = "GCSettings", menuName = "ScriptableObjects/GCSettings")]
    public class GCSettings : ScriptableObject
    {
        public string fileId;
        public bool usingFacebook;
        public bool usingGameAnalytics;
        public bool usingAdjust;
        public string adMediator;
        public bool usingAdMobAndroid;
        public bool usingAdMobIOS;
        public bool usingAdMostAndroid;
        public bool usingAdMostIOS;
        public bool usingFirebase;
        public bool usingFirebaseAndroid;
        public bool usingFirebaseIOS;

        public string FACEBOOK_AppID;
        public string FACEBOOK_ClientToken;
        public string FACEBOOK_AndroidKeystorePath; //Fetch automatically if possible

        public string GAMEANALYTICS_GameKey;
        public string GAMEANALYTICS_SecretKey;
        
        public string ADJUST_AppToken;
        public string ADJUST_AppSecret;
    
        public string ADMOB_AndroidAppID;
        public string ADMOB_AndroidIntersititialID;
        public string ADMOB_AndroidRewardedID;
        public string ADMOB_AndroidBannerID;
        public string ADMOB_IOSAppID;
        public string ADMOB_IOSIntersititialID;
        public string ADMOB_IOSRewardedID;
        public string ADMOB_IOSBannerID;
    
        public string ADMOST_AndroidAppID;
        public string ADMOST_AndroidIntersititialID;
        public string ADMOST_AndroidRewardedID;
        public string ADMOST_AndroidBannerID;
        public string ADMOST_IOSAppID;
        public string ADMOST_IOSIntersititialID;
        public string ADMOST_IOSRewardedID;
        public string ADMOST_IOSBannerID;
        
        public bool UsingAdMob() => adMediator == "AdMob";
        public bool UsingAdMost() => adMediator == "AdMost";
        
        public Texture2D gameCircleIcon;

        public void OverrideWithJson(string json)
        {
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }
}