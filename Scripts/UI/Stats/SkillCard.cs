using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillCard : MonoBehaviour
{
    [SerializeField] TMP_Text skillName;
    [SerializeField] TMP_Text skillPower;

    public void Set(string _name, int power){
        _name = _name.Replace(" (Skill)", "");
        skillName.text = _name;
        skillPower.text = power.ToString();
    }
}
