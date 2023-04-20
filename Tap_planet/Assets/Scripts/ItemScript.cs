using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour
{
    private Image sprite ;
    public String name;
    private Text title;
    private Text desciption;

    void Start()
    {
        
    }


    public Image ReturnImage()
    {
        return sprite;
    }

    public String ReturnName()
    {
        return name;
    }
}
