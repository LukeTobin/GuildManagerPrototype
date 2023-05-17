using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecruitCard : MonoBehaviour
{
    [SerializeField] AdventurerCardUI cardUI;
    [SerializeField] Button purchaseButton;
    [SerializeField] Button removeButton;
    [SerializeField] TMP_Text purchaseCostText;

    Recruiting recruiting = null;

    public AdventurerData Data {get;private set;}

    int price = 0;

    void Start(){
        cardUI.DisableDragging();
        purchaseButton.onClick.AddListener(Purchase);
        cardUI.cardButton.onClick.AddListener(Purchase);
        removeButton.onClick.AddListener(Remove);
    }

    public void Setup(AdventurerData data, Recruiting recruitingBoard, int mult = 5000){
        cardUI.Setup(data, false, true);
        price = data.currentStats.overall * mult;
        purchaseCostText.text = $"${price}";

        recruiting = recruitingBoard;
        Data = data;
    }

    void Purchase(){
        if(Data == null || Data.title == string.Empty) return;
        if(GameManager.Instance.game.gold < price) return;

        GameManager.Instance.game.gold -= price;
        EventManager.Instance.onGoldChanged.Invoke();

        RosterManager.Instance.AddCard(Data);
        EventManager.Instance.onNewRosterMember.Invoke();

        recruiting.Remove(this);
    }

    void Remove(){
        recruiting.Remove(this);
    }
}