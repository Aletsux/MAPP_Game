using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePanel : MonoBehaviour
{
    public bool active;

    void Awake()
    {
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
