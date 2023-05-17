using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DeleteSavedDeck : MonoBehaviour
{
    [SerializeField] DeckSwapperUI swapper;
    [SerializeField] DeckSlotManager slotManager;

    Button m_Button;

    private void Awake() {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(() => DeleteCurrentDeck());
    }

    // needs warning added in future
    public void DeleteCurrentDeck(){
        string path = swapper.GetCurrentDeckPath();

        if(File.Exists(path)){
            File.Delete(path);
            slotManager.ClearAllSlots();
            swapper.UpdateList(true);
        }
    }
}
