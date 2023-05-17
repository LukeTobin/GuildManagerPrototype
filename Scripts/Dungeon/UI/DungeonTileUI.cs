using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class DungeonTileUI : MonoBehaviour, IDropHandler
{   
    [Header("References")]
    [SerializeField] GameObject blockPanel;
    [SerializeField] Button progressButton;
    [SerializeField] TMP_Text progressText;

    AdventurerDeckUI storedDeckUI;

    public DungeonController Controller {get;private set;} 
    public DungeonData ActiveDungeon {get;private set;}

    private void Awake() {
        progressButton.onClick.AddListener(() => OpenDungeon());
        EventManager.Instance.onDungeonProgressed.AddListener(UpdateProgressText);
    }

    public void OnDrop(PointerEventData eventData){
        if(eventData.pointerDrag != null){
            storedDeckUI = eventData.pointerDrag.GetComponent<AdventurerDeckUI>();
            storedDeckUI.decklist.Activate(true);
            SaveManager.Instance.Save(storedDeckUI.decklist);
            storedDeckUI.gameObject.SetActive(false);

            blockPanel.SetActive(false);            
            progressButton.gameObject.SetActive(true);
            Controller.Load(storedDeckUI.decklist);

            EventManager.Instance.onDungeonCleared.AddListener(Unload);
        }
    }

    public void Create(DungeonController controller){
        Controller = controller;
        ActiveDungeon = controller.data;

        if(ActiveDungeon != null){
            UpdateProgressText();
        }
        else{
            Debug.Log("Missing DungeonData");
            return;
        }

        if(controller.data.decklist != null){
            blockPanel.SetActive(false);            
            progressButton.gameObject.SetActive(true);
            Controller.Load(controller.data.decklist);

            EventManager.Instance.onDungeonCleared.AddListener(Unload);
        }    
    }

    public void OpenDungeon(){
        if(ActiveDungeon == null) return;
        DungeonLiveLoader.Instance.LoadLiveDunGen(Controller);
    }

    void Unload(DungeonController controller){
        if(controller != Controller) return;

        if(storedDeckUI){
            storedDeckUI.Reset();
            storedDeckUI.gameObject.SetActive(true);
        }

        gameObject.SetActive(false);
    }

    void UpdateProgressText(){
        int percentProgress = (int)(((float)ActiveDungeon.progress / (float)ActiveDungeon.layout.Count) * 100);
        progressText.text = $"In Progress\n({percentProgress}%)";
    }
}
