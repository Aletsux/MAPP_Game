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
    public GameObject dustButton;
    public GameObject dustPurchaseButn;
    [Space]
    public GameObject GC;
    private GameController gameController;
    

 
   




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

  

 
    

    public void ChangeTextIdle()// när spelaren trycker på blå knapp inte köp ska det reflektera
    {
        if (gameController.IsIdleTrue() == false )
        {
            idleButton.GetComponent<ItemScript>().desciption = "Get 1 crystal each 60 sec! Every upgrade will decrease time 15 seconds.";
            idleButton.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
        }
        else if (gameController.IsIdleTrue() && gameController.IsIdleLvlTrue() == false )
        {
            idleButton.GetComponent<ItemScript>().desciption = "You get " + (gameController.ReturnClicksPerSec() + 1) + " crystal per " + (gameController.ReturnSecBeforeClick()) + " seconds";
            idleButton.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
        }
        else if (gameController.IsIdleLvlTrue() && gameController.IsIdleTrue() )
        {
            idleButton.GetComponent<ItemScript>().desciption = "You get " + gameController.ReturnClicksPerSec() + " crystals per second and " + gameController.ReturnClickPerTime() + " every " + (gameController.ReturnSecBeforeClick()) + " seconds";
            idleButton.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
        }
        
    }

    public void ChangeWhenBoughtIdle()// när spelaren köper ska den veta hur mycket de får nu och vad det nya priset är
    {
        //buyPressed = true;

        if (gameController.GetCrystals() >= gameController.GetIdleCost()) {

            gameController.DecreaseCrystals(gameController.GetIdleCost());
            gameController.BuyIdle();

            if (gameController.IsIdleTrue() && gameController.IsIdleLvlTrue() == false)
            {
                if (gameController.ReturnTimesToLvlUp() == 1)
                {
                    idlePurchaseButn.GetComponent<ItemScript>().desciption = "15 seconds have been subtraced, you now recieve 1 crystal each second and 1 every 60 seconds.";
                    idlePurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
                }
                else
                {
                    idlePurchaseButn.GetComponent<ItemScript>().desciption = "15 seconds have been subtraced, you now recieve 1 crystal each " + gameController.ReturnSecBeforeClick() + " seconds.";
                    idlePurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
                }

            }
            else if (gameController.IsIdleTrue() && gameController.IsIdleLvlTrue())
            {
                if (gameController.ReturnTimesToLvlUp() == 1)
                {
                    idlePurchaseButn.GetComponent<ItemScript>().desciption = "15 seconds have been subtraced, you now recieve " + gameController.ReturnClicksPerSec() + " per second and 1 per 60 seconds.";
                    idlePurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
                }
                else
                {
                    idlePurchaseButn.GetComponent<ItemScript>().desciption = "15 seconds have been subtraced, you now recieve " + gameController.ReturnClicksPerSec() + " crystal each second and 1 per " + gameController.ReturnSecBeforeClick() + " seconds.";
                    idlePurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
                }
            }
        }
        else
        {
            idlePurchaseButn.GetComponent<ItemScript>().desciption = "Not enough crystals.";
            idlePurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
        }

        

    }

    public void ChangeTextPerm()//
    {
        permButton.GetComponent<ItemScript>().price = "Price: " + gameController.GetPermCost() + " crystals.";
    }

    public void ChangeWhenBoughtPerm()
    {
        //buyPressed = true;
        
        if (gameController.GetCrystals() >= gameController.GetPermCost())
        {
            gameController.DecreaseCrystals(gameController.GetPermCost());
            gameController.ClickIncrease();

            permPurchaseButn.GetComponent<ItemScript>().desciption = "Your clicks will now give you " + (gameController.ReturnClickIncrease()) + " crystals!";
            permPurchaseButn.GetComponent<ItemScript>().price = "Price: " + gameController.GetPermCost() + " crystals.";
        }
        else
        {
            permPurchaseButn.GetComponent<ItemScript>().desciption = "Not enough crystals.";
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
        //buyPressed = true;
        if (gameController.GetCrystals() >= gameController.GetTpuCost())
        {
            gameController.DecreaseCrystals(gameController.GetTpuCost()); // reduces money in bank
            gameController.AddTPUAmount(); // adds 1 to tpuAmount

            tempPurchaseButn.GetComponent<ItemScript>().desciption = "Your time boost will have to be activated. Go back into the game and press the activation button.";
            tempPurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetTpuCost() + " crystals.";


        }
        else
        {
            tempPurchaseButn.GetComponent<ItemScript>().desciption = "Not enough crystals.";
            tempPurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetTpuCost() + " crystals.";
        }
            

    }

    public void ChangeTextDust()
    {

    }

    public void ChangeWhenBoughtDust()
    {

    }

    

}
