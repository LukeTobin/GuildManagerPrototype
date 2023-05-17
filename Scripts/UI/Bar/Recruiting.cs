using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recruiting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject recruitCardPrefab;
    [SerializeField] Transform recruitStorage;

    [Header("Settings")]
    [SerializeField][Range(0, 5)] int recruitsDaily = 2;

    List<RecruitCard> storedRecruits = new List<RecruitCard>();

    void Start()
    {
        LoadSaved();
        Subscribe();
    }

    void LoadSaved(){
        List<AdventurerData> barList = GameManager.Instance.game.barList;
        if(barList == null) return;
        
        for(int i = 0;i < barList.Count;i++){
            Create(barList[i], false);
        }
    }

    public void Create(AdventurerData data, bool addToSave, int customMult = 5000){
        GameObject recruitObject = Instantiate(recruitCardPrefab);
        recruitObject.transform.SetParent(recruitStorage, false);
        RecruitCard recruitCard = recruitObject.GetComponent<RecruitCard>();
        recruitCard.Setup(data, this, customMult);

        if(addToSave){
            GameManager.Instance.game.barList.Add(data);
            SaveManager.Instance.Save(GameManager.Instance.game);
        }

        storedRecruits.Add(recruitCard);
    }

    public void Remove(RecruitCard card){
        if(card == null){
            Debug.LogWarning("Tried removing RecruitCard that was null. Aborted.", this);
            return;
        }

        if(storedRecruits.Contains(card)){
            storedRecruits.Remove(card);
        }

        GameManager.Instance.game.barList.Remove(card.Data);
        SaveManager.Instance.Save(GameManager.Instance.game);

        card.gameObject.SetActive(false);
    }

    void NewRecruits(){
        int min = 0;
        int max = recruitsDaily+1;

        if(storedRecruits.Count <= 0) min = 1;
        
        if(storedRecruits.Count > 6) max /= 2;
        else if(storedRecruits.Count > 12) max /= 3;

        if(storedRecruits.Count >= 8) Remove(storedRecruits[Random.Range(0, storedRecruits.Count)]);

        int r = Random.Range(min, max);
        int average = RosterManager.Instance.AverageOverall();
        for(int i = 0;i < r;i++){
            int minOvr = Mathf.Abs(average - Random.Range(1, 15));
            Mathf.Clamp(minOvr, 1, 85);
            Create(AdventurerGenerator.Generate(new Vector2(minOvr,average), new Vector2(0.5f, 1.5f)), true);
        }
    }

    void Subscribe() => EventManager.Instance.onDayChanged.AddListener(NewRecruits);
}