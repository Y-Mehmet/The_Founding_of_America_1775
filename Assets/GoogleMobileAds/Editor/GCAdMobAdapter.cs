namespace GoogleMobileAds.Editor
{
    public static class GCAdMobAdapter
    {
        public static void SetAndroidAppID(string androidAppID)
        {
            var instance = GoogleMobileAdsSettings.LoadInstance();
            instance.GoogleMobileAdsAndroidAppId = androidAppID;
        }
        
        public static void SetIOSAppID(string iosAppID)
        {
            var instance = GoogleMobileAdsSettings.LoadInstance();
            instance.GoogleMobileAdsIOSAppId = iosAppID;
        }
    }
}
