using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionState : MonoBehaviour
{
    private bool active;

    public void Start()
    {
        active = false;
        SetObjectActive();
    }

    public void SetObjectActive() {
        gameObject.SetActive(!active);
        active = !active;
    }
}