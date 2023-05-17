using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData
{
    public string id;

    [Header("Card Details")]
    public string title;
    public Sprite sprite;
    public int cost;

    [Space]
    public Stats stats;
}