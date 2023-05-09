using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissedRaidPanelAnimation : PanelAnimation
{
    private Button exitButton;

    protected override void Awake()
    {
        base.Awake();
        transform.parent.gameObject.SetActive(false);
    }
    public override void StretchPanel()
    {
        if (!open)
        {
            transform.parent.gameObject.SetActive(true);
        }
        base.StretchPanel();
    }
    protected override void OnComplete(bool isActive)
    {
        transform.parent.gameObject.SetActive(isActive);
    }
}