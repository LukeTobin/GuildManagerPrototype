using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellJunk : MonoBehaviour
{
    Button m_Button;
    TMP_Text m_Text;

    void Start(){
        m_Text = GetComponentInChildren<TMP_Text>();
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(Sell);

        Refresh();
    }

    public void Refresh(){
        m_Text.text = $"Sell Junk [{RosterManager.Instance.rosterData.valuables.Count.ToString()}]";
    }

    void Sell(){
        int value = 0;
        for(int i = 0;i < RosterManager.Instance.rosterData.valuables.Count;i++){
            float cost = (float)RosterManager.Instance.rosterData.valuables[i].cost * 0.8f;
            value += (int)cost;
        }
        GameManager.Instance.game.gold += value;
        RosterManager.Instance.rosterData.valuables.Clear();
        SaveManager.Instance.Save(RosterManager.Instance.rosterData.valuables);

        EventManager.Instance.onGoldChanged.Invoke();

        Refresh();
    }
}