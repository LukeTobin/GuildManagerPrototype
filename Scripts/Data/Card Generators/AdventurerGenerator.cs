using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FreyUtils;

public static class AdventurerGenerator
{
    static string[] names = new string[]{"de", "ne", "po", "ka", "den", "van", "kep", "nen", "lu", 
                                    "lep", "pa", "even", "ku", "vat", "ze", "to", "et", "laap", 
                                    "wen", "qer", "bam", "men", "nes", "el", "uke", "ma"};

    // TODO: Potential, Quirks, Personality, Sprite
    public static AdventurerData Generate(Vector2 minMax, Vector2 range){
        // New blank adventurer
        AdventurerData data = new AdventurerData();

        // Determine the weighted average of stats
        float averageStat = ((float)(minMax.x + minMax.y) / 2) / 100;

        // assign class stats
        Stats stats = GetStats(averageStat, range);
        data.stats = stats;

        // assing class based on values
        data._class = GetClass(stats);
        
        // assign skills based on values
        data.skills = GetSkills(data);

        // Calculate the overall rating as the weighted average of stats
        int overall = Mathf.RoundToInt(((stats.recovery * data._class.coefficients.recovery) + (stats.resilience * data._class.coefficients.resilience) + (stats.toughness * data._class.coefficients.toughness) +
                                        (stats.dexterity * data._class.coefficients.dexterity) + (stats.swiftness * data._class.coefficients.swiftness) + (stats.precision * data._class.coefficients.precision) +
                                        (stats.magicka * data._class.coefficients.magicka) + (stats.physical * data._class.coefficients.physical) + (stats.power * data._class.coefficients.power) + 
                                        (stats.luck * data._class.coefficients.luck) + (stats.swiftness * data._class.coefficients.prowess) + (stats.swiftness * data._class.coefficients.mental))) / 12;
        overall = Mathf.Clamp(overall, 1, 100);

        data.stats.overall = overall;
        data.stats.health = data.stats.OVRVitality * Random.Range(100, 111);
        data.stats.stamina = Mathf.RoundToInt(data.stats.OVRAgility * 0.5f) + 100;

        data.title = GetName();
        data.currentStats = new Stats(data.stats);

        data.personal = new PersonalData();
        data.personal.age = 18;

        data.brain = new StandardCombatBrain(); // TODO
        
        data.id = $"{data.title}_{data.currentStats.overall.ToString()}_{System.DateTime.Now.ToString("ddMMfff")}";

        return data;
    }

    static string GetName(){
        string title = "";
        int cycles = Random.Range(2,4);
        for(int i = 0;i < cycles;i++){
            title += names[Random.Range(0, names.Length)];
        }
        return title;
    }

    static Stats GetStats(float averageStat, Vector2 range){
        Stats stats = new Stats();

        // Generate individual values
        int recovery = Mathf.RoundToInt((Random.Range(range.x, range.y) * averageStat + Random.Range(0f, averageStat / 2)) * 100);
        int resilience = Mathf.RoundToInt((Random.Range(range.x, range.y) * averageStat + Random.Range(0f, averageStat / 2)) * 100);
        int toughness = Mathf.RoundToInt((Random.Range(range.x, range.y) * averageStat + Random.Range(0f, averageStat / 2)) * 100);

        int OVRVitality = Mathf.RoundToInt((recovery + resilience + toughness) / 3);

        int dexterity = Mathf.RoundToInt((Random.Range(range.x, range.y) * averageStat + Random.Range(0f, averageStat / 2)) * 100);
        int swiftness = Mathf.RoundToInt((Random.Range(range.x, range.y) * averageStat + Random.Range(0f, averageStat / 2)) * 100);
        int precision = Mathf.RoundToInt((Random.Range(range.x, range.y) * averageStat + Random.Range(0f, averageStat / 2)) * 100);

        int OVRAgility = Mathf.RoundToInt((dexterity + swiftness + precision) / 3);

        int magicka = Mathf.RoundToInt((Random.Range(range.x, range.y) * averageStat + Random.Range(0f, averageStat / 2)) * 100);
        int physical = Mathf.RoundToInt((Random.Range(range.x, range.y) * averageStat + Random.Range(0f, averageStat / 2)) * 100);
        int power = Mathf.RoundToInt((Random.Range(range.x, range.y) * averageStat + Random.Range(0f, averageStat / 2)) * 100);

        int OVRProficiency = Mathf.RoundToInt((magicka + physical + power) / 3);

        int luck = Mathf.RoundToInt((Random.Range(range.x, range.y) * averageStat + Random.Range(0f, averageStat / 2)) * 100);
        int prowess = Mathf.RoundToInt((Random.Range(range.x, range.y) * averageStat + Random.Range(0f, averageStat / 2)) * 100);
        int mental = Mathf.RoundToInt((Random.Range(range.x, range.y) * averageStat + Random.Range(0f, averageStat / 2)) * 100);

        int OVRCapability = Mathf.RoundToInt((luck + prowess + mental) / 3);

        // Assign skill values
        stats.OVRVitality = OVRVitality;
        stats.recovery = recovery;
        stats.resilience = resilience;
        stats.toughness = toughness;
        stats.OVRAgility = OVRAgility;
        stats.dexterity = dexterity;
        stats.swiftness = swiftness;
        stats.precision = precision;
        stats.OVRProficiency = OVRProficiency;
        stats.magicka = magicka;
        stats.physical = physical;
        stats.power = power;
        stats.OVRCapability = OVRCapability;
        stats.luck = luck;
        stats.prowess = prowess;
        stats.mental = mental;

        return stats;
    }

    static Class GetClass(Stats stats){
        List<int> numbers = new List<int>{stats.OVRVitality, stats.OVRAgility, stats.OVRProficiency, stats.OVRCapability};
        List<int> maxIndices = new List<int>();
        int maxNumber = int.MinValue;

        for (int i = 0; i < numbers.Count; i++)
        {
            if (numbers[i] > maxNumber)
            {
                maxNumber = numbers[i];
                maxIndices.Clear();
                maxIndices.Add(i+1);
            }
            else if (numbers[i] == maxNumber)
            {
                maxIndices.Add(i+1);
            }
        }

        int index = maxIndices[Random.Range(0, maxIndices.Count)];
        List<Class> classes = new List<Class>();

        switch (index) {
            case 1:
                classes = DataLookup.Instance.vitalityClasses;
                break;
            case 2:
                classes = DataLookup.Instance.agilityClasses;
                break;
            case 3:
                int comp = Mathf.Abs(stats.magicka - stats.physical);
                // mixed damage class
                if (comp <= 10) {
                    classes = new List<Class>(DataLookup.Instance.magicClasses);
                    for (int i = 0; i < DataLookup.Instance.physicalClasses.Count; i++) {
                        classes.Add(DataLookup.Instance.physicalClasses[i]);
                    }
                }
                else if (stats.magicka > stats.physical) {
                    classes = DataLookup.Instance.magicClasses;
                }
                else {
                    classes = DataLookup.Instance.physicalClasses;
                }
                break;
            case 4:
                classes = DataLookup.Instance.capabilityClasses;
                break;
            default:
                classes = new List<Class>();
                Debug.Log("filled with no classes");
                break;
        }
        
        Class _class = classes[Random.Range(0, classes.Count)];
        return _class;
    }

    static List<Skill> GetSkills(AdventurerData data){
        List<Skill> potentialSkills = new List<Skill>();
        potentialSkills.AddRange(DataLookup.Instance.generalSkills);
        if(data == null) Debug.Log("data is null");
        if(data._class == null) Debug.Log("class is null");
        if(data._class.uniqueSkills.Count > 0) potentialSkills.AddRange(data._class.uniqueSkills);
        
        if(potentialSkills.Count < 4) {
            Debug.LogWarning($"Skill pool to small [{potentialSkills.Count}]");
            return null;
        }

        potentialSkills.Shuffle();

        List<Skill> selectedSkills = new List<Skill>();

        for(int i = 0;i < 4;i++){
            selectedSkills.Add(potentialSkills[i]);
        }
        
        return selectedSkills;
    }
}