using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "New Skill")]
[System.Serializable]
public class Skill : ScriptableObject
{
    [Header("Skill Details")]
    public SkillInformation Information = new SkillInformation();
    
    [Header("Skill Actions")]
    [Space] [SerializeReference] public List<Action> Actions = new List<Action>();

    public void Execute(CharacterCard source, CharacterCard target){
        if(Actions.Count <= 0) return; // guard clause
        
        // execute the first action
        Actions[0].Perform(Information, source, target);
        
        // if a skill contains a sequence of actions, loop through each index after 0
        if(Actions.Count <= 1) return;

        for(int i = 1;i < Actions.Count;i++){
            Actions[i].Perform(Information, source, target);
        }
    }
}

[System.Serializable]
public class SkillInformation
{
    [Header("General")]
    [Tooltip("Strength of the actions")] public int power = 0; // strength of action
    [Tooltip("Stamina cost for performing the action")] public int cost = 0; // stamina cost of action
    [Tooltip("How fast the unit will perform the actions")] [Range(0,10)] public int speed = 1; // speed of action
    [Tooltip("How long the skill will take to be ready to use again")] [Range(1,10)] public int recharge = 3; // how long until the action is ready again
    
    [Header("Typing")]
    [Tooltip("How type of damage the skill will do")] public DamageType scaling = DamageType.Physical;
    [Tooltip("Who the skills will hit")] public Targetting targetting = Targetting.Enemy;

    public SkillInformation(SkillInformation copy){
        power = copy.power;
        cost = copy.cost;
        speed = copy.speed;
        recharge = copy.recharge;
        scaling = copy.scaling;
        targetting = copy.targetting;
    }

    public SkillInformation(){
        power = 0;
        cost = 0;
        speed = 0;
        recharge = 1;
        scaling = DamageType.Physical;
        targetting = Targetting.Enemy;
    }
}

[System.Serializable]
public enum DamageType{
    Physical,
    Magic
}

[System.Serializable]
public enum Targetting{
    Enemy,
    Ally,
    All,
    AllEnemy,
    AllAlly,
    Self
}