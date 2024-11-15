using System;
using System.Linq;
using Facebook.Unity.Editor;
using Facebook.Unity.Settings;
using GameAnalyticsSDK;
using GameCircleSDK.Utility;
using GoogleMobileAds.Editor;
using UnityEditor;
using UnityEngine;
using MessageType = UnityEditor.MessageType;

namespace GameCircleSDK.Scripts.Editor
{
	[CustomEditor(typeof(GCSettings))]
	public class SettingEditor : UnityEditor.Editor
	{
		private int _choice;
		private readonly string[] _options = { "None", "AdMob", "AdMost" };
		private bool _noPrefabFoundExceptionThrown = false;
		
		[MenuItem("Game Circle/Settings")]
		public static void Getter(){
			Selection.activeObject = GameCircle.Settings;
		}

		public void OnEnable()
		{
			GameCircle.Settings = (GCSettings)target;
		}

		private static void Splitter()
		{
			var splitter = new GUIStyle
			{
				normal =
				{
					background = EditorGUIUtility.whiteTexture
				},
				stretchWidth = true,
				margin = new RectOffset(15, 0, 10, 5)
			};

			var position = GUILayoutUtility.GetRect(GUIContent.none, splitter, GUILayout.Height(1));

			if (Event.current.type != EventType.Repaint) return;
			
			var restoreColor = GUI.color;
			GUI.color = new Color(0.4f, 0.4f, 0.45f);
			splitter.Draw(position, false, false, false, false);
			GUI.color = restoreColor;
		}

		public override void OnInspectorGUI()
		{
			SetLogoAndVersion();
			EditorGUILayout.Space();
			SetFetchButton();
			EditorGUILayout.Space();
			SetFacebookSettings();
			EditorGUILayout.Space();
			SetGameAnalyticsSettings();
			EditorGUILayout.Space();
			SetAdjustSettings();
			EditorGUILayout.Space();
			SetAdMediatorSettings();
			EditorGUILayout.Space();
			SetFirebaseSettings();
			Undo.RecordObject(GameCircle.Settings, "Changed");
			Getter();
		}

		private void SetLogoAndVersion()
		{
			if (GameCircle.Settings.gameCircleIcon == null)
			{
				GameCircle.Settings.gameCircleIcon = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/GameCircleSDK/Images/GameCircleLogo.png", typeof(Texture2D));
			}
			
			GUILayout.BeginHorizontal();

			GUILayout.Label(GameCircle.Settings.gameCircleIcon, new GUILayoutOption[] {
				GUILayout.Width(48),
				GUILayout.Height(48)
			});

			GUILayout.BeginVertical();

			GUILayout.Space(16);

			GUILayout.BeginHorizontal();
			GUILayout.Label("Game Circle Unity SDK v.1.0.0");
			GUILayout.EndHorizontal();
			
			GUILayout.EndVertical();
			
			GUILayout.EndHorizontal();
		}

		private void SetFetchButton()
		{
			if (_noPrefabFoundExceptionThrown)
			{
				EditorGUILayout.HelpBox(EditorStrings.ERROR_NoPrefabFound, MessageType.Error);
			}
			
			GameCircle.Settings.fileId = EditorGUILayout.TextField("Data ID", GameCircle.Settings.fileId);

			if (GUILayout.Button("Fetch") && GameCircle.Settings.fileId != "")
			{
				try
				{
					FindObjectOfType<GCSDKInitializer>().FetchData(GameCircle.Settings.fileId);
					_noPrefabFoundExceptionThrown = false;
				}
				catch (Exception)
				{
					_noPrefabFoundExceptionThrown = true;
				}
			}
			
			Splitter();
		}

		private void SetFacebookSettings()
		{
			GameCircle.Settings.usingFacebook = EditorGUILayout.ToggleLeft("Facebook", GameCircle.Settings.usingFacebook);

			if (!GameCircle.Settings.usingFacebook)
			{
				FacebookSettings.AppIds[FacebookSettings.SelectedAppIndex] = string.Empty;
				FacebookSettings.ClientTokens[FacebookSettings.SelectedAppIndex] = string.Empty;
				FacebookSettings.AndroidKeystorePath = string.Empty;
				return;
			}

			EditorGUI.indentLevel += 2;
			
			if(GameCircle.Settings.FACEBOOK_AppID == "")
				EditorGUILayout.HelpBox(EditorStrings.ERROR_FACEBOOK_AppID_Empty, MessageType.Error);
			GameCircle.Settings.FACEBOOK_AppID = EditorGUILayout.TextField("App ID", GameCircle.Settings.FACEBOOK_AppID);
			
			if(GameCircle.Settings.FACEBOOK_ClientToken == "")
				EditorGUILayout.HelpBox(EditorStrings.ERROR_FACEBOOK_ClientToken_Empty, MessageType.Error);
			GameCircle.Settings.FACEBOOK_ClientToken = EditorGUILayout.TextField("Client Token", GameCircle.Settings.FACEBOOK_ClientToken);
			
			if(GameCircle.Settings.FACEBOOK_AndroidKeystorePath == "")
				EditorGUILayout.HelpBox(EditorStrings.ERROR_FACEBOOK_AndroidKeystorePath_Empty, MessageType.Error);
			GameCircle.Settings.FACEBOOK_AndroidKeystorePath = EditorGUILayout.TextField("Android Keystore Path", GameCircle.Settings.FACEBOOK_AndroidKeystorePath);
			
			FacebookSettings.AppIds[FacebookSettings.SelectedAppIndex] = GameCircle.Settings.FACEBOOK_AppID;
			FacebookSettings.ClientTokens[FacebookSettings.SelectedAppIndex] = GameCircle.Settings.FACEBOOK_ClientToken;
			FacebookSettings.AndroidKeystorePath = GameCircle.Settings.FACEBOOK_AndroidKeystorePath;
			if(GameCircle.Settings.FACEBOOK_AppID != "")
				ManifestMod.GenerateManifest();
			
			EditorGUI.indentLevel -= 2;
			Splitter();
		}

		private void SetGameAnalyticsSettings()
		{
			GameCircle.Settings.usingGameAnalytics = EditorGUILayout.ToggleLeft("Game Analytics", GameCircle.Settings.usingGameAnalytics);

			if (!GameCircle.Settings.usingGameAnalytics)
			{
				for(var i = 0; i < GameAnalytics.SettingsGA.Platforms.Count; i++)
				{
					if (GameAnalytics.SettingsGA.Platforms[i] == RuntimePlatform.Android)
						GameAnalytics.SettingsGA.RemovePlatformAtIndex(i);
				}

				return;
			}
			EditorGUI.indentLevel += 2;

			if(GameCircle.Settings.GAMEANALYTICS_GameKey == "")
				EditorGUILayout.HelpBox(EditorStrings.ERROR_GAMEANALYTICS_GameKey_Empty, MessageType.Error);
			GameCircle.Settings.GAMEANALYTICS_GameKey = EditorGUILayout.TextField("Game Key", GameCircle.Settings.GAMEANALYTICS_GameKey);
			
			if(GameCircle.Settings.GAMEANALYTICS_SecretKey == "")
				EditorGUILayout.HelpBox(EditorStrings.ERROR_GAMEANALYTICS_SecretKey_Empty, MessageType.Error);
			GameCircle.Settings.GAMEANALYTICS_SecretKey = EditorGUILayout.TextField("Secret Key", GameCircle.Settings.GAMEANALYTICS_SecretKey);
			
			if (GameAnalytics.SettingsGA.GetAvailablePlatforms().Contains("Android"))
			{
				GameAnalytics.SettingsGA.AddPlatform(RuntimePlatform.Android);
			}
			for(var i = 0; i < GameAnalytics.SettingsGA.Platforms.Count; i++)
			{
				if (GameAnalytics.SettingsGA.Platforms[i] != RuntimePlatform.Android) continue;
				GameAnalytics.SettingsGA.UpdateGameKey(i, GameCircle.Settings.GAMEANALYTICS_GameKey);
				GameAnalytics.SettingsGA.UpdateSecretKey(i, GameCircle.Settings.GAMEANALYTICS_SecretKey);
			
			}
			EditorGUI.indentLevel -= 2;
			Splitter();
		}

		private void SetAdjustSettings()
		{
			GameCircle.Settings.usingAdjust = EditorGUILayout.ToggleLeft("Adjust", GameCircle.Settings.usingAdjust);

			if (!GameCircle.Settings.usingAdjust)
			{
				return;
			}

			EditorGUI.indentLevel += 2;
			
			if(GameCircle.Settings.ADJUST_AppToken == "")
				EditorGUILayout.HelpBox(EditorStrings.ERROR_ADJUST_AppToken_Empty, MessageType.Error);
			GameCircle.Settings.ADJUST_AppToken = EditorGUILayout.TextField("App Token", GameCircle.Settings.ADJUST_AppToken);
			
			if(GameCircle.Settings.ADJUST_AppSecret == "")
				EditorGUILayout.HelpBox(EditorStrings.ERROR_ADJUST_AppSecret_Empty, MessageType.Error);
			GameCircle.Settings.ADJUST_AppSecret = EditorGUILayout.TextField("App Secret", GameCircle.Settings.ADJUST_AppSecret);
			
			EditorGUI.indentLevel -= 2;
			Splitter();
		}

		private void SetAdMediatorSettings()
		{
			for (var i = 0; i < _options.Length; i++)
			{
				if (_options[i] == GameCircle.Settings.adMediator)
					_choice = i;
			}
			_choice = EditorGUILayout.Popup("Ad Mediator", _choice, _options);
			GameCircle.Settings.adMediator = _options[_choice];
		
			if(GameCircle.Settings.UsingAdMob())
				SetAdMobSettings();
			if(GameCircle.Settings.UsingAdMost())
				SetAdMostSettings();
		
		}
	
		private void SetAdMobSettings()
		{
			if(!GameCircle.Settings.usingAdMobAndroid && !GameCircle.Settings.usingAdMobIOS)
				EditorGUILayout.HelpBox(EditorStrings.ERROR_ChooseAtLeastOne, MessageType.Error);
				
			SetAdMobAndroidSettings();
			SetAdMobIOSSettings();
			Splitter();
		}

		private void SetAdMobAndroidSettings()
		{
			EditorGUI.indentLevel += 2;
			GameCircle.Settings.usingAdMobAndroid = EditorGUILayout.ToggleLeft("Android", GameCircle.Settings.usingAdMobAndroid);
			EditorGUI.indentLevel -= 2;
			if (!GameCircle.Settings.usingAdMobAndroid) return;
			EditorGUI.indentLevel += 4;
			EditorGUILayout.HelpBox(EditorStrings.WARNING_LeaveUnusedEmpty, MessageType.Warning);
			if(GameCircle.Settings.ADMOB_AndroidAppID == "")
				EditorGUILayout.HelpBox(EditorStrings.ERROR_ADMOB_Android_AppID_Empty, MessageType.Error);
			GameCircle.Settings.ADMOB_AndroidAppID = EditorGUILayout.TextField("App ID", GameCircle.Settings.ADMOB_AndroidAppID);
			GameCircle.Settings.ADMOB_AndroidIntersititialID = EditorGUILayout.TextField("Interstitial ID", GameCircle.Settings.ADMOB_AndroidIntersititialID);
			GameCircle.Settings.ADMOB_AndroidRewardedID = EditorGUILayout.TextField("Rewarded ID", GameCircle.Settings.ADMOB_AndroidRewardedID);
			GameCircle.Settings.ADMOB_AndroidBannerID = EditorGUILayout.TextField("Banner ID", GameCircle.Settings.ADMOB_AndroidBannerID);
			EditorGUI.indentLevel -= 4;
			if(GameCircle.Settings.ADMOB_AndroidAppID != "")
				GCAdMobAdapter.SetAndroidAppID(GameCircle.Settings.ADMOB_AndroidAppID);
		}

		private void SetAdMobIOSSettings()
		{
			EditorGUI.indentLevel += 2;
			GameCircle.Settings.usingAdMobIOS = EditorGUILayout.ToggleLeft("IOS", GameCircle.Settings.usingAdMobIOS);
			EditorGUI.indentLevel -= 2;
			if (!GameCircle.Settings.usingAdMobIOS) return;
			EditorGUI.indentLevel += 4;
			EditorGUILayout.HelpBox(EditorStrings.WARNING_LeaveUnusedEmpty, MessageType.Warning);
			if(GameCircle.Settings.ADMOB_IOSAppID == "")
				EditorGUILayout.HelpBox(EditorStrings.ERROR_ADMOB_IOS_AppID_Empty, MessageType.Error);
			GameCircle.Settings.ADMOB_IOSAppID = EditorGUILayout.TextField("App ID", GameCircle.Settings.ADMOB_IOSAppID);
			GameCircle.Settings.ADMOB_IOSIntersititialID = EditorGUILayout.TextField("Interstitial ID", GameCircle.Settings.ADMOB_IOSIntersititialID);
			GameCircle.Settings.ADMOB_IOSRewardedID = EditorGUILayout.TextField("Rewarded ID", GameCircle.Settings.ADMOB_IOSRewardedID);
			GameCircle.Settings.ADMOB_IOSBannerID = EditorGUILayout.TextField("Banner ID", GameCircle.Settings.ADMOB_IOSBannerID);
			EditorGUI.indentLevel -= 4;
			if(GameCircle.Settings.ADMOB_IOSAppID != "")
				GCAdMobAdapter.SetIOSAppID(GameCircle.Settings.ADMOB_IOSAppID);
		}

		private void SetAdMostSettings()
		{
			if(!GameCircle.Settings.usingAdMostAndroid && !GameCircle.Settings.usingAdMostIOS)
				EditorGUILayout.HelpBox(EditorStrings.ERROR_ChooseAtLeastOne, MessageType.Error);
			SetAdMostAndroidSettings();
			SetAdMostIOSSettings();
			Splitter();
		}

		private void SetAdMostAndroidSettings()
		{
			EditorGUI.indentLevel += 2;
			GameCircle.Settings.usingAdMostAndroid = EditorGUILayout.ToggleLeft("Android", GameCircle.Settings.usingAdMostAndroid);
			EditorGUI.indentLevel -= 2;
			if (!GameCircle.Settings.usingAdMostAndroid) return;
			EditorGUI.indentLevel += 4;
			EditorGUILayout.HelpBox(EditorStrings.WARNING_LeaveUnusedEmpty, MessageType.Warning);
			GameCircle.Settings.ADMOST_AndroidAppID = EditorGUILayout.TextField("App ID", GameCircle.Settings.ADMOST_AndroidAppID);
			GameCircle.Settings.ADMOST_AndroidIntersititialID = EditorGUILayout.TextField("Interstitial ID", GameCircle.Settings.ADMOST_AndroidIntersititialID);
			GameCircle.Settings.ADMOST_AndroidRewardedID = EditorGUILayout.TextField("Rewarded ID", GameCircle.Settings.ADMOST_AndroidRewardedID);
			GameCircle.Settings.ADMOST_AndroidBannerID = EditorGUILayout.TextField("Banner ID", GameCircle.Settings.ADMOST_AndroidBannerID);
			EditorGUI.indentLevel -= 4;
			if(GameCircle.Settings.ADMOST_AndroidAppID == "")
				EditorGUILayout.HelpBox(EditorStrings.ERROR_ADMOST_AndroidAppID_Empty, MessageType.Error);
		}

		private void SetAdMostIOSSettings()
		{
			EditorGUI.indentLevel += 2;
			GameCircle.Settings.usingAdMostIOS = EditorGUILayout.ToggleLeft("IOS", GameCircle.Settings.usingAdMostIOS);
			EditorGUI.indentLevel -= 2;
			if (!GameCircle.Settings.usingAdMostIOS) return;
			EditorGUI.indentLevel += 4;
			EditorGUILayout.HelpBox(EditorStrings.WARNING_LeaveUnusedEmpty, MessageType.Warning);
			GameCircle.Settings.ADMOST_IOSAppID = EditorGUILayout.TextField("App ID", GameCircle.Settings.ADMOST_IOSAppID);
			GameCircle.Settings.ADMOST_IOSIntersititialID = EditorGUILayout.TextField("Interstitial ID", GameCircle.Settings.ADMOST_IOSIntersititialID);
			GameCircle.Settings.ADMOST_IOSRewardedID = EditorGUILayout.TextField("Rewarded ID", GameCircle.Settings.ADMOST_IOSRewardedID);
			GameCircle.Settings.ADMOST_IOSBannerID = EditorGUILayout.TextField("Banner ID", GameCircle.Settings.ADMOST_IOSBannerID);
			EditorGUI.indentLevel -= 4;
			if(GameCircle.Settings.ADMOST_IOSAppID == "")
				EditorGUILayout.HelpBox(EditorStrings.ERROR_ADMOST_IOSAppID_Empty, MessageType.Error);
		}

		private void SetFirebaseSettings()
		{
			GameCircle.Settings.usingFirebase = EditorGUILayout.ToggleLeft("Firebase", GameCircle.Settings.usingFirebase);
			if (!GameCircle.Settings.usingFirebase) return;
			SetFirebaseAndroidSettings();
			SetFirebaseIOSSettings();
		}

		private void SetFirebaseAndroidSettings()
		{
			EditorGUI.indentLevel += 2;
			if (GameCircle.Settings.usingFirebaseAndroid)
				EditorGUILayout.HelpBox(EditorStrings.WARNING_FIREBASE_Android_IncludeFile, MessageType.Warning);
			GameCircle.Settings.usingFirebaseAndroid = EditorGUILayout.ToggleLeft("Android", GameCircle.Settings.usingFirebaseAndroid);
			EditorGUI.indentLevel -= 2;
		}

		private void SetFirebaseIOSSettings()
		{
			EditorGUI.indentLevel += 2;
			if (GameCircle.Settings.usingFirebaseIOS)
				EditorGUILayout.HelpBox(EditorStrings.WARNING_FIREBASE_IOS_IncludeFile, MessageType.Warning);
			GameCircle.Settings.usingFirebaseIOS = EditorGUILayout.ToggleLeft("IOS", GameCircle.Settings.usingFirebaseIOS);
			EditorGUI.indentLevel -= 2;
		}
	}
}