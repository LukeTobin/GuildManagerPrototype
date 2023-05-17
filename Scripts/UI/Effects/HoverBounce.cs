using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HoverBounce : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    float tweenTime = 0.1f;
    float hoverSize = 1.2f;
    float tweenBounceSize = 1.15f;

    RectTransform rectTransform;
    Vector3 defaultScale;
    bool mouseOver = false;
    Sequence sequence;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        defaultScale = rectTransform.localScale;
    }

    private void Start() {
        ResetSeq();
    }

    void ResetSeq(){
        sequence = DOTween.Sequence();
        sequence.Append(rectTransform.DOScale(hoverSize, tweenTime));
        sequence.Append(rectTransform.DOScale(tweenBounceSize, tweenTime));
        sequence.Append(rectTransform.DOScale(hoverSize, tweenTime));
        sequence.SetAutoKill(false); // prevent sequence from being killed automatically
        sequence.Pause(); // pause the sequence initially
    }

    public void OnPointerEnter(PointerEventData eventData){
        if (!mouseOver) {
            sequence.Restart();
            mouseOver = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData){
        if(mouseOver) {
            //sequence.Rewind();
            sequence.Pause();
            rectTransform.DOScale(1f, tweenTime);
            mouseOver = false;
        }
    }

    private void OnDisable() {
        sequence.Pause();
        rectTransform.localScale = defaultScale;
        mouseOver = false;
    }
}
