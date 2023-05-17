using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SpawnEnemy : Action
{
    public Enemy spawn;

    public bool randomCount;
    [ShowIf("@randomCount == true")]
    public int maxRandom = 2;

    public override void Perform(SkillInformation info, CharacterCard source, CharacterCard target)
    {
        if(source == null) return;
        
        int spawnCount = randomCount == true ? Random.Range(1, maxRandom+1) : 1;
        
        for(int i = 0;i < spawnCount;i++){
            if(source == null) break;
            source.Dungeon.SpawnEnemy(spawn);
        }
    }
}