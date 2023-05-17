using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Decklist
{
    public string deckName;
    //public AdventurerData[] deck = new AdventurerData[4];
    public List<AdventurerData> deck = new List<AdventurerData>{null, null, null, null};

    public bool IsActive(){
        foreach(AdventurerData data in deck){
            if(data.personal == null){
                Debug.Log("no personal data");
            }
            if(!data.personal.active) {
                return false;
            }
        }
        return true;
    }

    public void Activate(bool state){
        foreach(AdventurerData data in deck){
            data.personal.active = state;
        }
        SaveManager.Instance.Save(RosterManager.Instance.rosterData);
    }
}