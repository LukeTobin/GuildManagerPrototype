using UnityEngine;

[System.Serializable]
public class ScaledDamage : Action
{
    public float multiplier = 1;
    public int bonusDamage = 0;

    public override void Perform(SkillInformation info, CharacterCard source, CharacterCard target)
    {
        SkillInformation newInfo = new SkillInformation(info);
        newInfo.power += bonusDamage;
        newInfo.power = Mathf.RoundToInt(newInfo.power * multiplier);
        target.Damage(newInfo, source.Data);
    }
}