using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Cards/Premade", order = 1)]
public class PremadeCard : ScriptableObject
{
    public CardData card = null;
}