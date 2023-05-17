using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Biome", menuName = "Dungeons/Biome", order = 1)]
public class DungeonBiome : ScriptableObject
{   
    public List<Enemy> dungeonEnemies = new List<Enemy>();

    [SerializeField] public List<EnemyDecklist> EnemyDecklists = new List<EnemyDecklist>();
    
    [Header("Bosses")]
    [SerializeField] public List<EnemyDecklist> BossDecklists = new List<EnemyDecklist>();
}

[Serializable]
public struct EnemyDecklist{
    public List<Enemy> decklist;
}