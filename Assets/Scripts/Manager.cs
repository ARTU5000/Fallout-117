using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Character", menuName = "ScriptableObjects/Characters", order = 1)]

public class Manager : ScriptableObject
{
    SPECIAL NPC;

    public string Name;
    
    public int Strenght;
    public int Perception;
    public int Endurance;
    public int Charisma;
    public int Inteligence;
    public int Agility;
    public int Luck;
    
    public int Level;
    public float Health;
    public float Radiation;
    public int CarryWeight;

    // Start is called before the first frame update
    void Start()
    {
        NPC = new SPECIAL(Name, Strenght, Perception, Endurance, Charisma, Inteligence, Agility, Luck, Level, Health, Radiation, CarryWeight);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
