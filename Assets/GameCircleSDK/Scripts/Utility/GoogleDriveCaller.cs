using System.Collections;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

namespace GameCircleSDK.Utility
{
    public static class GoogleDriveCaller
    {
        private static string _urlDelete1 = "/edit?usp=sharing";
        private static string _urlDelete2 = "/edit?usp=drive_link";
        private static string _urlAdd = "/gviz/tq?tqx=out:csv&headers=0";

        public static IEnumerator Call(string fileId)
        {
            var request = PrepareRequest(fileId);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Fetch unsuccessful.");
            }
            else
            {
                CSVParser.Parse(request.downloadHandler.text);
            }
            
            request.Dispose();
        }

        private static UnityWebRequest PrepareRequest(string fileId)
        {
            if(fileId.Contains(_urlDelete1))
                fileId = fileId.Replace(_urlDelete1, _urlAdd);
            if(fileId.Contains(_urlDelete2))
                fileId = fileId.Replace(_urlDelete2, _urlAdd);
            return UnityWebRequest.Get(fileId);
        }
    }
}