using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatScreen : MonoBehaviour
{
    public static StatScreen Instance;

    [Header("References")]
    [SerializeField] GameObject statAnchor;
    [SerializeField] Button panelButton;
    [SerializeField] TMP_Text ovrText;
    [Space]
    [SerializeField] TMP_Text characterNameText;
    [SerializeField] TMP_Text characterClassText;
    [Space]
    [SerializeField] Transform skillTransform;
    [SerializeField] GameObject skillCardPrefab;

    [Header("Stat Boxes")]
    [SerializeField] StatBox vitality;
    [SerializeField] StatBox agility;
    [SerializeField] StatBox proficiency;
    [SerializeField] StatBox capability;

    private AdventurerData adventurer;
    private List<SkillCard> skills = new List<SkillCard>();

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        if(statAnchor.activeInHierarchy) statAnchor.SetActive(false);
        panelButton.onClick.AddListener(Close);
    }

    public void Open(AdventurerData data){
        adventurer = data;

        MakeStatPairs();
        ovrText.text = data.stats.overall.ToString();
        
        characterNameText.text = data.title;

        string classStr = data._class.ToString();
        classStr = classStr.Replace(" (Class)", "");
        characterClassText.text = classStr;
        
        SetupSkills();

        statAnchor.SetActive(true);
    }

    public void Close(){
        adventurer = null;
        statAnchor.SetActive(false);
    }

    void MakeStatPairs(){
        StatPair[] vitPairs = new StatPair[]{new StatPair("Recovery", adventurer.stats.recovery),
                                            new StatPair("Resilience", adventurer.stats.resilience),
                                            new StatPair("Toughness", adventurer.stats.toughness)};

        StatPair[] agilPairs = new StatPair[]{new StatPair("Dexterity", adventurer.stats.dexterity),
                                             new StatPair("Swiftness", adventurer.stats.swiftness),
                                            new StatPair("Precision", adventurer.stats.precision)};

        StatPair[] proPairs = new StatPair[]{new StatPair("Magicka", adventurer.stats.magicka),
                                            new StatPair("Physical", adventurer.stats.physical),
                                            new StatPair("Power", adventurer.stats.power)};
        
        StatPair[] capPairs = new StatPair[]{new StatPair("Luck", adventurer.stats.luck),
                                            new StatPair("Prowess", adventurer.stats.prowess),
                                            new StatPair("Mental", adventurer.stats.mental)};
        
        vitality.SetStats(adventurer.stats.OVRVitality, vitPairs);
        agility.SetStats(adventurer.stats.OVRAgility, agilPairs);
        proficiency.SetStats(adventurer.stats.OVRProficiency, proPairs);
        capability.SetStats(adventurer.stats.OVRCapability, capPairs);
    }

    void SetupSkills(){
        if(skills.Count > 0){
            foreach(SkillCard skill in skills){
                skill.gameObject.SetActive(false);
            }

            skills.Clear();
        }

        for(int i = 0;i < adventurer.skills.Count;i++){
            GameObject skillObject = Instantiate(skillCardPrefab);
            skillObject.transform.SetParent(skillTransform, false);
            
            SkillCard skillCard = skillObject.GetComponent<SkillCard>();
            string skillName = adventurer.skills[i].ToString();
            int power = adventurer.skills[i].Information.power;
            
            skillCard.Set(skillName, power);
            skills.Add(skillCard);
        }
    }
}

public struct StatPair{
    public string title;
    public int value;

    public StatPair(string t, int v){
        title = t;
        value = v;
    }
}
