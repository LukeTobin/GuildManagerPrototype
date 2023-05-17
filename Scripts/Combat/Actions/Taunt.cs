[System.Serializable]
public class Taunt : Action
{
    public override void Perform(SkillInformation info, CharacterCard source, CharacterCard target)
    {
        // change too something else in future
        source.Status.focus += info.power;
    }
}