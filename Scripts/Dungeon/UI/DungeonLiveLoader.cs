using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DungeonLiveLoader : MonoBehaviour
{
    public static DungeonLiveLoader Instance;

    [Header("References")]
    [SerializeField] GameObject characterCardPrefab;
    [SerializeField] Transform playerLayoutGroup;
    [SerializeField] Transform enemyLayoutGroup;
    [Space]
    [SerializeField] GameObject overviewScreen;
    [SerializeField] GameObject dungenScreen;
    [Space]
    [SerializeField] Button customReturnButton;
    [SerializeField] GameObject staticReturnButton;
    [Space]
    [SerializeField] TMP_Text lastActionText;
    [SerializeField] GameObject actionTextHistoryObject;
    [SerializeField] TMP_Text historyTextRef;

    DungeonController activeController = null;

    List<CharacterCard> characterCards = new List<CharacterCard>();
    List<TMP_Text> historyTexts = new List<TMP_Text>();

    void Awake(){
        if(!Instance) Instance = this;
    }

    void Start(){
        customReturnButton.onClick.AddListener(() => Open(false));
        EventManager.Instance.onDungeonCleared.AddListener(CloseActiveDungeon);
    }

    public void LoadLiveDunGen(DungeonController _controller){
        Deactivate();

        activeController = _controller;
        for(int i = 0;i < activeController.team.Count;i++){
            activeController.team[i].gameObject.SetActive(true);
        }

        for(int i = 0;i < activeController.enemies.Count;i++){
            activeController.enemies[i].gameObject.SetActive(true);
        }

        Open();
    }

    void Deactivate(){
        if(activeController) activeController.Visible = false;
        foreach(CharacterCard card in characterCards){
            card.gameObject.SetActive(false);
        }
    }

    public CharacterCard MakeCharacterCard(AdventurerData adventurer, DungeonController sourceController, bool friendly = true){
        GameObject cardObject = Instantiate(characterCardPrefab);
        Transform layoutGroup = friendly ? playerLayoutGroup : enemyLayoutGroup;
        cardObject.transform.SetParent(layoutGroup, false);

        CharacterCard characterCard = cardObject.GetComponent<CharacterCard>();
        characterCard.Create(adventurer);
        characterCard.Dungeon = sourceController;
        characterCards.Add(characterCard);

        if(sourceController != activeController) 
            characterCard.gameObject.SetActive(false);
        
        return characterCard;
    }

    public void WriteToActionText(string txt){
        if(lastActionText.text != string.Empty){
            // Update history
            GameObject newHistoryTextObject = Instantiate(historyTextRef.gameObject);
            newHistoryTextObject.transform.SetParent(actionTextHistoryObject.transform, false);
            newHistoryTextObject.SetActive(true);
            TMP_Text newHistoryText = newHistoryTextObject.GetComponent<TMP_Text>();

            newHistoryText.text = lastActionText.text;
            
            historyTexts.Add(newHistoryText);
            if(historyTexts.Count > 6){
                historyTexts[0].gameObject.SetActive(false);
                historyTexts.RemoveAt(0);
            } 
        }

        lastActionText.text = txt;
    }

    void Open(bool open = true){
        overviewScreen.SetActive(!open);
        dungenScreen.SetActive(open);
        staticReturnButton.SetActive(!open);

        // Clear history
        lastActionText.text = string.Empty;
        if(historyTexts.Count > 0){
            foreach(TMP_Text t in historyTexts){
                t.gameObject.SetActive(false);
            }
            historyTexts.Clear();
        }

        activeController.Visible = true;
    }

    void CloseActiveDungeon(DungeonController controller){
        Deactivate();
        Open(false);
    }
}