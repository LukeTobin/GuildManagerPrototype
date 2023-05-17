using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class StatBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    [System.Serializable]
    private struct StatText{
        public TMP_Text statText;
        public Image statImage;
        public Color statColor;
        public TMP_Text statOverallText;
    }

    [Header("Text Fields")]
    [SerializeField] TMP_Text overallRatingText;
    [Space]
    [SerializeField] GameObject subStatContainer;
    [SerializeField] StatText[] statTexts = new StatText[3];
 
    [Header("Colours")]
    [SerializeField] Color color = Color.white;

    [Header("Tween Settings")]
    [SerializeField] float tweenTime = 0.25f;
    [SerializeField] float tweenUpscale = 1.5f;
    [SerializeField] float overallRatingAlpha = 0.8f;

    bool mouseOver = false;

    RectTransform rectTransform;
    Image image;

    void Start(){
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        image.color = color;
        image.canvas.sortingOrder = 0;

        for(int i = 0;i < statTexts.Length;i++)
            statTexts[i].statImage.color = statTexts[i].statColor;

        subStatContainer.SetActive(false);

        overallRatingText.DOFade(overallRatingAlpha, 0.1f);
    }

    public void OnPointerEnter(PointerEventData eventData){
        if (!mouseOver) {
            rectTransform.DOScale(tweenUpscale, tweenTime);

            // idk why this works??? for some reason moves its rotation too 45, guessing its to do with parent?
            rectTransform.DORotate(new Vector3(0,0,0), tweenTime);
            
            overallRatingText.DOFade(0, tweenTime);

            subStatContainer.SetActive(true);
            
            image.canvas.sortingOrder = 1;
            mouseOver = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData){
        if(mouseOver) {
            rectTransform.DOScale(1f, tweenTime);
            rectTransform.DORotate(new Vector3(0,0,45), tweenTime);
            overallRatingText.DOFade(overallRatingAlpha, tweenTime);
            subStatContainer.SetActive(false);
            image.canvas.sortingOrder = 0;
            mouseOver = false;
        }
    }

    public void SetStats(int ovr, StatPair[] pairs){
        overallRatingText.text = ovr.ToString();

        for(int i = 0;i < statTexts.Length;i++){
            statTexts[i].statText.text = pairs[i].title;
            statTexts[i].statOverallText.text = pairs[i].value.ToString();
        }
    }
}