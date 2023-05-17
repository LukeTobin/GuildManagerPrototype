using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Heal : Action
{
    [System.Serializable]
    public enum Healing {
        Flat,
        TargetHealthPercentage
    }

    public Healing healing;

    public override void Perform(SkillInformation info, CharacterCard source, CharacterCard target)
    {        
        switch(healing){
            case Healing.Flat:
                target.Heal(info, source.Data);
                break;
            case Healing.TargetHealthPercentage:
                int heal = Mathf.RoundToInt(target.Data.stats.health * info.power);
                target.Heal(heal);
                break;
        }
    }
}