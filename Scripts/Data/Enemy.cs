using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Cards/Enemy", order = 1)]
public class Enemy : ScriptableObject
{
    public AdventurerData data = new AdventurerData();
}
