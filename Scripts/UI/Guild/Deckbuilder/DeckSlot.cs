using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DeckSlot : MonoBehaviour, IDropHandler
{
    public int slotIndex = 0;
    public AdventurerData data;
    public bool populated = false;

    [Header("References")]
    [SerializeField] TMP_Text displayText;
    [SerializeField] Image backgroundImage;

    [Header("Colours")]
    [SerializeField] Color defaultColor;
    [SerializeField] Color populatedColor;

    Image defaultImage => backgroundImage;
    AdventurerCardUI storedCard;
    Button button;

    void Awake(){
        button = GetComponent<Button>();
        button.onClick.AddListener(() => Clear());
    }

    public void OnDrop(PointerEventData eventData){
        if(eventData.pointerDrag != null){
            if(storedCard != null) Clear();
            Set(eventData.pointerDrag.GetComponent<AdventurerCardUI>());
        }
    }

    public void Clear() {
        if(!populated) return;

        data = null;
        storedCard.gameObject.SetActive(true);
        storedCard.draggable.Clean();
        storedCard = null;
        backgroundImage.color = defaultColor;
        displayText.text = "+";
        populated = false;
    }

    public void Set(AdventurerCardUI card){
        if(card == null) return;
        
        storedCard = card;
        data = storedCard.data;
        storedCard.gameObject.SetActive(false);
        backgroundImage.color = populatedColor;
        displayText.text = data.title;
        populated = true;
    }
}
