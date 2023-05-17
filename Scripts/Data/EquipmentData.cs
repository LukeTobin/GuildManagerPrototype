using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentData : ItemData
{
    [Header("Equipment Details")]
    public int hp;
    public int speed;
    public int strength;
}

/*

Z{ +MAIN_STAT, +SUB_STAT, +SUB_STAT, +SUB_STAT, +SUB_STAT, +EFFECT } : Max Stats + Cherry Picked
S{ +MAIN_STAT, +SUB_STAT, +SUB_STAT, +SUB_STAT, +SUB_STAT, +EFFECT }
A{ +MAIN_STAT, +SUB_STAT, +SUB_STAT, +SUB_STAT, +SUB_STAT }
B{ +MAIN_STAT, +SUB_STAT, +SUB_STAT, +SUB_STAT }
C{ +MAIN_STAT, +SUB_STAT, +SUB_STAT, +/-SUB_STAT }
D{ +MAIN_STAT, +SUB_STAT, +/-SUB_STAT }
E{ +MAIN_STAT, +/-SUB_STAT }
F{ +MAIN_STAT }

*/