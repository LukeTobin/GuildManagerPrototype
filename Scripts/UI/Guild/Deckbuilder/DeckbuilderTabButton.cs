using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckbuilderTabButton : MonoBehaviour
{
    public Button button {get;set;}
    public Image image {get;set;}

    void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

}
