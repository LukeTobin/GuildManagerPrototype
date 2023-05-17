using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RosterManager : MonoBehaviour
{
    public static RosterManager Instance;

    public RosterData rosterData;

    void Awake(){
        Instance = this;
    }

    void Start(){
        Load();
        Minimum();
    }

    public void AddCard<T>(T card, bool lateSave = false){
        if(card is AdventurerData){
            AdventurerData data = card as AdventurerData;
            Debug.Log($"Saving card [{data.title}] as Adventurer");
            rosterData.adventurers.Add(data);
        }
        else if(card is CardData){
            CardData data = card as CardData;
            Debug.Log($"Saving card [{data.title}] as Valuable");
            rosterData.valuables.Add(data);
        }
        else{
            Debug.LogWarning($"Tried saving card [{card.ToString()}], but could not match type");
            return;
        }

        if(!lateSave)
            SaveManager.Instance.Save(rosterData);
    }

    void Load(){
        if(!File.Exists(SaveManager.Instance.GetDirectory(GameDirectory.Roster) + "roster.save")){
            rosterData = new RosterData();
            SaveManager.Instance.Save(rosterData);
        }
        else{
            rosterData = SaveData.current.LoadRoster("roster");
        }
    }

    void Minimum(){
        if(rosterData.adventurers.Count >= 4) return;

        Debug.Log("Agency provided adventurers being added");

        int c = Mathf.Abs(rosterData.adventurers.Count - 4);
        for(int i = 0;i < c;i++){
            AddCard(AdventurerGenerator.Generate(new Vector2(20,30), new Vector2(0.6f, 1.2f)));
            EventManager.Instance.onNewRosterMember.Invoke();
        }
        SaveManager.Instance.Save(rosterData);
    }

    public int AverageOverall(){
        int avg = 0;
        foreach(AdventurerData data in rosterData.adventurers){
            avg += data.stats.overall;
        }
        avg /= rosterData.adventurers.Count;
        return avg;
    }
}
