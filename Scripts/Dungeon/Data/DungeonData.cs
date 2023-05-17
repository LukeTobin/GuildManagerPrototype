using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

[System.Serializable]
public class DungeonData
{
    public string id = "";

    [OptionalField]
    public List<char> layout = new List<char>();

    [OptionalField]
    public char rating;

    public int progress = 0;
    
    [OptionalField]
    public DungeonBiome biome;

    [OptionalField]
    public Decklist decklist;
}