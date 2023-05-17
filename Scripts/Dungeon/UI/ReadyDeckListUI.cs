using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadyDeckListUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform deckLayoutGroup;
    [SerializeField] GameObject deckPrefab;

    string[] saveFiles;
    List<AdventurerDeckUI> decklistObjects = new List<AdventurerDeckUI>();

    void LoadDecklists(){
        if(!Directory.Exists(SaveManager.Instance.GetDirectory(GameDirectory.Decklist))){
            Directory.CreateDirectory(SaveManager.Instance.GetDirectory(GameDirectory.Decklist));
        }

        saveFiles = Directory.GetFiles(SaveManager.Instance.GetDirectory(GameDirectory.Decklist));
    }

    public void DisplayDecklists(){
        LoadDecklists();

        if(decklistObjects.Count > 0){
            for(int j = 0;j < decklistObjects.Count;j++) decklistObjects[j].gameObject.SetActive(false);
            decklistObjects.Clear();
        }

        if(saveFiles == null || saveFiles.Length <= 0) return;
        
        for (int i = 0; i < saveFiles.Length; i++)
        {
            string fileName = saveFiles[i].Replace(SaveManager.Instance.GetDirectory(GameDirectory.Decklist), "").Replace(".save", "");
            Decklist loadedList = SaveData.current.LoadDeck(fileName);
            if(loadedList.IsActive()) continue;

            GameObject deckObject = Instantiate(deckPrefab);
            deckObject.transform.SetParent(deckLayoutGroup, false);

            AdventurerDeckUI deck = deckObject.GetComponent<AdventurerDeckUI>();
            
            deck.CreateDeckContainer(loadedList);
            decklistObjects.Add(deck);
        }
    }

}
