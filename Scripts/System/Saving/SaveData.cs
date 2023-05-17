using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData _current;
    public static SaveData current{
        get{
            if(_current == null) _current = new SaveData();
            return _current;
        }
        set{
            if(value != null){
                _current = value;
            }
        }
    }

    public CardData CardData {get; private set;}
    public Decklist DecklistData {get; private set;}
    public DungeonData DungeonData {get; private set;}
    public GameState GameState {get; private set;}
    public RosterData RosterData {get; private set;}

    public void Load(string saveName){
        SaveData.current = (SaveData)SerializationManager.Load(SaveManager.Instance.GetDirectory(GameDirectory.CurrentSave) + saveName + ".save");
    }

    public Decklist LoadDeck(string saveName){
        SaveData.current = (SaveData)SerializationManager.Load(SaveManager.Instance.GetDirectory(GameDirectory.Decklist) + saveName + ".save");
        return SaveData.current.DecklistData;
    }

    public DungeonData LoadDungeon(string saveName){
        SaveData.current = (SaveData)SerializationManager.Load(SaveManager.Instance.GetDirectory(GameDirectory.Dungeon) + saveName + ".save");
        return SaveData.current.DungeonData;
    }

    public GameState LoadState(string saveName){
        SaveData.current = (SaveData)SerializationManager.Load(SaveManager.Instance.GetDirectory(GameDirectory.CurrentSave) + saveName + "_state.save");
        return SaveData.current.GameState;
    }

    public RosterData LoadRoster(string saveName){
        SaveData.current = (SaveData)SerializationManager.Load(SaveManager.Instance.GetDirectory(GameDirectory.Roster) + saveName + ".save");
        return SaveData.current.RosterData;
    }

    public void Set<T>(T data){
        Clean();

        if(data is CardData)
            CardData = data as CardData;
        else if(data is Decklist)
            DecklistData = data as Decklist;
        else if(data is DungeonData)
            DungeonData = data as DungeonData;
        else if(data is GameState)
            GameState = data as GameState;
        else if(data is RosterData)
            RosterData = data as RosterData;
        else
            Debug.LogError("Tried setting to data of unknown type: " + data.ToString());
    }

    public void Clean(){
        CardData = null;
        DecklistData = null;
        DungeonData = null;
        GameState = null;
        RosterData = null;
    }
}
