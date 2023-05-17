using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckbuilderTabMenu : MonoBehaviour
{
    [System.Serializable]
    public struct Tab{
        public DeckbuilderTabButton button;
        public GameObject tabScreen;
    }

    [SerializeField] List<Tab> tabs = new List<Tab>();

    [Header("Color")]
    [SerializeField] Color unselected = Color.white;
    [SerializeField] Color selected = Color.blue;

    void Start(){
        for(int i = 0;i < tabs.Count;i++){
            Tab tab = tabs[i];
            tabs[i].button.button.onClick.AddListener(() => OpenTab(tab));
            if(i == 0){
                tabs[i].button.image.color = selected;
                tabs[i].tabScreen.SetActive(true);
            }else{
                tabs[i].button.image.color = unselected;
                tabs[i].tabScreen.SetActive(false);
            }
        }

        
    }

    public void OpenTab(Tab tab){
        for(int i = 0;i < tabs.Count;i++){
            if(tabs[i].button != tab.button){
                tabs[i].button.image.color = unselected;
                tabs[i].tabScreen.SetActive(false);
            }
        }

        tab.button.image.color = selected;
        tab.tabScreen.SetActive(true);
    }
}