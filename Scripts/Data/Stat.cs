using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public enum StatType{
        Vitality,
        Recovery,
        Resilience,
        Toughness, 
        Agility,
        Dexterity,
        Swiftness,
        Precision,
        Proficiency,
        Magicka,
        Physical,
        Power,
        Capability,
        Luck,
        Prowess,
        Mental
    }

    public StatType stat;
}
