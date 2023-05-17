using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Class", menuName = "New Class", order = 1)]
[System.Serializable]
public class Class : ScriptableObject
{
    // unique skills?

    [Header("Alignment")]
    public Stats alignment = new Stats();

    [Header("Bonuses")]
    public Stats statBonus = new Stats();

    [Header("Coefficients")]
    public StatWeights coefficients = new StatWeights();

    [Header("Unique Skills")]
    public List<Skill> uniqueSkills = new List<Skill>();
}

[System.Serializable]
public class StatWeights{
    [Header("Overall")]
    public float overall = 1f;
    public float health = 1f;
    public float stamina = 1f;

    [Header("Vitality")]
    public float OVRVitality = 1f;

    public float recovery = 1f;
    public float resilience = 1f;
    public float toughness = 1f;

    [Header("Agility")]
    public float OVRAgility = 1f;

    public float dexterity = 1f;
    public float swiftness = 1f;
    public float precision = 1f;

    [Header("Proficiency")]
    public float OVRProficiency = 1f;

    public float magicka = 1f;
    public float physical = 1f;
    public float power = 1f;

    [Header("Capability")]
    public float OVRCapability = 1f;

    public float luck = 1f;
    public float prowess = 1f;
    public float mental = 1f;
}