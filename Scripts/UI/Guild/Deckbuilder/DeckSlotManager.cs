using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DeckSlotManager : MonoBehaviour
{
    [Header("Deck Slots")]
    public DeckSlot[] deckSlots = new DeckSlot[4];

    [Header("References")]
    [SerializeField] RosterSetup roster = null;
    [SerializeField] DeckSwapperUI swapper;

    [HideInInspector] public string lastDeckListPath = string.Empty;
    Decklist loadedDecklist = null;

    // could maybe be refactored later
    public void LoadLastDecklist(){
        if(lastDeckListPath != string.Empty){
            ConvertAndAssignDecklist(lastDeckListPath);
            return;
        }

        if(swapper.GetCurrentDeckPath() != string.Empty)
            ConvertAndAssignDecklist(swapper.GetCurrentDeckPath());
    }

    public void ConvertAndAssignDecklist(string path){
        SaveData.current = (SaveData)SerializationManager.Load(path);
        Decklist loadDeck = CopyDecklist(SaveData.current.DecklistData);
        AssignToDeckSlots(loadDeck);
        lastDeckListPath = path;
    }

    void AssignToDeckSlots(Decklist list){
        ClearAllSlots();

        for(int i = 0;i < deckSlots.Length;i++){
            if(list.deck[i] != null && list.deck[i].id != string.Empty)
                deckSlots[i].Set(roster.GetCard(list.deck[i]));
        }
        
        loadedDecklist = list;
    }

    public void ClearAllSlots(){
        foreach(DeckSlot slot in deckSlots) 
            slot.Clear();
    }

    List<Decklist> GetDecklists(){
        List<Decklist> decklist = new List<Decklist>();
        string[] decklistPath = Directory.GetFiles(SaveManager.Instance.GetDirectory(GameDirectory.Decklist));
        for (int i = 0; i < decklistPath.Length; i++)
        {
            SaveData.current = (SaveData)SerializationManager.Load(decklistPath[i]);
            if(SaveData.current.DecklistData != null) decklist.Add(CopyDecklist(SaveData.current.DecklistData));
        }
        return decklist;
    }

    Decklist CopyDecklist(Decklist copy){
        Decklist newList = new Decklist();

        if(copy == null){
            Debug.LogWarning("Tried to copy an empty decklist, aborting...", this);
            return newList;
        }

        newList.deckName = copy.deckName;
        newList.deck = new List<AdventurerData>(copy.deck);
        return newList;
    }
}
