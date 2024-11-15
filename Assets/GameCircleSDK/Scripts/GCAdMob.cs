using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace GameCircleSDK
{
    public class GCAdMob : GCAdMediator
    {
        private string _interstitialId;
        private string _rewardedId;
        private string _bannerId;

        private InterstitialAd _interstitialAd;
        private RewardedAd _rewardedAd;
        private BannerView _bannerView;

        private bool _registeredInterstitial;
        private bool _registeredRewarded;

        public override void Initialize()
        {
            FetchIds();
            MobileAds.Initialize(initStatus => { });
            LoadInterstitialAd();
            LoadRewardedAd();
        }

        private void FetchIds()
        {
#if UNITY_ANDROID
            if (GameCircle.Settings.usingAdMobAndroid)
            {
                _interstitialId = GameCircle.Settings.ADMOB_AndroidIntersititialID;
                _rewardedId = GameCircle.Settings.ADMOB_AndroidRewardedID;
                _bannerId = GameCircle.Settings.ADMOB_AndroidBannerID;
            }
#else
        if (GameCircle.Settings.usingAdMobIOS)
        {
            _interstitialId = GameCircle.Settings.ADMOB_IOSIntersititialID;
            _rewardedId = GameCircle.Settings.ADMOB_IOSRewardedID;
            _bannerId = GameCircle.Settings.ADMOB_IOSBannerID;
        }
#endif
        }

        private void LoadInterstitialAd()
        {
            if (_interstitialAd != null)
            {
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }

            var adRequest = new AdRequest();
            InterstitialAd.Load(_interstitialId, adRequest, (ad, error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " + "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : " + ad.GetResponseInfo());

                _interstitialAd = ad;
                RegisterReloadHandler(_interstitialAd);
            });
        }

        private void LoadRewardedAd()
        {
            if (_rewardedAd != null)
            {
                _rewardedAd.Destroy();
                _rewardedAd = null;
            }

            var adRequest = new AdRequest();
            RewardedAd.Load(_rewardedId, adRequest, (ad, error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " + "with error : " + error);
                    return;
                }
                Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());

                _rewardedAd = ad;
                RegisterReloadHandler(_rewardedAd);
            });
        }
    
        private void RegisterReloadHandler(InterstitialAd ad)
        {
            if (_registeredInterstitial) return;
            ad.OnAdFullScreenContentClosed += LoadInterstitialAd;

            ad.OnAdFullScreenContentFailed += error =>
            {
                Debug.LogError("Interstitial ad failed to open full screen content " + "with error : " + error);
                LoadInterstitialAd();
            };
        }

        private void RegisterReloadHandler(RewardedAd ad)
        {
            if (_registeredRewarded) return;
            ad.OnAdFullScreenContentClosed += LoadRewardedAd;

            ad.OnAdFullScreenContentFailed += error =>
            {
                Debug.LogError("Rewarded ad failed to open full screen content " + "with error : " + error);
                LoadRewardedAd();
            };
        }
    
        public override void ShowInterstitialAd()
        {
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
                _interstitialAd.Show();
            else
                Debug.LogError("Interstitial ad is not ready yet.");
        }
    
        public override void ShowRewardedAd(Action action)
        {
            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                _rewardedAd.Show(reward =>
                {
                    action?.Invoke();
                });
            }
        }
        
        public override void ShowBannerAd()
        {
            _bannerView ??= new BannerView(_bannerId, AdSize.Banner, AdPosition.Bottom);
        
            var adRequest = new AdRequest();
            adRequest.Keywords.Add("unity-admob-sample");
        
            _bannerView.LoadAd(adRequest);
        }
    }
}