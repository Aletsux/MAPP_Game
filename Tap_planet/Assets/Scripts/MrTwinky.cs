using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrTwinky : MonoBehaviour
{
    void Start()
    {
        ActivateTwinky();
    }

    public void ActivateTwinky()
    {
        if (PlayerPrefs.GetInt("IdleExtenderLvl") > 1)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}
