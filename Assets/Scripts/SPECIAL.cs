using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPECIAL
{
    private string Name;

    private int Strenght;
    private int Perception;
    private int Endurance;
    private int Charisma;
    private int Inteligence;
    private int Agility;
    private int Luck;

    private int Level;
    private float Health;
    private float Radiation;
    private int CarryWeight;

    public SPECIAL(string Name, int Strenght, int Perception, int Endurance, int Charisma, int Inteligence, int Agility, int Luck, int Level, float Health, float Radiation, int CarryWeight)
    {
        this.Name = Name; 
        this.Strenght = Strenght;
        this.Perception = Perception;
        this.Endurance = Endurance;
        this.Charisma = Charisma;
        this.Inteligence = Inteligence;
        this.Agility = Agility;
        this.Luck = Luck;
        this.Level = Level;
        this.Health = Health;
        this.Radiation = Radiation;
        this.CarryWeight = CarryWeight;
    }

    public string name
    {
        get { return Name; }
        set { Name = value; }
    }
    public int strenght 
    {
        get { return Strenght; }
        set { Strenght = value; }
    }
    public int perception
    {
        get { return Perception; }
        set { Perception = value; }
    }
    public int endurance
    {
        get { return Endurance; }
        set { Endurance = value; }
    }
    public int charisma
    {
        get { return Charisma; }
        set { Charisma = value; }
    }
    public int inteligence
    {
        get { return Inteligence; }
        set { Inteligence = value; }
    }
    public int agility
    {
        get { return Agility; }
        set { Agility = value; }
    }
    public int luck
    {
        get { return Luck; }
        set { Luck = value; }
    }
    public int level
    {
        get { return Level; }
        set { Level = value; }
    }
    public float health
    {
        get { return Health; }
        set { Health = value; }
    }
    public float radiation
    {
        get { return Radiation; }
        set { Radiation = value; }
    }
    public int carryweight
    {
        get { return CarryWeight; }
        set { CarryWeight = value; }
    }
}
