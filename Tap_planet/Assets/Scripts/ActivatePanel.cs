using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePanel : MonoBehaviour
{
    private bool active;

    void Start()
    {
        active = false;
        Toggle(active);
    }

    public void Toggle()
    {
        gameObject.SetActive(!active);
        active = !active;
    }

    public void Toggle(bool toggle)
    {
        gameObject.SetActive(toggle);
    }
}
