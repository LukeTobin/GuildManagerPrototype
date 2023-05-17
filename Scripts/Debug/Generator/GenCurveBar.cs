using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenCurveBar : MonoBehaviour
{
    public Slider slider;
    [SerializeField] TMP_Text barCounter;
    public Image fillImage;

    private void Awake() {
        slider.onValueChanged.AddListener(UpdateCounter);
    }

    void UpdateCounter(float value){
        int c = (int)value;
        barCounter.text = c.ToString();
    }
}
