using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelAnimation : MonoBehaviour
{
    protected RectTransform rectTransform;
    protected float targetHeight;
    protected float targetWidth;
    
    public float closedHeight = 100;
    public float closedWidth = 100;

    public float duration = 0.1f;
    public float delay = 0.1f;
    protected bool isActive;

    protected virtual void Awake()
    {
        isActive = false;
        rectTransform = gameObject.GetComponent<RectTransform>();
        targetHeight = rectTransform.rect.height;
        targetWidth = rectTransform.rect.width;
        rectTransform.sizeDelta = new Vector2(closedWidth, closedHeight);
    }

    public virtual void StretchPanel()
    {
        if (isActive == false)
        {
            LeanTween.size(rectTransform, new Vector2(targetWidth, closedHeight), duration)
        .setEase(LeanTweenType.easeInOutQuad)
        .setDelay(delay)
        .setOnComplete(() =>
        {
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
                
            });
        });
        }
        isActive = !isActive;
    }

    public bool IsActive()
    {
        return isActive;
    }
}