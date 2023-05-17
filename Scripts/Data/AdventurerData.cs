using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AdventurerData : CardData
{
    [Header("Adventurer Details")]
    public Stats currentStats;

    [SerializeReference] public List<Skill> skills = new List<Skill>();
    [SerializeReference] public List<Skill> learnable = new List<Skill>();

    public Class _class;
    public PersonalData personal = new PersonalData();
    [SerializeReference] public CombatBrain brain;

    public AdventurerData(){
        this.id = "";
        this.title = "";
        this.sprite = null;
        this.cost = 0;
        this.stats = new Stats();

        this.currentStats = new Stats(this.stats);
        this.skills = new List<Skill>();
        this.learnable = new List<Skill>();
        this._class = null;
        this.personal = new PersonalData();
        this.brain = new StandardCombatBrain();
    }

    public AdventurerData(AdventurerData copy){
        this.id = copy.id;
        this.title = copy.title;
        this.sprite = copy.sprite;
        this.cost = copy.cost;
        this.stats = copy.stats;

        this.currentStats = copy.currentStats;
        this.skills = copy.skills;
        this.learnable = copy.skills;
        this._class = copy._class;
        this.personal = copy.personal;
        this.brain = copy.brain;
    }

    public AdventurerData(string id, string title, Sprite sprite, int cost, Stats stats, Stats currentStats, List<Skill> skills, List<Skill> learnable, Class _class, PersonalData personal, CombatBrain brain)
    {
        this.id = id;
        this.title = title;
        this.sprite = sprite;
        this.cost = cost;
        this.stats = stats;
        this.currentStats = currentStats;
        this.skills = skills;
        this.learnable = learnable;
        this._class = _class;
        this.personal = personal;
        this.brain = brain;
    }
}
