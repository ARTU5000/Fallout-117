using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ResourceReducer : MonoBehaviour
{
    [SerializeField]int[] resource = new int[3]; //0 = energia, 1 = agua, 2 = comida
    public TextMeshProUGUI Pointer;
    private string rute;
    private SaveResources SaveP;

    private float minuteTimer = 0f;
    private float secondTimer = 0f;
    private const float minuteInterval = 60f;
    private const float secondInterval = 20f;

    void Start()
    {
        Load();
    }

    void Update()
    {
        minuteTimer += Time.deltaTime;
        secondTimer += Time.deltaTime;

        if (minuteTimer >= minuteInterval)
        {
            ReduceResources();
            minuteTimer = 0f;
        }

        if (secondTimer >= secondInterval)
        {
            ReduceRandomResource();
            secondTimer = 0f;
        }

        Pointer.text = "Energia: " + resource[0].ToString() + "\n Agua: " + resource[1].ToString() + "\n Comida: " + resource[2].ToString();
    }

    void ReduceResources()
    {
        Load();
        for (int i = 0; i < resource.Length; i++)
        {
            resource[i] --;
        }
        Save();
    }

    void ReduceRandomResource()
    {
        Load();
        int randomIndex = Random.Range(0, resource.Length);
        resource[randomIndex] --;
        Save();
    }

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

        if (File.Exists(filePath))
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
}