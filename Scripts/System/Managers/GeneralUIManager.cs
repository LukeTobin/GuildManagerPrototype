using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneralUIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMP_Text goldText;
    [SerializeField] TMP_Text dayText;

    static string DAY_TEXT = "Day ";
    
    void Start(){
        UpdateTextLabels();
        SubscribeEvents();
    }

    void UpdateTextLabels(){
        goldText.text = GameManager.Instance.game.gold.ToString();
        dayText.text = DAY_TEXT + GameManager.Instance.game.day.ToString();
    }

    void SubscribeEvents(){
        EventManager.Instance.onDayChanged.AddListener(UpdateTextLabels);
        EventManager.Instance.onGoldChanged.AddListener(UpdateTextLabels);
    }
}
