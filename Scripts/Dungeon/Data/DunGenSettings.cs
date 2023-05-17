using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "DunGen Setting", menuName = "Dungeons/DunGen Setting", order = 1)]
public class DunGenSettings : ScriptableObject
{
    [Header("Size Limits")]
    [Range(1,10)] public int minSize = 3;
    [Range(1,20)] public int maxSize = 15;

    [Header("Enemy Weights")]
    [Range(0,100)] public int enemyChance = 50;
    [Range(0,10)]  public int presetDeckWeight = 3;
    
    [Header("Enemies")]
    public List<Enemy> randomEnemies = new List<Enemy>();
    [Space] public List<Decklist> enemyDecklists = new List<Decklist>();

    //[Header("Boss Weights")]

    //[Header("Biome Weights")]

    //[Header("Unique Loot")]
}
