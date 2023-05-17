using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenCurveHandler : MonoBehaviour
{
    [SerializeField] GenCurveBar[] bars = new GenCurveBar[7];
    [SerializeField] Color[] colorMatch = new Color[7];

    [Header("Refs")]
    [SerializeField] TMP_InputField genAmountInput;
    [SerializeField] Slider centerSlider;
    [SerializeField] Slider spreadSlider;
    [SerializeField] Slider peakSlider;
    [SerializeField] Button genButton;

    private Dictionary<int, int> valueToIntMap = new Dictionary<int, int> {
        { 95, 6 },
        { 85, 5 },
        { 70, 4 },
        { 55, 3 },
        { 40, 2 },
        { 25, 1 },
        { 0, 0 }
    };

    void Start(){
        for(int i = 0;i < bars.Length;i++){
            bars[i].fillImage.color = colorMatch[i];
        }

        genButton.onClick.AddListener(Generate);
    }

    public void Generate(){
        int value = 0;
        int.TryParse(genAmountInput.text, out value);

        if(value <= 0) return;

        for(int i = 0;i < bars.Length;i++){
            bars[i].slider.maxValue = value;
            bars[i].slider.value = 0;
        }
        
        for(int i = 0;i < value;i++){
            int newGen = SingleGeneration();
            int match = IndexMatch(newGen);
            bars[match].slider.value++;
        }
    }

    int SingleGeneration(){
        int[] stats = new int[15];
        
        for (int i = 0; i < 15; i++) {
            float u = UnityEngine.Random.value;
            float r = Mathf.Sqrt(-2.0f * Mathf.Log(u));
            float theta = 2.0f * Mathf.PI * UnityEngine.Random.value;
            float x = r * Mathf.Sin(theta);
            float value = centerSlider.value + spreadSlider.value * x * peakSlider.value;
            value = Mathf.Clamp(value, 1, 100);
            stats[i] = Mathf.RoundToInt(value);
        }

        float overall = 0f;
        float totalWeight = 0f;
        for (int i = 0; i < 15; i++) {
            overall += stats[i] * 1f;
            totalWeight += 1f;
        }

        overall /= totalWeight;
        int final = (int)overall;
        return final;
    }

    public int IndexMatch(int value) {
        int result = 0;
        foreach (KeyValuePair<int, int> kvp in valueToIntMap) {
            if (value >= kvp.Key && (result == 0 || value < result)) {
                result = kvp.Value;
            }
        }
        return result;
    }
}