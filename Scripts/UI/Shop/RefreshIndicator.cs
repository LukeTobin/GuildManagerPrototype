using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RefreshIndicator : MonoBehaviour
{
    TMP_Text refreshText;

    private void Awake() {
        refreshText = GetComponent<TMP_Text>();    
    }

    void Start()
    {
        EventManager.Instance.onDayChanged.AddListener(UpdateRefreshText);
    }

    public void UpdateRefreshText(){
        int daysUntilRefresh = (7 - GameManager.Instance.game.day + 7) % 7; // maybe make a global settings too get the refresh days
        if(daysUntilRefresh == 0) daysUntilRefresh = 7;
        refreshText.text = $"Refresh in <b>{daysUntilRefresh}</b> days.";
    }
}
