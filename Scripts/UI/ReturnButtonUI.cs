using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnButtonUI : MonoBehaviour
{
    public ScreenUI TargetScreen {get;set;}

    public Button ReturnButton {get;private set;}

    void Awake(){
        ReturnButton = GetComponent<Button>();
    }

    void Update()
    {
        if(gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape)){
            ReturnButton.onClick.Invoke();
        }
    }

    public void SetupReturnButton(ScreenUI target, bool state){
        TargetScreen = target;
        gameObject.SetActive(state);
    }
}
