using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugForceBarGen : MonoBehaviour
{
    Button button;

    [SerializeField] Recruiting  recruiting;    

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Generate);
    }

    void Generate(){
        AdventurerData data = AdventurerGenerator.Generate(new Vector2(GameManager.Instance.min, GameManager.Instance.max), new Vector2(0.5f, 1.5f));
        recruiting.Create(data, true, 0);
    }
}
