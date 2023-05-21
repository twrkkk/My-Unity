using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadFromURL : MonoBehaviour
{
    [SerializeField] private string url;
    [SerializeField] private string savePath;
    [SerializeField] private AlembicTimeline alembicTimeline;

    public void DownloadFile()
    {
        Debug.Log("start download");
        StartCoroutine(DownloadFile(url, Application.dataPath + "/Resources/file.abc"));
    }

    IEnumerator DownloadFile(string url, string savePath)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error downloading file: {request.error}");
            }
            else
            {
                File.WriteAllBytes(savePath, request.downloadHandler.data);
                Debug.Log($"File downloaded and saved to: {savePath}");
                alembicTimeline.Play();
            }
        }
    }
}
