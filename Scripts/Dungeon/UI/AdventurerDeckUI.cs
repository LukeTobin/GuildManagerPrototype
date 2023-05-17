using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AdventurerDeckUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector3 saveStart;
    CanvasGroup canvasGroup;

    [Header("References")]
    [SerializeField] TMP_Text deckNameText;
    [SerializeField] TMP_Text ratingText;
    [SerializeField] TMP_Text adventurerNamesText;

    [HideInInspector] public Decklist decklist = new Decklist();

    void Awake(){
        canvasGroup = GetComponent<CanvasGroup>();
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
        transform.position = saveStart;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void CreateDeckContainer(Decklist _decklist){
        if(_decklist == null) {
            Debug.Log("decklist is empty");
            if(SaveData.current.DecklistData != null) {
                _decklist = SaveData.current.DecklistData;
                Debug.Log("replaced decklist with current SavaData file");
            }
            else {
                Debug.Log("no suitable replacement, destroying decklist object");
                gameObject.SetActive(false);
                return;
            }
        }

        // create a copy of the decklist
        decklist.deckName = _decklist.deckName;
        decklist.deck = new List<AdventurerData>(_decklist.deck);

        deckNameText.text = decklist.deckName;
        adventurerNamesText.text = string.Empty;

        int avgRating = 0;
        int divideBy = 0;
        for(int i = 0;i < decklist.deck.Count;i++){
            if(decklist.deck[i] != null) {
                adventurerNamesText.text += decklist.deck[i].title + "\n";
                avgRating += decklist.deck[i].stats.overall;
                divideBy++;
            }
        }

        string rating = "";
        if(avgRating == 0 || divideBy == 0) rating = "N/A";
        else rating =  Rating.Get(avgRating).ToString();
        ratingText.text = rating;
    }

    public void Reset(){
        transform.position = saveStart;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        
        decklist.Activate(false);
        SaveManager.Instance.Save(decklist);
    }
}
