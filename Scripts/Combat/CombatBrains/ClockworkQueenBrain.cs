using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClockworkQueenBrain : CombatBrain
{
    public Skill customSkill;

    public override Intent Evaluate(CharacterCard source, List<CharacterCard> team, List<CharacterCard> enemies)
    {
        if(team.Count < 2){
            return new Intent(customSkill, new List<CharacterCard>(){source});
        }
        else return base.Evaluate(source, team, enemies);
    }
}
