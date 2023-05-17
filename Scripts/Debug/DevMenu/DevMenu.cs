using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.MUIP;

public class DevMenu : MonoBehaviour
{
    public static DevMenu Instance;

    [Header("Notifactions")]
    [SerializeField] GameObject notificationPrefab;
    [SerializeField] NotificationStacking stacking;

    bool open = false;
    
    private void Awake() {
        Instance = this;
    }

    void Start(){
        gameObject.SetActive(false);
        open = false;
    }

    public void Open(){
        open = !open;
        gameObject.SetActive(open);
    }

    public void NextDay(){
        GameManager.Instance.NextDay();
    }

    public void AddGold(){
        GameManager.Instance.AddGold(1000);
    }

    public void ShowTestNotification(){
        GameObject _notification = Instantiate(notificationPrefab);
        _notification.transform.SetParent(stacking.gameObject.transform, false);
        NotificationManager notification = _notification.GetComponent<NotificationManager>();

        notification.title = "NEW TEST";
        notification.description = "DESCRIPTION SET FROM CODE";
        notification.UpdateUI();
        
        notification.Open();
    }
}
