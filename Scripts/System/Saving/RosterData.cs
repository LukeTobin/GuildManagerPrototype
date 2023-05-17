using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[System.Serializable]
public class RosterData
{   
    [OptionalField]
    public List<AdventurerData> adventurers = new List<AdventurerData>();
    
    [OptionalField]
    public List<CardData> valuables = new List<CardData>();
}
