using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Action
{
    public abstract void Perform(SkillInformation info, CharacterCard source, CharacterCard target);
}