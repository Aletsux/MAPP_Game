using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionState : MonoBehaviour
{
    private bool active;

    public void Start()
    {
        //active = false;
        setObjectInactive();
    }

    public void SetObjectActive() {
        gameObject.SetActive(true);
        //active = !active;
    }

    public void setObjectInactive()
    {
        gameObject.SetActive(false);
    }
}