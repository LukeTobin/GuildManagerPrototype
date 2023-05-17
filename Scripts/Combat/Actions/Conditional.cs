using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Conditional : Action
{
    public enum StatType{
        Vitality,
        Agility,
        Proficiency,
        Capability,
        Stamina,
        Health
    }

    public enum Condition{
        Greater,
        Less
    }

    public enum CompareTo{
        Self,
        Target
    }
    
    public enum Operator{
        None,
        Multiply,
        Divide
    }

    public enum StatState {
        Current,
        Base
    }

    [Header("Conditions & Operators")]
    public CompareTo comparison;
    [Space]
    public StatType sourceType;
    public StatState sourceState;
    public Operator sourceOperator;
    [ShowIf("@this.sourceOperator == Operator.Multiply || this.sourceOperator == Operator.Divide")] public float sourceOperationValue;
    [Space]
    public Condition condition;
    [Space]
    public StatType targetType;
    public StatState targetState;
    public Operator targetOperator;
    [ShowIf("@this.targetOperator == Operator.Multiply || this.targetOperator == Operator.Divide")]
    public float targetOperationValue;

    [Header("Results")]
    [SerializeReference] public List<Action> trueActions = new List<Action>();
    [SerializeReference] public List<Action> falseActions = new List<Action>();
    
    public override void Perform(SkillInformation info, CharacterCard source, CharacterCard target)
    {
        bool conditions = ReadConditions(info, source, target);

        List<Action> actionList;
        
        if(conditions) actionList = trueActions;
        else actionList = falseActions;

        for(int i = 0;i < actionList.Count;i++){
            actionList[i].Perform(info, source, target);
        }
    }

    bool ReadConditions(SkillInformation info, CharacterCard source, CharacterCard target){
        bool state = false;
        
        // check comparison target
        CharacterCard conditionTarget = null;
        switch(comparison){
            case CompareTo.Self:
                conditionTarget = source;
                break;
            case CompareTo.Target:
                conditionTarget = target;
                break;
        }

        // get values
        int sourceValue = GetStatTypeValue(source, sourceType, sourceState);
        int targetValue = GetStatTypeValue(conditionTarget, targetType, targetState);

        // apply operator
        if(sourceOperator != Operator.None) sourceValue = (sourceOperator == Operator.Multiply) ? Mathf.RoundToInt(sourceValue * sourceOperationValue) : Mathf.RoundToInt(sourceValue / sourceOperationValue);
        if(targetOperator != Operator.None) targetValue = (targetOperator == Operator.Multiply) ? Mathf.RoundToInt(targetValue * targetOperationValue) : Mathf.RoundToInt(targetValue / targetOperationValue);

        // condition check
        state = (condition == Condition.Greater) ? sourceValue > targetValue : sourceValue < targetValue;

        return state;
    }

    int GetStatTypeValue(CharacterCard c, StatType t, StatState s){
        int value = -1;

        switch(t){
            case StatType.Vitality:
                if(s == StatState.Current)
                    value = c.Data.currentStats.OVRVitality;
                else
                    value = c.Data.stats.OVRVitality;
                break;
            
            case StatType.Agility:
                if(s == StatState.Current)
                    value = c.Data.currentStats.OVRAgility;
                else
                    value = c.Data.stats.OVRAgility;
                break;

            case StatType.Proficiency:
                if(s == StatState.Current)
                    value = c.Data.currentStats.OVRProficiency;
                else
                    value = c.Data.stats.OVRProficiency;
                break;

            case StatType.Capability:
                if(s == StatState.Current)
                    value = c.Data.currentStats.OVRCapability;
                else
                    value = c.Data.stats.OVRCapability;
                break;

            case StatType.Stamina:
                if(s == StatState.Current)
                    value = c.Data.currentStats.stamina;
                else
                    value = c.Data.stats.stamina;
                break;

            case StatType.Health:
                if(s == StatState.Current)
                    value = c.Data.currentStats.health;
                else
                    value = c.Data.stats.health;
                break;

            default:
                Debug.LogWarning($"Could not find a corresponding value for StatType[{s.ToString()}]");
                break;
        }

        return value;
    }
}