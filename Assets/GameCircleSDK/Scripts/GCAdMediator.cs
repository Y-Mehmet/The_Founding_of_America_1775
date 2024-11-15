using System;

namespace GameCircleSDK
{
    public class GCAdMediator
    {
        public virtual void Initialize() {}

        public virtual void ShowInterstitialAd() {}

        public virtual void ShowRewardedAd(Action action) {}

        public virtual void ShowBannerAd() {}
    }
}
