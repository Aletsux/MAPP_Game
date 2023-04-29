using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollSize : MonoBehaviour
{
    void Start()
    {
        // Canvas canvas = ; //hittar canvas, funkar bra eftersom det finns bara ett
        float h = FindObjectOfType<Canvas>().GetComponent<RectTransform>().rect.height - 350; // skapar varibel och lagrar canvasens höjd minus höjden på beskrivningspanelen
        RectTransform rt = gameObject.GetComponent<RectTransform>(); //skapar behållare för detta objekts rectTr
        rt.sizeDelta = new Vector2(1080, h); // sätter objektets rectTr
    }
}
