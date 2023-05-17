using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUIManager : MonoBehaviour
{
    public static ScreenUIManager Instance;

    [System.Serializable]
    private struct ScreenButton{
        public string id;
        public Button button;
        public ScreenUI screen;
        public bool createReturnButton;

        public ScreenButton(string _id, Button btn, ScreenUI scn, bool state){
            id = _id; button = btn; screen = scn; createReturnButton = state;
        }
    }

    [Header("Defauls")]
    [SerializeField] ScreenUI activeScreen;
    [SerializeField] ReturnButtonUI returnButton;
    [SerializeField] bool displayReturnOnStart;

    [Space]
    [SerializeField] List<ScreenButton> screenButtons;

    ScreenButton returnSettings;

    void Awake(){
        Instance = this;
    }

    void Start() {
        PreregisterScreenButtons();
        SetupReturnButton();
    }

    void PreregisterScreenButtons(){
        foreach(ScreenButton sb in screenButtons){
            if(sb.button) sb.button.onClick.AddListener(() => ActivateScreen(sb));
            if(sb.screen != activeScreen) sb.screen.HardClose();
        }
    }

    void SetupReturnButton(){
        returnButton.SetupReturnButton(activeScreen, displayReturnOnStart);
        returnSettings = new ScreenButton("return", returnButton.ReturnButton, returnButton.TargetScreen, false);
        returnSettings.button.onClick.AddListener(() => ActivateScreen(returnSettings));
    }

    void ActivateScreen(ScreenButton screenButton){
        if(activeScreen) activeScreen.Close();

        returnButton.SetupReturnButton(activeScreen, screenButton.createReturnButton);

        screenButton.screen.Open();
        activeScreen = screenButton.screen;
    }

    public void OpenScreenByID(string id){
        for(int i = 0;i < screenButtons.Count;i++){
            if(id == screenButtons[i].id){
                screenButtons[i].button.onClick.Invoke();
                return;
            }
        }
    }
}
