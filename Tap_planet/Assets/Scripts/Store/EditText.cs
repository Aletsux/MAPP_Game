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

    








    // Start is called before the first frame update
    void Start()
    {
        gameController = GC.GetComponent<GameController>();

        
    }

    // Update is called once per frame
    void Update()
    {



    }



    public void ChangeTextIdle()// när spelaren trycker på blå knapp inte köp ska det reflektera
    {
        if (gameController.IsIdleTrue() == false && gameController.IsEngelska() == true)
        {
            idleButton.GetComponent<ItemScript>().desciption = "Get 1 crystal each 60 sec! Every upgrade will decrease time 15 seconds.";
            idleButton.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
        }
        else if (gameController.IsIdleTrue() && gameController.IsIdleLvlTrue() == false && gameController.IsEngelska() == true)
        {
            idleButton.GetComponent<ItemScript>().desciption = "You get " + (gameController.ReturnClicksPerSec() + 1) + " crystal per " + (gameController.ReturnSecBeforeClick()) + " seconds";
            idleButton.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
        }
        else if (gameController.IsIdleLvlTrue() && gameController.IsIdleTrue() && gameController.IsEngelska() == true)
        {
            idleButton.GetComponent<ItemScript>().desciption = "You get " + gameController.ReturnClicksPerSec() + " crystals per second and " + gameController.ReturnClickPerTime() + " every " + (gameController.ReturnSecBeforeClick()) + " seconds";
            idleButton.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
        }
        else if(gameController.IsIdleTrue() == false && gameController.IsSvenska() == true)
        {
            idleButton.GetComponent<ItemScript>().desciption = "Få 1 kristall varje sekund! Varje uppgradering tar bort 15 sekunder.";
            idleButton.GetComponent<ItemScript>().price = "pris: " + gameController.GetIdleCost() + " Kristaller.";
        }
        else if (gameController.IsIdleTrue() && gameController.IsIdleLvlTrue() == false && gameController.IsSvenska() == true)
        {
            idleButton.GetComponent<ItemScript>().desciption = "Du får " + (gameController.ReturnClicksPerSec() + 1) + " kristaller för varje " + (gameController.ReturnSecBeforeClick()) + " sekunder.";
            idleButton.GetComponent<ItemScript>().price = "pris: " + gameController.GetIdleCost() + " kristaller.";
        }
        else if (gameController.IsIdleLvlTrue() && gameController.IsIdleTrue() && gameController.IsSvenska() == true)
        {
            idleButton.GetComponent<ItemScript>().desciption = "Du får " + gameController.ReturnClicksPerSec() + " kristaller för varje sekund och " + gameController.ReturnClickPerTime() + " varje " + (gameController.ReturnSecBeforeClick()) + " sekunder.";
            idleButton.GetComponent<ItemScript>().price = "pris: " + gameController.GetIdleCost() + " kristaller.";
        }

    }

    public void ChangeWhenBoughtIdle()// när spelaren köper ska den veta hur mycket de får nu och vad det nya priset är
    {

        if (GameController.GetCrystals() >= gameController.GetIdleCost())
        {
            GameController.DecreaseCrystals(gameController.GetIdleCost());
            gameController.BuyIdle();

            if(gameController.IsEngelska() == true)
            {
                if (gameController.IsIdleTrue() == true && gameController.IsIdleLvlTrue() == false)
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
            else if(gameController.IsSvenska() == true)
            {
                if (gameController.IsIdleTrue() == true && gameController.IsIdleLvlTrue() == false)
                {
                    if (gameController.ReturnTimesToLvlUp() == 1)
                    {
                        idlePurchaseButn.GetComponent<ItemScript>().desciption = "15 sekunder är borttaget, du får nu 1 kristall varje sekund och 1 varje 60 sekunder.";
                        idlePurchaseButn.GetComponent<ItemScript>().price = "pris: " + gameController.GetIdleCost() + " kristaller.";
                    }
                    else
                    {
                        idlePurchaseButn.GetComponent<ItemScript>().desciption = "15 sekunder är borttaget, du får nu 1 kristall varje " + gameController.ReturnSecBeforeClick() + " sekund.";
                        idlePurchaseButn.GetComponent<ItemScript>().price = "pris: " + gameController.GetIdleCost() + " kristaller.";
                    }

                }
                else if (gameController.IsIdleTrue() && gameController.IsIdleLvlTrue())
                {
                    if (gameController.ReturnTimesToLvlUp() == 1)
                    {
                        idlePurchaseButn.GetComponent<ItemScript>().desciption = "15 sekunder är borttaget, du får nu " + gameController.ReturnClicksPerSec() + " kristall varje sekund och en varje 60 sekunder.";
                        idlePurchaseButn.GetComponent<ItemScript>().price = "pris: " + gameController.GetIdleCost() + " kristaller.";
                    }
                    else
                    {
                        idlePurchaseButn.GetComponent<ItemScript>().desciption = "15 sekunder är borttaget, du får nu " + gameController.ReturnClicksPerSec() + " kristall varje sekund och 1 varje " + gameController.ReturnSecBeforeClick() + " sekunder.";
                        idlePurchaseButn.GetComponent<ItemScript>().price = "pris: " + gameController.GetIdleCost() + " kristaller.";
                    }
                }
            }
            
            
        }
        else
        {
            if (gameController.IsEngelska() == true)
            {
                idlePurchaseButn.GetComponent<ItemScript>().desciption = "Not enough crystals.";
                idlePurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
            }
            else if (gameController.IsSvenska() == true)
            {
                idlePurchaseButn.GetComponent<ItemScript>().desciption = "Inte nog med kristaller.";
                idlePurchaseButn.GetComponent<ItemScript>().price = "pris: " + gameController.GetIdleCost() + " kristaller.";
            }
        }
    }

    public void ChangeTextPerm()// Your clicks will increased by 1 crystal with each purchase! Every 10 purchase will add 5.
    {
        if (gameController.IsEngelska())
        {
            permButton.GetComponent<ItemScript>().price = "Price: " + gameController.GetPermCost() + " crystals.";
            permButton.GetComponent<ItemScript>().desciption = "Your clicks will increased by 1 crystal with each purchase! Every 10 purchase will add 5.";
        }
        else if (gameController.IsSvenska())
        {
            permButton.GetComponent<ItemScript>().price = "Pris: " + gameController.GetPermCost() + " kristaller.";
            permButton.GetComponent<ItemScript>().desciption = "Du får en extra kristall för varje gång du klickar! Varje 10 köp lägger till 5.";
        }
       
    }

    public void ChangeWhenBoughtPerm()
    {
        //buyPressed = true;

        if (GameController.GetCrystals() >= gameController.GetPermCost())
        {
            GameController.DecreaseCrystals(gameController.GetPermCost());
            gameController.ClickIncrease();

            if (gameController.IsEngelska())
            {
                permPurchaseButn.GetComponent<ItemScript>().desciption = "Your clicks will now give you " + (GameController.ReturnClickIncrease()) + " crystals!";
                permPurchaseButn.GetComponent<ItemScript>().price = "Price: " + gameController.GetPermCost() + " crystals.";
            }
            else if(gameController.IsSvenska())
            {
                permPurchaseButn.GetComponent<ItemScript>().desciption = "Dina klicks ger dig " + (GameController.ReturnClickIncrease()) + " kristaller!";
                permPurchaseButn.GetComponent<ItemScript>().price = "Pris: " + gameController.GetPermCost() + " kristaller.";
            }

            
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
        if (GameController.GetCrystals() >= gameController.GetTpuCost())
        {
            GameController.DecreaseCrystals(gameController.GetTpuCost()); // reduces money in bank
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

    



}
