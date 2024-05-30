using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;

public class NetworkManager : MonoBehaviour
{
    public string ip = "http://172.100.4.82";
    public string port = "3000";
    void Start()
    {
        // A correct website page.
        StartCoroutine(GetRequest(ip + ":" + port));

        // A non-existing page.
        StartCoroutine(GetRequest("https://error.html"));


        StartCoroutine(CreateDweller());
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    IEnumerator CreateDweller()
    {
        using (UnityWebRequest www = UnityWebRequest.Post(ip + ":" + port + "/api/vaultdweller/create", "{\"name\": \"El Ivan\", \"gender\": \"Masculino\", \"life\": 1, \"level\": 9, \"strength\": 1, \"perception\": 5, \"endurance\": 2, \"charisma\": 7, \"inteligence\": 9, \"agility\": 4, \"luck\": 4}", "application/json"))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}