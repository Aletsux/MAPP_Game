using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsAnimation : PanelAnimation
{
    private Button openSettings;
    private GameObject closeSettings;
    private GameObject text;

    protected override void Awake()
    {
        base.Awake();
        openSettings = GetComponent<Button>();
        closeSettings = transform.GetChild(0).gameObject;
        closeSettings.SetActive(false);
        text = transform.GetChild(1).gameObject;
    }

    public override void StretchPanel()
    {
        if (open == false)
        {
            LeanTween.size(rectTransform, new Vector2(targetWidth, closedHeight), duration)
        .setEase(LeanTweenType.easeInOutQuad)
        .setDelay(delay)
        .setOnComplete(() =>
        {
            text.SetActive(open);

            LeanTween.size(rectTransform, new Vector2(targetWidth, targetHeight), duration)
            .setEase(LeanTweenType.easeInOutQuad)
            .setDelay(delay)
            .setOnComplete(() =>
            {
                SetButtonsActive();
                
            });
        });

        }
        else
        {
            SetButtonsActive();

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

                text.SetActive(!open);

            });
        });
        }
        open = !open;
    }

    private void SetButtonsActive()
    {
        if (openSettings.interactable == false)
        {
            openSettings.interactable = true;
            closeSettings.SetActive(false);
        }
        else
        {
            openSettings.interactable = false;
            closeSettings.SetActive(true);
        }
    }
}
