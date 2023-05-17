using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPurchaseUI : MonoBehaviour
{
    [SerializeField] List<PremadeCard> starterPack = new List<PremadeCard>();
    Button m_Button;

    void Awake(){
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(() => FakePurchase());
    }

    void FakePurchase(){
        if(!Directory.Exists(Application.persistentDataPath + "/saves/roster/")){
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/roster/");
        }

        for(int i = 0;i < starterPack.Count;i++){
            //SaveManager.Instance.Save(starterPack[i].adventurer);
        }
    }
}
