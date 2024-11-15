using com.adjust.sdk;
using GameCircleSDK.Utility;
using UnityEngine;

namespace GameCircleSDK
{
    public class GCSDKInitializer : MonoBehaviour
    {
        [SerializeField] private Adjust _adjustPrefab;

        public Adjust adjustInstance;
        
        private void Awake()
        {
            if (FindObjectsOfType<GCSDKInitializer>().Length > 1)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }
        
        private void Start()
        {
            GameCircle.Initialize();
            InstantiateAdjustInstance();
        }

        private void InstantiateAdjustInstance()
        {
            if (!GameCircle.Settings.usingAdjust) return;
            adjustInstance = Instantiate(_adjustPrefab, transform);
            adjustInstance.startManually = false;
            adjustInstance.appToken = GameCircle.Settings.ADJUST_AppToken;
            adjustInstance.environment = AdjustEnvironment.Production;
            print(GameCircle.Settings.ADJUST_AppSecret);

            var secretString = GameCircle.Settings.ADJUST_AppSecret.Replace(" ", "");
            secretString = secretString.Replace("(", "").Replace(")", "");
            var secretArray = secretString.Split(",");
            adjustInstance.secretId = long.Parse(secretArray[0]);
            adjustInstance.info1 = long.Parse(secretArray[1]);
            adjustInstance.info2 = long.Parse(secretArray[2]);
            adjustInstance.info3 = long.Parse(secretArray[3]);
            adjustInstance.info4 = long.Parse(secretArray[4]);
        }

        public void FetchData(string fileId)
        {
            StartCoroutine(GoogleDriveCaller.Call(fileId));
        }
    }
}
