using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] int[] resource = new int[3]; //0 = energia, 1 = agua, 2 = comida
    SaveResources SaveP;
    string rute;

    void start()
    {
        Time.timeScale = 1f;
    }

    public void menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("mainmenu");
        Time.timeScale = 1f;
    } 

    public void intro()
    {
        Time.timeScale = 1f;

        resource[0] = 0;
        resource[1] = 0;
        resource[2] = 0;
        Save();

        SceneManager.LoadScene("intro");
        Time.timeScale = 1f;
    }  

    public void juego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Juego");
        Time.timeScale = 1f;
    } 

    public void finala()
    {
        Time.timeScale = 1f;

        resource[0] = 0;
        resource[1] = 0;
        resource[2] = 0;
        Save();

        SceneManager.LoadScene("FinalA");
        Time.timeScale = 1f;
    }

    public void finalb()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("FinalB");
        Time.timeScale = 1f;
    }

    public void Save()
    {
        rute = Application.streamingAssetsPath + "/Resources.json";
        SaveP = new SaveResources(resource[0], resource[1], resource[2]);
        string json = JsonUtility.ToJson(SaveP, true);
        System.IO.File.WriteAllText(rute, json);
    } 
}
