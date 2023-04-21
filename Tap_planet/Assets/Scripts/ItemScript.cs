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
    public String desciption;

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

    public String ReturnDescription()
    {
        return desciption;
    }
}
