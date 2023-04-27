using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollSize : MonoBehaviour
{
    void Start()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        float h = canvas.GetComponent<RectTransform>().rect.height - 350;
        rt.sizeDelta = new Vector2(1080, h);
    }
}
