using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    [Header("Overall")]
    public int overall;
    public int health;
    public int stamina;

    [Header("Vitality")]
    public int OVRVitality; // overall health : how tanky the unit is

    public int recovery; // how quickly they recover hp inside & outside of battle, slightly affecting stamina
    public int resilience; // how defensive they are against magic and status
    public int toughness; // how defensive they are against physical and sickness

    [Header("Agility")]
    public int OVRAgility; // Speed main stat, effects max stamina

    public int dexterity; // accuracy & evasion
    public int swiftness; // how quickly they can use/perform skills
    public int precision; // accuracy & how likely the unit is to hit for critical damage

    [Header("Proficiency")]
    public int OVRProficiency; // Main stat for power of attacks

    public int magicka; // output of their magical attacks
    public int physical; // output of their physical attacks
    public int power; // output of critical attacks

    [Header("Capability")]
    public int OVRCapability; // unknown? potential for learning new skills? adaptability
    
    public int luck; // increases quality and quantity of rewards found in dungeons 
    public int prowess; // units intelligence in combat, dictates how and when they use specific skills
    public int mental; // how defensive they are against mental effects

    public Stats(){
        overall = 0;
        health = 0;
        stamina = 0;
        OVRVitality = 0;
        recovery = 0;
        resilience = 0;
        toughness = 0;
        OVRAgility = 0;
        dexterity = 0;
        swiftness = 0;
        precision = 0;
        OVRProficiency = 0;
        magicka = 0;
        physical = 0;
        power = 0;
        OVRCapability = 0;
        luck = 0;
        prowess = 0;
        mental = 0;
    }

    public Stats(Stats copy){
        overall = copy.overall;
        health = copy.health;
        stamina = copy.stamina;
        OVRVitality = copy.OVRVitality;
        recovery = copy.recovery;
        resilience = copy.resilience;
        toughness = copy.toughness;
        OVRAgility = copy.OVRAgility;
        dexterity = copy.dexterity;
        swiftness = copy.swiftness;
        precision = copy.precision;
        OVRProficiency = copy.OVRProficiency;
        magicka = copy.magicka;
        physical = copy.physical;
        power = copy.power;
        OVRCapability = copy.OVRCapability;
        luck = copy.luck;
        prowess = copy.prowess;
        mental = copy.mental;
    }
}