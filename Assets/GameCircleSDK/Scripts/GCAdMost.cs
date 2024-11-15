using System;
using AMR;

namespace GameCircleSDK
{
    public class GCAdMost : GCAdMediator
    {
        private Action _rewardedCompletedCallback;
        private bool _registeredInterstitial;
        private bool _registeredRewarded;
        private const string AD_PLATFORM = "Admost";
    
        public override void Initialize()
        {
            var config = new AMRSdkConfig
            {
#if UNITY_ANDROID
                ApplicationIdAndroid = GameCircle.Settings.ADMOST_AndroidAppID,
                InterstitialIdAndroid = GameCircle.Settings.ADMOST_AndroidIntersititialID,
                RewardedVideoIdAndroid = GameCircle.Settings.ADMOST_AndroidRewardedID,
                BannerIdAndroid = GameCircle.Settings.ADMOST_AndroidBannerID,
#else
            ApplicationIdIOS = GameCircle.Settings.ADMOST_IOSAppID,
            InterstitialIdIOS = GameCircle.Settings.ADMOST_IOSIntersititialID,
            RewardedVideoIdIOS = GameCircle.Settings.ADMOST_IOSRewardedID,
            BannerIdIOS = GameCircle.Settings.ADMOST_IOSBannerID
#endif
            };

            AMRSDK.startWithConfig(config, OnSDKDidInitialize);
        
            AMRSDK.setOnRewardedVideoDismiss(OnVideoDismiss);
            AMRSDK.setOnRewardedVideoComplete(OnVideoComplete);
            AMRSDK.setOnInterstitialDismiss(OnInterstitialDismiss);

            if (GameCircle.Settings.usingFirebase)
            {
                AMRSDK.setOnInterstitialImpression(OnInterstitialImpression);
                AMRSDK.setOnRewardedVideoImpression(OnRewardedVideoImpression);
                AMRSDK.setOnBannerImpression(OnBannerImpression);
            }
        }

        private void OnSDKDidInitialize(bool success, String error)
        {
            LoadInterstitial();
            LoadRewarded();
            LoadBanner();
        }

        private void OnInterstitialDismiss()
        {
            AMRSDK.loadInterstitial();
        }

        private void OnVideoDismiss()
        {
            AMRSDK.loadRewardedVideo();
        }

        private void OnVideoComplete()
        {
            _rewardedCompletedCallback?.Invoke();
        }

        private void LoadInterstitial()
        {
            AMRSDK.loadInterstitial();
        }

        private void LoadRewarded()
        {
            AMRSDK.loadRewardedVideo();
        }

        private void LoadBanner()
        {
            AMRSDK.loadBanner(Enums.AMRSDKBannerPosition.BannerPositionBottom, false);
        }

        public override void ShowInterstitialAd()
        {
            if (AMRSDK.isInterstitialReady())
                AMRSDK.showInterstitial();
        }

        public override void ShowRewardedAd(Action action)
        {
            _rewardedCompletedCallback = action;
        
            if (AMRSDK.isRewardedVideoReady())
                AMRSDK.showRewardedVideo();
        }

        public override void ShowBannerAd()
        {
            AMRSDK.showBanner();
        }

        private void OnBannerImpression(AMRAd ad)
        {
            if (ad == null) return;
        
            Firebase.Analytics.Parameter[] adParameters = {
                new ("ad_platform", AD_PLATFORM),
                new ("ad_source", ad.Network),
                new ("ad_unit_name", ad.AdSpaceId),
                new ("ad_format", "Banner"),
                new ("currency",ad.Currency),
                new ("value", ad.Revenue)
            };
            Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression", adParameters);
        }
    
        private void OnRewardedVideoImpression(AMRAd ad)
        {
            if (ad == null) return;
        
            Firebase.Analytics.Parameter[] adParameters = {
                new ("ad_platform", AD_PLATFORM),
                new ("ad_source", ad.Network),
                new ("ad_unit_name", ad.AdSpaceId),
                new ("ad_format", "Rewarded"),
                new ("currency",ad.Currency),
                new ("value", ad.Revenue)
            };
            Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression", adParameters);
        }
    
        private void OnInterstitialImpression(AMRAd ad)
        {
            if (ad == null) return;
        
            Firebase.Analytics.Parameter[] adParameters = {
                new ("ad_platform", AD_PLATFORM),
                new ("ad_source", ad.Network),
                new ("ad_unit_name", ad.AdSpaceId),
                new ("ad_format", "Interstitial"),
                new ("currency",ad.Currency),
                new ("value", ad.Revenue)
            };
            Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression", adParameters);
        }
    }
}
