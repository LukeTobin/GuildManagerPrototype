using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StandardCombatBrain : CombatBrain
{
    public override Intent Evaluate(CharacterCard source, List<CharacterCard> team, List<CharacterCard> enemies)
    {
        return base.Evaluate(source, team, enemies);
    }
}
