using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugDunGen : MonoBehaviour
{
    [SerializeField] DunGenSettings setting;

    Button m_Button;

    void Awake(){
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(CreateDungeon);
    }

    void CreateDungeon(){
        DungeonManager.Instance.CreateNewDungeon(setting);
    }
}
