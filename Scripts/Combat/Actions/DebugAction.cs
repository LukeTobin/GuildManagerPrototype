using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DebugAction : Action
{
    public string test = "Hello World";

    public override void Perform(SkillInformation info, CharacterCard source, CharacterCard target)
    {
        Debug.Log($"Test skill, created by {source.Data.title} targetting {target.Data.title}. {test} [Info - P:{info.power}], C:{info.cost}, D:{info.scaling.ToString()}, T:{info.targetting.ToString()}");
    }
}