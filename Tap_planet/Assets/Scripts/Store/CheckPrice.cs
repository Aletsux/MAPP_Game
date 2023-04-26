using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPrice : MonoBehaviour
{
    private int price;
    public Button button;

    void Update()
    {
        if (GameController.GetStardust() < price)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
}
