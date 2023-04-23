using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditText : MonoBehaviour
{
    public GameObject idleButton;
    public GameObject idlePurchaseButn;
    [Space]
    public GameObject permButton;
    public GameObject permPurchaseButn;
    [Space]
    public GameObject tempButton;
    public GameObject tempPurchaseButn;
    [Space]
    public GameObject GC;
    private GameController gameController;
    

    private bool buyPressed = false;
    private float timer = 0f;
    private float timeToShow = 1f;
    private int count = 0;

    private int costPerm;




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
        //ChangeTextIdle();
        //ChangeWhenBoughtIdle();

        //ChangeTextPerm();
        //ChangeWhenBoughtPerm();

        //ChangeTextTemp();
        //ChangeWhenBoughtTemp();



        if (buyPressed) 
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

  

    private void ResetTimer()
    {
        timer = 0f;
    }

    

    public void ChangeTextIdle()// när spelaren trycker på blå knapp inte köp ska det reflektera
    {
        if (gameController.IsIdleTrue() == false && buyPressed == false)
        {
            idleButton.GetComponent<ItemScript>().desciption = "Get 1 crystal each 60 sec! Every upgrade will decrease time 15 seconds.";
            idleButton.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
        }
        else if (gameController.IsIdleTrue() && gameController.IsIdleLvlTrue() == false && buyPressed == false)
        {
            idleButton.GetComponent<ItemScript>().desciption = "You get " + (gameController.ReturnClicksPerSec() + 1) + " crystal per " + (gameController.ReturnSecBeforeClick()) + " seconds";
            idleButton.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
        }
        else if (gameController.IsIdleLvlTrue() && gameController.IsIdleTrue() && buyPressed == false)
        {
            idleButton.GetComponent<ItemScript>().desciption = "You get " + gameController.ReturnClicksPerSec() + " crystals per second and " + gameController.ReturnClickPerTime() + " every " + (gameController.ReturnSecBeforeClick()) + " seconds";
            idleButton.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
        }
        
    }

    public void ChangeWhenBoughtIdle()// när spelaren köper ska den veta hur mycket de får nu och vad det nya priset är
    {
        buyPressed = true;

        if (buyPressed && gameController.IsIdleTrue() == false)
        {
            idlePurchaseButn.GetComponent<ItemScript>().desciption = "You will recieve 1 crystal each minute.";
            idlePurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
        }
        else if (buyPressed && gameController.IsIdleTrue() && gameController.IsIdleLvlTrue() == false)
        {
            if (gameController.ReturnTimesToLvlUp() == 1)
            {
                idlePurchaseButn.GetComponent<ItemScript>().desciption = "15 seconds have been subtraced, you now recieve 1 crystal each second and 1 every 60 seconds.";
                idlePurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
            }
            else
            {
                idlePurchaseButn.GetComponent<ItemScript>().desciption = "15 seconds have been subtraced, you now recieve 1 crystal each " + (gameController.ReturnSecBeforeClick() - 15) + " seconds.";
                idlePurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
            }

        }
        else if (buyPressed && gameController.IsIdleTrue() && gameController.IsIdleLvlTrue())
        {
            if (gameController.ReturnTimesToLvlUp() == 1)
            {
                idlePurchaseButn.GetComponent<ItemScript>().desciption = "15 seconds have been subtraced, you now recieve " + (gameController.ReturnClicksPerSec() + 1) + " per second and 1 per 60 seconds.";
                idlePurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
            }
            else
            {
                idlePurchaseButn.GetComponent<ItemScript>().desciption = "15 seconds have been subtraced, you now recieve " + gameController.ReturnClicksPerSec() + " crystal each second and 1 per " + (gameController.ReturnSecBeforeClick() - 15) + " seconds.";
                idlePurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
            }
        }

    }

    public void ChangeTextPerm()//
    {
        permButton.GetComponent<ItemScript>().price = "Price: " + gameController.GetPermCost() + " crystals.";
    }

    public void ChangeWhenBoughtPerm()
    {
        buyPressed = true;
        
        if (buyPressed)
        {
            permPurchaseButn.GetComponent<ItemScript>().desciption = "Your clicks will now give you " + (gameController.ReturnClickIncrease()+1) + " crystals!";
            
            permPurchaseButn.GetComponent<ItemScript>().price = "Price: " + gameController.GetPermCost() + " crystals.";

            
        }
    }

    public void ChangeTextTemp()
    {
        tempButton.GetComponent<ItemScript>().desciption = "Your clicks will be boosted with " + gameController.ReturnTPUAddClicksBy() + " for " + gameController.ReturnTPUTimeBeforeReset() + " seconds.";
        tempButton.GetComponent<ItemScript>().price = "price: " + gameController.GetTpuCost() + " crystals.";
    }

    public void ChangeWhenBoughtTemp()
    {
        buyPressed = true;
        if (buyPressed)
        {
            tempPurchaseButn.GetComponent<ItemScript>().desciption = "Your time boost will have to be activated. Go back into the game and press the activation button.";
            tempPurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetTpuCost() + " crystals.";
        }
            

    }

    

}
