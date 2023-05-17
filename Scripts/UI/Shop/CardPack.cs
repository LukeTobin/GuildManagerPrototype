using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class CardPack
{
    [Header("Pack Settings")]
    public string packName;
    [Range(1,5)] public int minPackSize = 1;
    [Range(2,5)] public int maxPackSize = 4;
    [Space]
    [Range(0,5)] public int minAdventurers = 0;
}
