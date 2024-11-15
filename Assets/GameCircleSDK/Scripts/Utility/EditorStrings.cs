namespace GameCircleSDK.Utility
{
    public static class EditorStrings
    {
        public static string ERROR_NoPrefabFound = "There should be an instance of Game Circle SDK prefab in the first scene to fetch.";

        public static string ERROR_FACEBOOK_AppID_Empty = "App ID can not be empty.";
        public static string ERROR_FACEBOOK_ClientToken_Empty = "Client Token can not be empty.";
        public static string ERROR_FACEBOOK_AndroidKeystorePath_Empty = "Keystore Path can not be empty.";
    
        public static string ERROR_GAMEANALYTICS_GameKey_Empty = "Game Key can not be empty.";
        public static string ERROR_GAMEANALYTICS_SecretKey_Empty = "Secret Key can not be empty.";
    
        public static string ERROR_ADJUST_AppToken_Empty = "App Token can not be empty.";
        public static string ERROR_ADJUST_AppSecret_Empty = "App Secret can not be empty.";
        
        public static string ERROR_ADMOB_Android_AppID_Empty = "App ID can not be empty.";
        public static string ERROR_ADMOB_IOS_AppID_Empty = "App ID can not be empty.";
        
        public static string ERROR_ChooseAtLeastOne = "At least one platform should be enabled.";
        public static string ERROR_ADMOST_AndroidAppID_Empty = "Android App ID can not be empty.";
        public static string ERROR_ADMOST_IOSAppID_Empty = "IOS App ID can not be empty.";
        
        public static string WARNING_FIREBASE_Android_IncludeFile = "Make sure to have the 'google-services.json' file under 'Assets'.";
        public static string WARNING_FIREBASE_IOS_IncludeFile = "Make sure to have the 'GoogleService-Info_1.plist' file under 'Assets'.";
        
        public static string WARNING_LeaveUnusedEmpty = "Make sure the IDs of unused ad types are left empty.";
    }
}
