using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelAnimation : MonoBehaviour
{
    private RectTransform rectTransform;
    private float targetHeight;
    private float targetWidth;
    
    public float closedHeight = 100;
    public float closedWidth = 100;

    private float duration = 0.1f;
    private float delay = 0.1f;
    private bool isActive;

    public bool hasText;
    private GameObject text;

    //public PanelAnimation(GameObject g, float height, float width)
    //{
    //    rectTransform = g.GetComponent<RectTransform>();
    //    targetHeight = height;
    //    targetWidth = width;
    //    rectTransform.sizeDelta = new Vector2(closedWidth, closedHeight);
    //    isActive = false;
    //}

    void Start()
    {
        isActive = false;
        rectTransform = gameObject.GetComponent<RectTransform>();
        targetHeight = rectTransform.rect.height;
        targetWidth = rectTransform.rect.width;
        rectTransform.sizeDelta = new Vector2(closedWidth, closedHeight);
        if (hasText)
            text = transform.GetChild(1).gameObject;
    }

    public void StretchPanel()
    {
        if (isActive == false)
        {
            LeanTween.size(rectTransform, new Vector2(targetWidth, closedHeight), duration)
        .setEase(LeanTweenType.easeInOutQuad)
        .setDelay(delay)
        .setOnComplete(() =>
        {
            if (hasText)
                text.SetActive(isActive);
            LeanTween.size(rectTransform, new Vector2(targetWidth, targetHeight), duration)
            .setEase(LeanTweenType.easeInOutQuad)
            .setDelay(delay)
            .setOnComplete(() =>
            {

            });
        });

        }
        else
        {
            LeanTween.size(rectTransform, new Vector2(targetWidth, closedHeight), duration)
        .setEase(LeanTweenType.easeInOutQuad)
        .setDelay(delay)
        .setOnComplete(() =>
        {
            LeanTween.size(rectTransform, new Vector2(closedWidth, closedHeight), duration)
            .setEase(LeanTweenType.easeInOutQuad)
            .setDelay(delay)
            .setOnComplete(() =>
            {
                if (hasText)
                    text.SetActive(isActive);
            });
        });
        }
        isActive = !isActive;
    }

    public bool IsActive()
    {
        return isActive;
    }

    public 
}