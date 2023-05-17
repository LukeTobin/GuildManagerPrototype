using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowCardStats : MonoBehaviour, IPointerClickHandler
{
    AdventurerCardUI adventurerCardUI;

    void Awake(){
        adventurerCardUI = GetComponent<AdventurerCardUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            StatScreen.Instance.Open(adventurerCardUI.data);
        }
    }
}
