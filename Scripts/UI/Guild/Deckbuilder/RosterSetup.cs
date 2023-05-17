using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class RosterSetup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject adventurerCardPrefab;
    [SerializeField] Transform rosterLayoutGroup;

    List<AdventurerCardUI> storedRoster = new List<AdventurerCardUI>();
    
    public void UpdateRoster(){
        RosterData roster = RosterManager.Instance.rosterData;

        // caused dup glitch
        // if(storedRoster.Count > 0){
        //     for(int j = 0;j < storedRoster.Count;j++) storedRoster[j].gameObject.SetActive(false);
        //     storedRoster.Clear();
        // }

        if(roster.adventurers == null || roster.adventurers.Count <= 0) return;
        
        for (int i = 0; i < roster.adventurers.Count; i++)
        {
            if(Stored(roster.adventurers[i])) continue;

            GameObject adventurer = Instantiate(adventurerCardPrefab);
            adventurer.transform.SetParent(rosterLayoutGroup, false);

            AdventurerCardUI card = adventurer.GetComponent<AdventurerCardUI>();
            
            card.Setup(roster.adventurers[i], true, true);
            storedRoster.Add(card);
        }
    }

    public AdventurerCardUI GetCard(AdventurerData data){
        for(int i = 0;i < storedRoster.Count;i++){
            if(storedRoster[i].data.id == data.id)
                return storedRoster[i];
        }

        Debug.LogWarning("Could not find AdventurerCardUI for " + data.title, this);
        return null;
    }

    bool Stored(AdventurerData data){
        for (int i = 0; i < storedRoster.Count; i++)
        {
            if(data == storedRoster[i].data) return true;
        }

        return false;
    }
}
