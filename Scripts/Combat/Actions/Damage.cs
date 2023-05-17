[System.Serializable]
public class Damage : Action
{
    public override void Perform(SkillInformation info, CharacterCard source, CharacterCard target)
    {
        target.Damage(info, source.Data);
    }
}