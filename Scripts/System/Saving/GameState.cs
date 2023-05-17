using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

[System.Serializable]
public class GameState
{
    public string id = "";

    // Game
    public int day = 1;
    public int gold = 0;

    // Shop
    public List<ShopItemData> shopItemDatas = new List<ShopItemData>(){null,null,null,null,null,null,null,null};

    // Bar
    [OptionalField]
    public List<AdventurerData> barList = new List<AdventurerData>();

    public GameState(string saveName){
        id = saveName;
        day = 1;
        gold = 0;
        shopItemDatas = new List<ShopItemData>(){null,null,null,null,null,null,null,null};
        barList = new List<AdventurerData>();
    }
}