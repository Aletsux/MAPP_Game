using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissedRaidPanelAnimation : PanelAnimation
{
    public GameObject parent;

    protected override void Awake()
    {
        base.Awake();
        parent.SetActive(false);
    }
    public override void StretchPanel()
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
        .setDelay(0)
        .setOnComplete(() =>
        {
            LeanTween.size(rectTransform, new Vector2(closedWidth, closedHeight), duration)
            .setEase(LeanTweenType.easeInOutQuad)
            .setDelay(delay)
            .setOnComplete(() =>
            {
                parent.SetActive(false);
            });
        });
        }
        isActive = !isActive;
    }
}
