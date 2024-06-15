using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class terminales : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    public GameObject elTrigger;
    private bool inTriggerZone = false;
    [SerializeField] int[] resource = new int[3]; //0 = energia, 1 = agua, 2 = comida
    SaveResources SaveP;
    string rute;
    private Collider triggerCollider;

    // Start is called before the first frame update
    void Start()
    {
        triggerCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inTriggerZone && Input.GetKeyDown(KeyCode.E))
        {
            Load();
            int randomIndex = UnityEngine.Random.Range(0, resource.Length);
            resource[randomIndex]++;
            Save();
            infoText.text = "Simplemente funciona";
            StartCoroutine(DisableTriggerForTwoMinutes());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == elTrigger)
        {
            inTriggerZone = true;
            infoText.text = "Presiona 'E' para supervisar";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == elTrigger)
        {
            inTriggerZone = false;
            infoText.text = "";
        }
    }

    IEnumerator DisableTriggerForTwoMinutes()
    {
        triggerCollider.enabled = false;
        yield return new WaitForSeconds(5); // 5 segundos
        infoText.text = "";
        yield return new WaitForSeconds(120); // 120 segundos = 2 minutos
        triggerCollider.enabled = true;
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
