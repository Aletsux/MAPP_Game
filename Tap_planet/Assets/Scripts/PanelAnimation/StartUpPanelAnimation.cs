using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpPanelAnimation : PanelAnimation
{
    protected override void OnComplete(bool isActive)
    {
        if (!isActive)
            GameObject.FindGameObjectWithTag("twinky").GetComponent<Animator>().SetTrigger("spin");
    }
}
