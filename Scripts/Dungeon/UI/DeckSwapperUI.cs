using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeckSwapperUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] DeckSlotManager slotManager;

    TMP_Dropdown dropdown;
    List<string> storedDecklists = new List<string>();

    private void Awake() {
        dropdown = GetComponent<TMP_Dropdown>();    
        dropdown.onValueChanged.AddListener(delegate { ValueChange(); });
    }

    public void UpdateList(bool reconstruct = false){
        if(!Directory.Exists(SaveManager.Instance.GetDirectory(GameDirectory.Decklist))){
            Directory.CreateDirectory(SaveManager.Instance.GetDirectory(GameDirectory.Decklist));
        }

        int n = 0;

        List<string> decklist = new List<string>(Directory.GetFiles(SaveManager.Instance.GetDirectory(GameDirectory.Decklist)));
        // store decklist path's
        if(storedDecklists.Count <= 0 || reconstruct) storedDecklists = new List<string>(decklist);
        else{
            for(int j = 0;j < decklist.Count;j++){
                if(!storedDecklists.Contains(decklist[j])){
                    storedDecklists.Add(decklist[j]);
                    //slotManager.lastDeckListPath = decklist[j];
                }
            }
        }

        for (int i = 0; i < decklist.Count; i++)
        {
            if(decklist[i] == slotManager.lastDeckListPath) n = i;
            decklist[i] = decklist[i].Replace(SaveManager.Instance.GetDirectory(GameDirectory.Decklist), "");
            decklist[i] = decklist[i].Replace(".save", "");
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(decklist);
        dropdown.value = n;
    }

    public void ValueChange(){
        slotManager.ConvertAndAssignDecklist(storedDecklists[dropdown.value]);
    }

    public string GetCurrentDeckPath() {
        if(storedDecklists.Count <= 0) return string.Empty;
        return storedDecklists[dropdown.value];
    }
}
