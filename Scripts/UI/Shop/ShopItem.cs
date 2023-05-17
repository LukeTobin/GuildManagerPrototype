using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

[System.Serializable]
public class ShopItem : MonoBehaviour
{
    public ShopItemData shopData;

    [Header("References")]
    [SerializeField] Image itemSprite;
    [SerializeField] Sprite emptySprite;
    [Space]
    [SerializeField] TMP_Text itemNameText;
    [SerializeField] TMP_Text itemPriceText;
    [SerializeField] string emptyString = "X";
    [SerializeField] string emptyPriceString = "Out of Stock";

    [Header("Color")]
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color emptyColor = Color.gray;

    Button m_Button;
    RectTransform rectTransform;
    Image image;
    
    void Awake(){
        m_Button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        m_Button.onClick.AddListener(Purchase);
    }

    bool TryPurchase(){
        if(GameManager.Instance.game.gold < shopData.price) return false;
        else return true;
    }

    void Purchase(){
        if(!TryPurchase() || shopData.empty || shopData == null) return;

        GameManager.Instance.game.gold -= shopData.price; // change to using eventsystem & a currency manager?
        if(shopData.card != null)
            RosterManager.Instance.AddCard(shopData);

        Empty();

        // Event triggers
        EventManager.Instance.onGoldChanged.Invoke(); // UI update
        EventManager.Instance.onItemPurchased.Invoke(); // Data save step

    }

    public void Set(ShopItemData data){
        if(data == null){
            Debug.LogWarning("Tried loading an empty ShopItemData, aborting", this);
            return;
        }

        shopData = data;

        if(shopData.empty){
            Empty();
        }
        else{
            
            if(shopData.card != null) {
                if(!itemNameText) Debug.Log("missing ref");
                if(shopData == null) Debug.Log("missing data");
                if(shopData.card == null) Debug.Log("missing card");
                if(shopData.card.title == null) Debug.Log("missing title?");
                itemNameText.text = shopData.card.title == null ? "NULL NAME UUGHUHHHH" : shopData.card.title.ToString();
                itemSprite.sprite = shopData.card.sprite;
            }
            else if(shopData.pack != null){
                itemNameText.text = shopData.pack.packName;
                itemSprite.sprite = emptySprite;
            }
            else {
                Empty(); 
                return;
            }

            itemPriceText.text = "$" + shopData.price;

            if(!image) image = GetComponent<Image>();
            image.color = defaultColor;

            shopData.empty = false;
        }   
    }

    void Empty(){
        itemNameText.text = emptyString;
        itemPriceText.text = emptyPriceString;
        shopData.empty = true;
        image.color = emptyColor;
        itemSprite.sprite = emptySprite;
    }
}

[System.Serializable]
public class ShopItemData{
    public int price = 0;
    public bool empty = false;
    public CardData card = null;
    public CardPack pack = null;
}