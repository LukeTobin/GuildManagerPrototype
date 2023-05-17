using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject shopObject;

    [Header("Debug")]
    [SerializeField] bool forceRefresh;

    public List<ShopItem> shopItems = new List<ShopItem>();

    private void Start() {
        Preload();
        if(forceRefresh && Debug.isDebugBuild) RefreshStock();
        EventManager.Instance.onDayChanged.AddListener(RefreshStock);
        EventManager.Instance.onItemPurchased.AddListener(SaveAllData);
    }

    public void Preload(){
        if(GameManager.Instance.game.shopItemDatas.Count <= 0) return;

        for(int i = 0;i < shopItems.Count;i++){
            ShopItemData data;
            if(GameManager.Instance.game.shopItemDatas.Count >= i){
                data = GameManager.Instance.game.shopItemDatas[i];

                if(data == null){
                    data = NewShopItem();
                } 
                else if(data.card.id == string.Empty && data.pack.packName == string.Empty){
                    data = NewShopItem();
                }
            }else{
                data = NewShopItem();
            }
            shopItems[i].Set(data);
        }
    }

    public ShopItemData NewShopItem(){
        ShopItemData data = new ShopItemData();
        data.empty = false;
        int index = Random.Range(0, GameManager.Instance.sellableCards.Count);
        data.card = GameManager.Instance.sellableCards[index].card;
        data.card.title = GameManager.Instance.sellableCards[index].name;
        data.pack = null;
        data.price = data.card.cost;
        return data;
    }

    public void RefreshStock(){
        if(GameManager.Instance.game.day % 7 != 0 && !forceRefresh) return;
        for(int i = 0;i < shopItems.Count;i++){
            ShopItemData data = NewShopItem();
            shopItems[i].Set(data);
        }
        NotificationController.Instance.CreateNotification("Shop Refreshed!", "New items are now available in the town shop.", () => ScreenUIManager.Instance.OpenScreenByID("shop"), false);
        SaveAllData();
    }

    public void SaveAllData(){
        for(int i = 0;i < shopItems.Count;i++){
            GameManager.Instance.game.shopItemDatas[i] = shopItems[i].shopData;
        }

        SaveManager.Instance.Save(GameManager.Instance.game);
    }
}