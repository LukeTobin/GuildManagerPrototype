using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <>/saves/save_name/folder/card_type/cardName.save

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private void Awake() {
        if(!Instance) Instance = this;
    }

    public void Save<T>(T data){
        SaveData.current.Set(data);

        if(data is CardData){
            CardData _data = data as CardData;
            SerializationManager.Save("roster/" + _data.id, SaveData.current);
        }
        else if(data is Decklist){
            Decklist _data = data as Decklist;
            SerializationManager.Save("decklists/" + _data.deckName, SaveData.current);
        }
        else if(data is DungeonData){
            DungeonData _data = data as DungeonData;
            SerializationManager.Save("dungeons/" + _data.id, SaveData.current);
        }
        else if(data is GameState){
            GameState _data = data as GameState;
            SerializationManager.Save(_data.id + "_state", SaveData.current);
        }
        else if(data is RosterData){
            RosterData _data = data as RosterData;
            SerializationManager.Save("roster/roster", SaveData.current);
        }
        else{
            Debug.LogError("Tried saving data of unknown type: " + data.ToString());
        }
    }

    public string GetDirectory(GameDirectory dir){
        string directory = Application.persistentDataPath + "/saves/" + GameManager.Instance.GameSave;

        switch(dir){
            case GameDirectory.Root:
                directory = Application.persistentDataPath;
                break;
            case GameDirectory.Saves:
                directory = Application.persistentDataPath + "/saves/";
                break;
            case GameDirectory.CurrentSave:
                directory = $"{Application.persistentDataPath}/saves/{GameManager.Instance.GameSave}/";
                break;
            case GameDirectory.Decklist:
                directory = $"{Application.persistentDataPath}/saves/{GameManager.Instance.GameSave}/decklists/";
                break;
            case GameDirectory.Roster:
                directory = $"{Application.persistentDataPath}/saves/{GameManager.Instance.GameSave}/roster/";
                break;
            case GameDirectory.Dungeon:
                directory = $"{Application.persistentDataPath}/saves/{GameManager.Instance.GameSave}/dungeons/";
                break;
        }

        return directory;
    }
}

public enum GameDirectory{
    Root,
    Saves,
    CurrentSave,
    Decklist,
    Roster,
    Dungeon
}
