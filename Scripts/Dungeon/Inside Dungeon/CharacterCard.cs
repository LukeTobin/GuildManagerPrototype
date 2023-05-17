using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CharacterCard : MonoBehaviour
{   
    [Header("Colors")]
    [SerializeField] string friendlyHexColor = "#FFFFFF";
    [SerializeField] string enemyHexColor = "#FFFFFF"; 
    
    [SerializeField] GameObject characterContainer; //unused

    [Header("Defaults")]
    [SerializeField] Skill Pass; // a skill that does nothing and passes their turn

    [SerializeField] AdventurerData tempData;

    // Events : In Order of Operation
    [HideInInspector] public UnityEvent<AdventurerData, CardData, int> OnDamaged = new UnityEvent<AdventurerData, CardData, int>();
    [HideInInspector] public UnityEvent<AdventurerData, CardData, int> OnHealed = new UnityEvent<AdventurerData, CardData, int>();
    [HideInInspector] public UnityEvent<string> OnActionPerformed = new UnityEvent<string>();

    public AdventurerData Data {get; private set;}
    public Status Status {get; private set;}
    public Intent CombatIntent {get; private set;}

    public DungeonController Dungeon {get; set;}

    public bool IsEnemy {get;set;}

    CombatBrain brain;
    CombatCardUI cardUI;

    [HideInInspector] public List<SkillCooldown> cooldowns = new List<SkillCooldown>();

    void Awake(){
        cardUI = GetComponent<CombatCardUI>();
    }

    /// <summary>
    /// Exterior constructor for a CharacterCard
    /// </summary>
    /// <param name="_data">The data to setup the CharacterCard with</param>
    public void Create(AdventurerData _data){
        Data = _data;
        Status = new Status();

        // late assign current stats. Example case: Enemies in Dungeons
        if(Data.currentStats.overall == 0) Data.currentStats = new Stats(Data.stats);

        brain = _data.brain == null ? new CombatBrain() : _data.brain;

        cardUI.Setup(Data);
    }

    /// <summary>
    /// Populate the Intent variable with a new Combat Intent
    /// </summary>
    /// <param name="team">Unit's that are on the same team</param>
    /// <param name="enemies">Unit's that are on an enemy team</param>
    public void NewIntent(List<CharacterCard> team, List<CharacterCard> enemies){
        // combatbrain -> evaluate all skills -> return a skill
        if(brain == null) brain = new CombatBrain();

        Intent intent = brain.Evaluate(this, team, enemies);
        if(intent == null) {
            intent = new Intent(Pass, new List<CharacterCard>{this});
            cardUI.ShowIntent("pass");
        }else{
            cardUI.ShowIntent("attack");
        }

        CombatIntent = intent;
    }

    /// <summary>
    /// Mark a skill as used and put it on a cooldown passed on it's recharge time
    /// </summary>
    /// <param name="usedSkill">Skill to put on cooldown</param>
    public void SkillUsed(Skill usedSkill){
        SkillCooldown cooldown = new SkillCooldown(usedSkill, 0);
        cooldowns.Add(cooldown);
        cardUI.ShowIntent("none");
    }

    /// <summary>
    /// Decrease cooldown on each skill in the cooldown list. Triggered on a new Combat Turn.
    /// </summary>
    public void NextStep(){
        if(cooldowns.Count <= 0) return;

        for(int i = cooldowns.Count-1;i >= 0;i--){
            cooldowns[i].step++;
            if(cooldowns[i].step >= cooldowns[i].skill.Information.recharge){
                cooldowns.RemoveAt(i);
            }
        }
    }

    #region Combat Calcs

    /// <summary>
    /// Damage the Character. Use's damage scaling
    /// </summary>
    /// <param name="incomingSkill">The skill's that will damage the user</param>
    /// <param name="source">The unit that's attempting to damage this unit</param>
    public void Damage(SkillInformation incomingSkill, AdventurerData source){
        int finalDamage = incomingSkill.power;

        // scale damage
        float damage = finalDamage;
        if(incomingSkill.scaling == DamageType.Physical){
            damage = ((float)source.currentStats.physical * (float)incomingSkill.power) / (float)Data.currentStats.toughness * ((float)source.currentStats.OVRProficiency / 10f);
        }
        else if(incomingSkill.scaling == DamageType.Magic){
            damage = ((float)source.currentStats.magicka * (float)incomingSkill.power) / (float)Data.currentStats.resilience * ((float)source.currentStats.OVRProficiency / 10f);
        }

        // expected damage
        finalDamage = (int)damage;

        // player feedback
        string hexColorSource = IsEnemy == true ? enemyHexColor : friendlyHexColor;
        string hexColorTarget = IsEnemy == true ? friendlyHexColor : enemyHexColor; 

        OnActionPerformed.Invoke($"[<b><color={hexColorSource}>{source.title}</color></b>] attacking [<b><color={hexColorTarget}>{Data.title}</color></b>] for <b>{finalDamage}</b> damage.");
        
        // apply damage
        Data.currentStats.health -= finalDamage;
        Data.currentStats.health = Mathf.Clamp(Data.currentStats.health, 0, Data.stats.health);

        // notify if damage was dealt
        if(finalDamage > 0){
             OnDamaged.Invoke(Data, source, finalDamage);
        }

        // check if the unit should have died
        if(Data.currentStats.health <= 0){
            EventManager.Instance.onCharacterDied.Invoke(this);
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Heal the Character. Use's heal scaling
    /// </summary>
    /// <param name="incomingSkill">The skill's that will heal the user</param>
    /// <param name="source">The unit that's attempting to heal this unit</param>
    public void Heal(SkillInformation incomingSkill, AdventurerData source){
        float heal = incomingSkill.power;

        if(incomingSkill.scaling == DamageType.Magic){
            heal = ((float)source.currentStats.magicka * (float)incomingSkill.power) / (float)Data.stats.health * ((float)source.currentStats.OVRProficiency / 10f);
        }

        string hexColorSource = IsEnemy == true ? enemyHexColor : friendlyHexColor;

        OnActionPerformed.Invoke($"[<b><color={hexColorSource}{source.title}</color></b>] healing [<b><color={hexColorSource}{Data.title}</color></b>] for <b>{heal}</b> health.");

        Heal((int)heal);
    }

    /// <summary>
    /// Heal the unit with flat values
    /// </summary>
    /// <param name="heal">The exact value the unit will be healed for.</param>
    public void Heal(int heal){
        Data.currentStats.health += heal;
        Data.currentStats.health = Mathf.Clamp(Data.currentStats.health, 0, Data.stats.health);
        OnHealed.Invoke(Data, null, heal);
    }

    #endregion
}

public class Intent{
    public Skill skill;
    public List<CharacterCard> targets;

    public Intent(Skill k, List<CharacterCard> t){
        skill = k;
        targets = t;
    }
}

public class SkillCooldown{
    public Skill skill;
    public int step;

    public SkillCooldown(Skill k, int s){
        skill = k;
        step = s;
    }
}