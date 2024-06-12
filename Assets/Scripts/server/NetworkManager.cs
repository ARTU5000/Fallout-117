using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;

public class NetworkManager : MonoBehaviour
{
    public string ip = "http://172.100.4.82";
    public string port = "3000";
    [SerializeField] int[] resource = new int[3]; //0 = energia, 1 = agua, 2 = comida
    [SerializeField] int[] resourceOnline = new int[3];


    public GameObject[] resourceImage = new GameObject[3];
    int index;

    //public TextMeshProUGUI Pointer;
    private string rute;
    private SaveResources SaveP;

    private float minuteTimer = 0f;
    private float secondTimer = 0f;
    private const float minuteInterval = 60f;
    private const float secondInterval = 20f;

    
    public void Save()
    {
        rute = Application.streamingAssetsPath + "/Resources.json";
        SaveP = new SaveResources(resource[0], resource[1], resource[2]);
        string json = JsonUtility.ToJson(SaveP, true);
        System.IO.File.WriteAllText(rute, json);
    }

    public void Load()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Resources.json");

        if (System.IO.File.Exists(filePath))
        {
            rute = Application.streamingAssetsPath + "/Resources.json";
            string json = System.IO.File.ReadAllText(rute);
            SaveP = JsonUtility.FromJson<SaveResources>(json);

            resource[0] = SaveP.NRG;
            resource[1] = SaveP.WTR;
            resource[2] = SaveP.FUD;
        }
        else
        {
            resource[0] = 0;
            resource[1] = 0;
            resource[2] = 0;
            Save();
        }
    }
    void Start()
    {
        index = 0;
        // A correct website page.
        StartCoroutine(GetRequest(ip + ":" + port));
        /*
        // A non-existing page.
        StartCoroutine(GetRequest("https://error.html"));


        StartCoroutine(CreateDweller());
    
        StartCoroutine(CreateDweller());*/
        StartCoroutine(ManageDweller());
        //GetDweller();
    }
    
    IEnumerator ManageDweller()
    {
        int dwellerId = 1; // ID del dweller que quieres obtener y actualizar
        yield return StartCoroutine(GetDweller(dwellerId));

        // Suponiendo que tienes un VaultDweller object
        VaultDweller dweller = new VaultDweller
        {
            energia = 4,
            agua = 5,
            comida = 6
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
        string uri = ip + ":" + port + "/api/vaultdweller/getDatos";
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

                resourceOnline[0] = dweller.energia;
                resourceOnline[1] = dweller.agua;
                resourceOnline[2] = dweller.comida;
            }
        }
    }
    IEnumerator UpdateDweller(VaultDweller dweller)
    {
        string uri = ip + ":" + port + "/api/vaultdweller/setDatos";
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

    public void Get()
    {
        int dwellerId = 1; // ID del dweller que quieres obtener y actualizar
        GetDweller(dwellerId);


        VaultDweller dweller = new VaultDweller
        {
            energia = 4,
            agua = 5,
            comida = 6
        };

        UpdateDweller(dweller);
    }


    public void send()
    {
        int dwellerId = 1; // ID del dweller que quieres obtener y actualizar
        GetDweller(dwellerId);

        switch (index)
        {
            case 0:
                VaultDweller dweller = new VaultDweller
                {
                    energia = resourceOnline[0] += 5,
                    agua = resourceOnline[0],
                    comida = resourceOnline[0]
                };
                UpdateDweller(dweller);
            break;
            case 1:
                VaultDweller dweller1 = new VaultDweller
                {
                    energia = resourceOnline[0],
                    agua = resourceOnline[0] += 5,
                    comida = resourceOnline[0]
                };
                UpdateDweller(dweller1);
                break;
            case 2:
                VaultDweller dweller2 = new VaultDweller
                {
                    energia = resourceOnline[0],
                    agua = resourceOnline[0],
                    comida = resourceOnline[0] += 5
                };
                UpdateDweller(dweller2);
                break;
        }

    }

    public void next()
    {

    }

    public void prev()
    {

    }
}
[System.Serializable]
public class VaultDweller
{
    public int id;
    public int energia;
    public int agua;
    public int comida;
}