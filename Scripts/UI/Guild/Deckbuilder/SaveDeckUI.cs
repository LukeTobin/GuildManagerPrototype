using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SaveDeckUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMP_InputField decklistName;
    [SerializeField] DeckSwapperUI swapper;
    [Space][SerializeField] List<DeckSlot> slots;

    Button m_Button;

    void Awake(){
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(() => SaveDeck());
    }

    void SaveDeck(){
        Decklist decklist = new Decklist();
        decklist.deckName = decklistName.text != string.Empty ? decklistName.text : "";
        //decklist.deckRating = 'U';
        foreach(DeckSlot slot in slots){
            decklist.deck[slot.slotIndex] = slot.data;
        }

        if(!Directory.Exists(SaveManager.Instance.GetDirectory(GameDirectory.Decklist))){
            Directory.CreateDirectory(SaveManager.Instance.GetDirectory(GameDirectory.Decklist));
        }

        SaveManager.Instance.Save(decklist);
        swapper.UpdateList();
    }
}
