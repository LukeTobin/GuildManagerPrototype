using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    AdventurerCardUI adventurerCardUI;
    Vector3 saveStart;
    CanvasGroup canvasGroup;

    void Awake(){
        canvasGroup = GetComponent<CanvasGroup>();
        adventurerCardUI = GetComponent<AdventurerCardUI>();
    }

    public void OnBeginDrag(PointerEventData eventData){
        saveStart = transform.position;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData){
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData){
        Clean();
    }

    public void Clean(){
        transform.position = saveStart;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
