using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSetMatch : MonoBehaviour
{
    void Start()
    {
        if (Screen.width / Screen.height > .6)
            GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
    }
}