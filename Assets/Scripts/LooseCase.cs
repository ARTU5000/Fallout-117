using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LooseCase : MonoBehaviour
{
    public GameObject luse;
    public GameObject bajo;
    SaveResources SaveP;
    string rute;
    [SerializeField] int[] resource = new int[3]; //0 = energia, 1 = agua, 2 = comida
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        luse.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (int item in resource)
        {
            if (item < -20) 
            {
                luse.SetActive(true);
                Time.timeScale = 0f;
            }

        }

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

            resource[0] = (SaveP.NRG);
            resource[1] = (SaveP.WTR);
            resource[2] = (SaveP.FUD);
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
