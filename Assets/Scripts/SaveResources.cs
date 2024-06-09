using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveResources
{
    public int NRG; //Energy
    public int WTR; //Water
    public int FUD; //Food

    public SaveResources (int NRG, int WTR, int FUD)
    {
        this.NRG = NRG;
        this.WTR = WTR;
        this.FUD = FUD;
    }
}
