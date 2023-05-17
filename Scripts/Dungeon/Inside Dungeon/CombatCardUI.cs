using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatCardUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] Slider healthSlider;
    [SerializeField] TMP_Text characterName;
    [SerializeField] Image characterImage;
    [SerializeField] TMP_Text healthText;

    [Header("Intent UI")]
    [SerializeField] Image intentImage;
    [Space]
    [SerializeField] Sprite noIntent;
    [SerializeField] Sprite atkIntent;
    [SerializeField] Sprite passIntent;
    
    CharacterCard characterCard;

    Dictionary<string, Sprite> intentTable = new Dictionary<string, Sprite>();

    void Awake(){
        // populate hash table
        intentTable.Add("none", noIntent);
        intentTable.Add("attack", atkIntent);
        intentTable.Add("pass", passIntent);
    }

    void Start(){
        characterCard = GetComponent<CharacterCard>();
        Subscribe(); // subscribe to damage events to update UI
    }

    /// <summary>
    /// Populates the UI for a CharacterCard
    /// </summary>
    /// <param name="data">Data the UI is based on</param>
    public void Setup(AdventurerData data){
        characterName.text = data.title;
        if(data.sprite) characterImage.sprite = data.sprite;

        healthSlider.minValue = 1;
        healthSlider.maxValue = data.stats.health;
        healthSlider.value = data.currentStats.health;
        healthText.text = $"{data.currentStats.health} / {data.stats.health}";

        intentImage.sprite = noIntent;
    }

    /// <summary>
    /// Display the action intended to be carried out by a CharacterCard
    /// </summary>
    /// <param name="key">The key corresponding to the intended action. 
    /// "none": no action, 
    /// "pass": pass action, 
    /// "attack": attack action</param>
    public void ShowIntent(string key){
        Sprite spr = null;
        intentTable.TryGetValue(key, out spr);

        if(spr) intentImage.sprite = spr;
    }

    void Subscribe(){
        characterCard.OnDamaged.AddListener(UpdateHealth);
        characterCard.OnHealed.AddListener(UpdateHealth);
    }

    void UpdateHealth(AdventurerData data, CardData _data, int dmg){
        healthSlider.value = data.currentStats.health;
        healthText.text = $"{data.currentStats.health} / {data.stats.health}";
    }
}
