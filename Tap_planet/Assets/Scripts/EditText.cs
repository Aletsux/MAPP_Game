using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditText : MonoBehaviour
{

    //GameObject canvas;
    //GameObject storePanel;
    //GameObject powerUpTab;
    //GameObject scroll;
    //GameObject panel;
    public GameObject idleButton;
    public GameObject idlePurchaseButn;



    public GameObject GC;
    private GameController gameController;

    private bool buyPressed;

    private float timer = 0f;
    private float timeToShow = 1f;




    // Start is called before the first frame update
    void Start()
    {
        gameController = GC.GetComponent<GameController>();
        //FindButtonIdle();
        //FindBuyIdleButton();

    }

    // Update is called once per frame
    void Update()
    {
        ChangeTextIdle();
        ChangeWhenBought();

        if (buyPressed) // bool set to true in DisplayMessage()
        {
            timer += Time.deltaTime;
            if (timer >= timeToShow)
            {
                timer = 0f;
                buyPressed = false;
                ResetTimer();
            }
        }
    }

    //public void FindButtonIdle()
    //{
    //    canvas = GameObject.Find("Canvas");
    //    storePanel = canvas.transform.GetChild(6).gameObject;
    //    powerUpTab = storePanel.transform.GetChild(3).gameObject;
    //    scroll = powerUpTab.transform.GetChild(0).gameObject;
    //    panel = scroll.transform.GetChild(0).gameObject;
    //    button = panel.transform.GetChild(0).gameObject;
    //}

    //public void FindBuyIdleButton()
    //{
    //    canvas = GameObject.Find("Canvas");
    //    storePanel = canvas.transform.GetChild(6).gameObject;
    //    powerUpTab = storePanel.transform.GetChild(3).gameObject;
    //    scroll = powerUpTab.transform.GetChild(0).gameObject;
    //    panel = scroll.transform.GetChild(0).gameObject;
    //    button = panel.transform.GetChild(0).gameObject;
    //    purchaseButton = button.transform.GetChild(2).gameObject;
    //}

    public void ChangeTextIdle()
    {
        if (gameController.IsIdleTrue() == false && buyPressed == false)
        {
            idleButton.GetComponent<ItemScript>().desciption = "Get 1 crystal each 60 sec! Every upgrade will decrease time 15 seconds.";


        }
        else if (gameController.IsIdleTrue() && gameController.IsIdleLvlTrue() == false && buyPressed == false)
        {
            idleButton.GetComponent<ItemScript>().desciption = "You get " + (gameController.ReturnClicksPerSec() + 1) + " crystal per " + (gameController.ReturnSecBeforeClick()) + " seconds";
        }
        else if (gameController.IsIdleLvlTrue() && gameController.IsIdleTrue() && buyPressed == false)
        {
            idleButton.GetComponent<ItemScript>().desciption = "You get " + gameController.ReturnClicksPerSec() + " crystals per second and " + gameController.ReturnClickPerTime() + " every " + (gameController.ReturnSecBeforeClick()) + " seconds";
        }

    }

    public void ChangeWhenBought()
    {
        buyPressed = true;
        if (buyPressed && gameController.IsIdleTrue() == false)
        {
            idlePurchaseButn.GetComponent<ItemScript>().desciption = "You will recieve 1 crystal each minute.";
        }
        else if (buyPressed && gameController.IsIdleTrue() && gameController.IsIdleLvlTrue() == false)
        {
            if (gameController.ReturnTimesToLvlUp() == 1)
            {
                idlePurchaseButn.GetComponent<ItemScript>().desciption = "15 seconds have been subtraced, you now recieve 1 crystal each second.";
            }
            else
            {
                idlePurchaseButn.GetComponent<ItemScript>().desciption = "15 seconds have been subtraced, you now recieve 1 crystal each " + (gameController.ReturnSecBeforeClick() - 15);
            }

        }
        else if (buyPressed && gameController.IsIdleTrue() && gameController.IsIdleLvlTrue())
        {
            if (gameController.ReturnTimesToLvlUp() == 1)
            {
                idlePurchaseButn.GetComponent<ItemScript>().desciption = "15 seconds have been subtraced, you now recieve " + (gameController.ReturnClicksPerSec() + 1) + " per second.";
            }
            else
            {
                idlePurchaseButn.GetComponent<ItemScript>().desciption = "15 seconds have been subtraced, you now recieve " + gameController.ReturnClicksPerSec() + " crystal each second and 1 per " + (gameController.ReturnSecBeforeClick() - 15);
            }
        }


    }

    private void ResetTimer()
    {
        timer = 0f;
    }


}
