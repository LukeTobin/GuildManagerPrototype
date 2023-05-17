using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdventurerCardUI : MonoBehaviour
{
    [HideInInspector] public AdventurerData data;

    [Header("Text")]
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text classText;
    [SerializeField] TMP_Text ovrText;
    [SerializeField] TMP_Text rankText;

    [HideInInspector] public DraggableCard draggable;
    [HideInInspector] public ShowCardStats showCardStats;
    [HideInInspector] public Button cardButton;

    void Awake(){
        GetAddons();
    }

    public void Setup(AdventurerData _data, bool draggableCard = false, bool showStats = false){
        if(!draggable || !showCardStats || !showCardStats) GetAddons();
        
        data = _data;
        nameText.text = data.title;
        classText.text = data._class.name;
        ovrText.text = data.stats.overall.ToString();
        rankText.text = Rating.Get(data.currentStats.overall).ToString();
        draggable.enabled = draggableCard;
        showCardStats.enabled = showStats;
    }

    public void DisableDragging(){
        if(!draggable || !showCardStats) GetAddons();

        draggable.enabled = false;
    }

    void GetAddons(){
        if(!cardButton) cardButton = GetComponent<Button>();
        if(!draggable) draggable = GetComponent<DraggableCard>();
        if(!showCardStats) showCardStats = GetComponent<ShowCardStats>();
    }
}