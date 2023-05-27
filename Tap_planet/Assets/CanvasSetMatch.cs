using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSetMatch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float height = Screen.height; // skapar varibel och lagrar canvasens höjd
        float width = Screen.width;
        
        print(width);
        print(height);
        if (width/height > .6)
            GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
    }

    // 1080/1920 = ,56



    // ipad 5 
}