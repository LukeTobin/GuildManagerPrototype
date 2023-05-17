using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DebugAdventurerBuilder : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] SaveManager save;

    [Header("Builder")]
    [SerializeField] AdventurerData adventurer = null;
    
    [Space]
    [SerializeField] bool dontCopyStats;
    [SerializeField] bool dontMakeOVRs;

    [Button]
    void Create(){
        if(!Debug.isDebugBuild) return;

        // should be handled somewhere else
        if(!dontMakeOVRs){
            adventurer.stats.OVRVitality = (adventurer.stats.recovery + adventurer.stats.resilience + adventurer.stats.toughness) / 3;
            adventurer.stats.OVRAgility = (adventurer.stats.dexterity + adventurer.stats.swiftness + adventurer.stats.precision) / 3;
            adventurer.stats.OVRProficiency = (adventurer.stats.magicka + adventurer.stats.physical + adventurer.stats.power) / 3;
            adventurer.stats.OVRCapability = (adventurer.stats.luck + adventurer.stats.prowess + adventurer.stats.mental) / 3;
            adventurer.stats.overall = (adventurer.stats.OVRVitality + adventurer.stats.OVRAgility + adventurer.stats.OVRProficiency + adventurer.stats.OVRCapability) / 4;
            adventurer.stats.health = adventurer.stats.OVRVitality * 100;
        }

        if(!dontCopyStats)
            adventurer.currentStats = new Stats(adventurer.stats);

        save.Save(adventurer);
    }


    [Header("Loader")]
    public string loadName;
    [Space]
    public AdventurerData loaded = null;

    [Button]
    void Load(){
        SaveData.current.Load("roster/" + loadName);
        if(SaveData.current.CardData != null){
            loaded = (AdventurerData)SaveData.current.CardData;
        }
    }

    [Button]
    void Test(){
        loaded._class = Resources.Load<Class>("Classes/" + adventurer._class.name);
    }
}
