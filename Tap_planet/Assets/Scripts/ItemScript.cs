using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ItemScript : MonoBehaviour
{
    public Sprite sprite;
    public String itemName;
    public String description;

    //public TMP_Text description;
    //public TMP_Text testText;

    void Start()
    {
        
    }


    public Sprite ReturnImage()
    {
        return sprite;
    }

    public String ReturnName()
    {
        return itemName;
    }

    //public String ReturnTMPName() {
      //  String strText = testText.text;
       // return strText;
    //}

    public String ReturnDescription()
    {
        return description;
    }
}
