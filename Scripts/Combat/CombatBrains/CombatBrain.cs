using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatBrain
{
    [HideInInspector] public bool evaluating = false;
    [HideInInspector] public int skillIndex = -1;

    /// <summary>
    /// Evaluate which skill should be used by the character
    /// </summary>
    /// <param name="source">The character that want's to evaluate a skill choice</param>
    /// <param name="team">The character's team</param>
    /// <param name="enemies">The opposing team</param>
    /// <returns>An Intent</returns>
    public virtual Intent Evaluate(CharacterCard source, List<CharacterCard> team, List<CharacterCard> enemies){
        // get a value for each skill
        List<Skill> availableSkills = new List<Skill>();
        for(int i = 0;i < source.Data.skills.Count;i++){
            if(!OnCooldown(source.Data.skills[i], source))
                availableSkills.Add(source.Data.skills[i]);
        }

        if(availableSkills.Count <= 0) return null;

        Skill skill = availableSkills[Random.Range(0, availableSkills.Count)];
        List<CharacterCard> targets = new List<CharacterCard>();

        switch(skill.Information.targetting){
            case Targetting.Enemy:
                targets.Add(enemies[Random.Range(0, enemies.Count)]);
                break;
            case Targetting.Ally:
                targets.Add(team[Random.Range(0, team.Count)]);
                break;
            case Targetting.All:
                targets.AddRange(team);
                targets.AddRange(enemies);
                break;
            case Targetting.AllEnemy:
                targets.AddRange(enemies);
                break;
            case Targetting.AllAlly:
                targets.AddRange(team);
                break;
            case Targetting.Self:
                targets.Add(source);
                break;
            default:
                Debug.LogWarning("Could evaluate skill based on targetting type");
                break;
        }

        return new Intent(skill, targets);
    }

    protected bool OnCooldown(Skill skill, CharacterCard character){
        if(character.cooldowns.Count <= 0) return false;

        for(int i = 0;i < character.cooldowns.Count;i++){
            if(skill == character.cooldowns[i].skill)
                return true;
        }

        return false;
    }
}