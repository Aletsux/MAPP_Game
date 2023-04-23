using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissedRaid : MonoBehaviour
{
    private bool active;

    void Start()
    {
        active = false;
        ActivatePanel(false);
    }

    public void ActivatePanel()
    {
        if (active)
        {
            gameObject.SetActive(!active);
            active = !active;
        }
    }

    public void ActivatePanel(bool toggle)
    {
        gameObject.SetActive(toggle);
    }
}
