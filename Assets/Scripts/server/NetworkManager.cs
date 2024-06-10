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
    
        StartCoroutine(CreateDweller());
        StartCoroutine(ManageDweller());
    }
    
    IEnumerator ManageDweller()
    {
        int dwellerId = 1; // ID del dweller que quieres obtener y actualizar
        yield return StartCoroutine(GetDweller(dwellerId));
    
        // Suponiendo que tienes un VaultDweller object
        VaultDweller dweller = new VaultDweller
        {
            id = dwellerId,
            name = "El Ivan",
            gender = "Masculino",
            life = 2,
            level = 10,
            strength = 2,
            perception = 6,
            endurance = 3,
            charisma = 8,
            inteligence = 10,
            agility = 5,
            luck = 5
        };
    
        yield return StartCoroutine(UpdateDweller(dweller));
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
        IEnumerator GetDweller(int dwellerId)
    {
        string uri = ip + ":" + port + "/api/vaultdweller/getDweller";
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, "{\"id\": " + dwellerId + "}", "application/json"))
        {
            yield return webRequest.SendWebRequest();
    
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Received: " + webRequest.downloadHandler.text);
                // Parse the JSON response
                VaultDweller dweller = JsonUtility.FromJson<VaultDweller>(webRequest.downloadHandler.text);
                // Now you can use the dweller object
            }
        }
    }
    IEnumerator UpdateDweller(VaultDweller dweller)
    {
        string uri = ip + ":" + port + "/api/vaultdweller/update";
        string jsonData = JsonUtility.ToJson(dweller);
    
        using (UnityWebRequest webRequest = UnityWebRequest.Put(uri, jsonData))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();
    
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Update complete!");
            }
        }
    }
}
[System.Serializable]
public class VaultDweller
{
    public int id;
    public string name;
    public string gender;
    public int life;
    public int level;
    public int strength;
    public int perception;
    public int endurance;
    public int charisma;
    public int inteligence;
    public int agility;
    public int luck;
}