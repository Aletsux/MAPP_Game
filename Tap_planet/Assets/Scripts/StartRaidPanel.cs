using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRaidPanel : MonoBehaviour
{
    void Start()
    {
        ActivatePanel(false);
    }

    public void ActivatePanel(bool toggle)
    {
        gameObject.SetActive(toggle);
    }
}
