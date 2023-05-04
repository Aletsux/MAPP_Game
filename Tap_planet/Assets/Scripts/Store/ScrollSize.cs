using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollSize : MonoBehaviour
{
    void Start()
    {
<<<<<<< HEAD
        Canvas canvas = FindObjectOfType<Canvas>();
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        float h = canvas.GetComponent<RectTransform>().rect.height - 350;
        rt.sizeDelta = new Vector2(1080, h);
=======
        float h = FindObjectOfType<Canvas>().GetComponent<RectTransform>().rect.height - 350; // skapar varibel och lagrar canvasens höjd minus höjden på beskrivningspanelen
        RectTransform rt = gameObject.GetComponent<RectTransform>(); //skapar behållare för detta objekts rectTr
        rt.sizeDelta = new Vector2(1080, h); // sätter objektets rectTr
>>>>>>> testMain2
    }
}
