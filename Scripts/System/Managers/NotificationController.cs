using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Michsky.MUIP;

public class NotificationController : MonoBehaviour
{
    public static NotificationController Instance;

    [Header("References")]
    [SerializeField] GameObject defaultNotificationPrefab;
    [SerializeField] GameObject rewardNotificationPrefab;
    [SerializeField] Transform notificationRoot;

    void Awake(){
        Instance = this;
    }

    public void CreateNotification(string title, string description, UnityAction call, bool reward){
        GameObject _notification = reward ? Instantiate(rewardNotificationPrefab) : Instantiate(defaultNotificationPrefab);
        _notification.transform.SetParent(notificationRoot, false);
        NotificationManager notification = _notification.GetComponent<NotificationManager>();

        notification.title = title;
        notification.description = description;
        if(call != null) notification.button.onClick.AddListener(call);
        notification.UpdateUI();
        
        notification.Open();
    }
}