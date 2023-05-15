using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsAnimation : PanelAnimation
{
    private Button openSettings;
    private GameObject closeSettings;
    private GameObject text;
    public Sprite openedSprite;
    public Sprite transitionalSprite;
    public Sprite closedSprite;
    private Image imageComponent;

    protected override void Awake()
    {
        base.Awake();
        openSettings = GetComponent<Button>();
        closeSettings = transform.GetChild(0).gameObject;
        closeSettings.SetActive(false);
        imageComponent = gameObject.GetComponent<Image>();
        imageComponent.sprite = closedSprite;
    }

    public override void StretchPanel()
    {
        if (open == false)
        {
            imageComponent.sprite = transitionalSprite;
            LeanTween.size(rectTransform, new Vector2(targetWidth, closedHeight), duration)
        .setEase(LeanTweenType.easeInOutQuad)
        .setDelay(delay)
        .setOnComplete(() =>
        {
            imageComponent.sprite = openedSprite;
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
            imageComponent.sprite = transitionalSprite;
            LeanTween.size(rectTransform, new Vector2(closedWidth, closedHeight), duration)
            .setEase(LeanTweenType.easeInOutQuad)
            .setDelay(delay)
            .setOnComplete(() =>
            {
                imageComponent.sprite = closedSprite;

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
