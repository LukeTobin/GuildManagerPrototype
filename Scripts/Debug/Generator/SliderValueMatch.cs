using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValueMatch : MonoBehaviour
{
    Slider slider;
    [SerializeField] TMP_Text textInt;

    private void Awake() {
        slider = GetComponent<Slider>();
        textInt.text = slider.value.ToString();

        slider.onValueChanged.AddListener(UpdateText);
    }

    void UpdateText(float value){
        textInt.text = value.ToString("F1");
    }
}
