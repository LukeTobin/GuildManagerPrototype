using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenUI : MonoBehaviour
{
    public UnityEvent OpenEvent;
    public UnityEvent CloseEvent;

    public void Open(){
        gameObject.SetActive(true);
        OpenEvent.Invoke();
    }

    public void Close(){
        gameObject.SetActive(false);
        CloseEvent.Invoke();
    }

    public void HardClose(){
        gameObject.SetActive(false);
    }
}
