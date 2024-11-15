using System;
using System.IO;
using com.adjust.sdk;
using Facebook.Unity;
using GameAnalyticsSDK;
using UnityEditor;
using UnityEngine;

namespace GameCircleSDK
{
    public static class GameCircle
    {
        private static GCSettings _settings;
        
        public static GCSettings Settings
        {
            get
            {
                if(_settings == null)
                {
                    FetchSettings();
                }
                return _settings;
            }
            set => _settings = value;
        }
    
        private static GCAdMediator _adMediator;
	
        private static void FetchSettings()
        {
            try
            {
                _settings = (GCSettings)Resources.Load("GameCircle/Settings", typeof(GCSettings));

#if UNITY_EDITOR
                if(_settings == null)
                {
                    if(!Directory.Exists(Application.dataPath + "/Resources"))
                    {
                        Directory.CreateDirectory(Application.dataPath + "/Resources");
                    }
                    if(!Directory.Exists(Application.dataPath + "/Resources/GameCircle"))
                    {
                        Directory.CreateDirectory(Application.dataPath + "/Resources/GameCircle");
                        Debug.LogWarning("GameCircle: Resources/GameCircle folder is required to store settings. it was created ");
                    }

                    const string path = "Assets/Resources/GameCircle/Settings.asset";

                    if(File.Exists(path))
                    {
                        AssetDatabase.DeleteAsset(path);
                        AssetDatabase.Refresh();
                    }

                    var asset = ScriptableObject.CreateInstance<GCSettings>();
                    AssetDatabase.CreateAsset(asset, path);
                    AssetDatabase.Refresh();

                    AssetDatabase.SaveAssets();
                    Debug.LogWarning("GameCircle: Settings file didn't exist and was created");
                    Selection.activeObject = asset;

                    _settings = asset;
                }
#endif
            }
            catch(Exception e)
            {
                Debug.Log("Error getting Settings: " + e.Message);
            }
        }

        public static void Initialize()
        {
            if(Settings.usingFacebook)
                FB.Init();
            if(Settings.usingGameAnalytics)
                GameAnalytics.Initialize();

            if (Settings.UsingAdMob())
            {
                Debug.Log("GC: Initializing AdMob.");
                _adMediator = new GCAdMob();
            }
            else if (Settings.UsingAdMost())
            {
                Debug.Log("GC: Initializing AdMost.");
                _adMediator = new GCAdMost();
            }
            else
            {
                Debug.Log("GC: Initializing without an ad mediator.");
            }
            _adMediator?.Initialize();
        }

        public static void ShowInterstitialAd()
        {
            _adMediator?.ShowInterstitialAd();
        }

        public static void ShowRewardedAd(Action action)
        {
            _adMediator?.ShowRewardedAd(action);
        }

        public static void ShowBannerAd()
        {
            _adMediator?.ShowBannerAd();
        }
    }
}
