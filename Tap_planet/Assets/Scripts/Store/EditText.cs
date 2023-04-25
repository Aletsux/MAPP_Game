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

    private int dustCost = 250;



    // ta in localization

    

    private bool IsEngelska()
    {
        int ID = PlayerPrefs.GetInt("LanguageKey", 0); // 0 är engelska och 1 är svenska // engelska ska vara false och svenska true


        if(ID == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsSvenska()
    {
        int ID = PlayerPrefs.GetInt("LanguageKey", 0); // 0 är engelska och 1 är svenska // engelska ska vara false och svenska true


        if (ID == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }




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
        if (gameController.IsIdleTrue() == false && IsEngelska() == true)
        {
            idleButton.GetComponent<ItemScript>().desciption = "Get 1 crystal each 60 sec! Every upgrade will decrease time 15 seconds.";
            idleButton.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
        }
        else if (gameController.IsIdleTrue() && gameController.IsIdleLvlTrue() == false && IsEngelska() == true)
        {
            idleButton.GetComponent<ItemScript>().desciption = "You get " + (gameController.ReturnClicksPerSec() + 1) + " crystal per " + (gameController.ReturnSecBeforeClick()) + " seconds";
            idleButton.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
        }
        else if (gameController.IsIdleLvlTrue() && gameController.IsIdleTrue() && IsEngelska() == true)
        {
            idleButton.GetComponent<ItemScript>().desciption = "You get " + gameController.ReturnClicksPerSec() + " crystals per second and " + gameController.ReturnClickPerTime() + " every " + (gameController.ReturnSecBeforeClick()) + " seconds";
            idleButton.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
        }
        else if(gameController.IsIdleTrue() == false && IsSvenska() == true)
        {
            idleButton.GetComponent<ItemScript>().desciption = "Få 1 kristall varje sekund! Varje uppgradering tar bort 15 sekunder.";
            idleButton.GetComponent<ItemScript>().price = "pris: " + gameController.GetIdleCost() + " Kristaller.";
        }
        else if (gameController.IsIdleTrue() && gameController.IsIdleLvlTrue() == false && IsSvenska() == true)
        {
            idleButton.GetComponent<ItemScript>().desciption = "Du får " + (gameController.ReturnClicksPerSec() + 1) + " kristaller för varje " + (gameController.ReturnSecBeforeClick()) + " sekunder.";
            idleButton.GetComponent<ItemScript>().price = "pris: " + gameController.GetIdleCost() + " kristaller.";
        }
        else if (gameController.IsIdleLvlTrue() && gameController.IsIdleTrue() && IsSvenska() == true)
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

            if(IsEngelska() == true)
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
            else if(IsSvenska() == true)
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
            if (IsEngelska() == true)
            {
                idlePurchaseButn.GetComponent<ItemScript>().desciption = "Not enough crystals.";
                idlePurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
            }
            else if (IsSvenska() == true)
            {
                idlePurchaseButn.GetComponent<ItemScript>().desciption = "Inte nog med kristaller.";
                idlePurchaseButn.GetComponent<ItemScript>().price = "pris: " + gameController.GetIdleCost() + " kristaller.";
            }
        }
    }

    public void ChangeTextPerm()// Your clicks will increased by 1 crystal with each purchase! Every 10 purchase will add 5.
    {
        if (IsEngelska())
        {
            permButton.GetComponent<ItemScript>().price = "Price: " + gameController.GetPermCost() + " crystals.";
            permButton.GetComponent<ItemScript>().desciption = "Your clicks will increased by 1 crystal with each purchase! Every 10 purchase will add 5.";
        }
        else if (IsSvenska())
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

            if (IsEngelska())
            {
                permPurchaseButn.GetComponent<ItemScript>().desciption = "Your clicks will now give you " + (GameController.ReturnClickIncrease()) + " crystals!";
                permPurchaseButn.GetComponent<ItemScript>().price = "Price: " + gameController.GetPermCost() + " crystals.";
            }
            else if(IsSvenska())
            {
                permPurchaseButn.GetComponent<ItemScript>().desciption = "Dina klicks ger dig " + (GameController.ReturnClickIncrease()) + " kristaller!";
                permPurchaseButn.GetComponent<ItemScript>().price = "Pris: " + gameController.GetPermCost() + " kristaller.";
            }

            
        }
        else
        {
            if (IsEngelska())
            {
                permPurchaseButn.GetComponent<ItemScript>().desciption = "Not enough crystals.";
                permPurchaseButn.GetComponent<ItemScript>().price = "Price: " + gameController.GetPermCost() + " crystals.";
            }
            else if (IsSvenska())
            {
                permPurchaseButn.GetComponent<ItemScript>().desciption = "Inte nog med kristaller.";
                permPurchaseButn.GetComponent<ItemScript>().price = "Pris: " + gameController.GetPermCost() + " kristaller.";
            }
            
        }
    }

    public void ChangeTextTemp()
    {
        if (IsEngelska())
        {
            tempButton.GetComponent<ItemScript>().desciption = "Your clicks will be boosted with " + gameController.ReturnTPUAddClicksBy() + " for " + gameController.ReturnTPUTimeBeforeReset() + " seconds.";
            tempButton.GetComponent<ItemScript>().price = "price: " + gameController.GetTpuCost() + " crystals.";
        }
        else if (IsSvenska())
        {
            tempButton.GetComponent<ItemScript>().desciption = "Varje klick får du en boost med " + gameController.ReturnTPUAddClicksBy() + " kristaller under " + gameController.ReturnTPUTimeBeforeReset() + " sekunder.";
            tempButton.GetComponent<ItemScript>().price = "pris: " + gameController.GetTpuCost() + " kristaller.";
        }
        
    }

    public void ChangeWhenBoughtTemp()
    {
        //buyPressed = true;
        if (GameController.GetCrystals() >= gameController.GetTpuCost())
        {
            GameController.DecreaseCrystals(gameController.GetTpuCost()); // reduces money in bank
            gameController.AddTPUAmount(); // adds 1 to tpuAmount

            if (IsEngelska())
            {
                tempPurchaseButn.GetComponent<ItemScript>().desciption = "Your time boost will have to be activated. Go back into the game and press the activation button.";
                tempPurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetTpuCost() + " crystals.";
            }
            else if (IsSvenska())
            {
                tempPurchaseButn.GetComponent<ItemScript>().desciption = "Din tidsbaserade boost måste aktiveras. Gå tillbaka till spelet och tryck på aktiveringsknappen.";
                tempPurchaseButn.GetComponent<ItemScript>().price = "pris: " + gameController.GetTpuCost() + " kristaller.";
            }
      
        }
        else
        {
            if (IsEngelska())
            {
                tempPurchaseButn.GetComponent<ItemScript>().desciption = "Not enough crystals.";
                tempPurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetTpuCost() + " crystals.";
            }
            else if (IsSvenska())
            {
                tempPurchaseButn.GetComponent<ItemScript>().desciption = "Inte nog med kristaller.";
                tempPurchaseButn.GetComponent<ItemScript>().price = "pris: " + gameController.GetTpuCost() + " kristaller.";
            }
        }
    }


    public void ChangeTextDust()
    {
        if (IsEngelska())
        {
            dustButton.GetComponent<ItemScript>().desciption = "Bigger chance to find stardust when mining.";
            dustButton.GetComponent<ItemScript>().price = "price: " + GetDustCost();

        }
        else if (IsSvenska())
        {
            dustButton.GetComponent<ItemScript>().desciption = "större chans att finna stjärnpuder när du klickar!.";
            dustButton.GetComponent<ItemScript>().price = "pris: " + GetDustCost();
        }
    }

    public void ChangeWhenBoughtDust()
    {
        dustCost = (GameController.GetStardustMinerLevel() == 0) ? 250 : GameController.GetStardustMinerLevel() * 100;
        

        if (GameController.GetStardustMinerLevel() == 20)
        {
            if (IsEngelska())
            {
                dustPurchaseButn.GetComponent<ItemScript>().desciption = "No more upgrades!";
                dustPurchaseButn.GetComponent<ItemScript>().price = "price: X";
            }
            else if (IsSvenska())
            {
                dustPurchaseButn.GetComponent<ItemScript>().desciption = "Inga mer uppgraderingar!";
                dustPurchaseButn.GetComponent<ItemScript>().price = "pris: X";
            }
 
        }
        else if (GameController.GetStardust() >= dustCost)
        {
            gameController.IncreaseStardustMinerLevel();
            GameController.DecreaseStardust(dustCost);

            if (IsEngelska())
            {
                dustPurchaseButn.GetComponent<ItemScript>().desciption = GameController.GetStardustMinerLevel() + "% chance to find stardust!";
                dustPurchaseButn.GetComponent<ItemScript>().price = "price: " + dustCost + " crystals";
            }
            else if (IsSvenska())
            {
                dustPurchaseButn.GetComponent<ItemScript>().desciption = GameController.GetStardustMinerLevel() + "% större chans att finna stjärnpuder!";
                dustPurchaseButn.GetComponent<ItemScript>().price = "pris: " + dustCost + " kristaller";
            }

        }
        else
        {
            if (IsEngelska())
            {
                dustPurchaseButn.GetComponent<ItemScript>().desciption = "Not Enough Stardust!";
                dustPurchaseButn.GetComponent<ItemScript>().price = "price: " + dustCost + " crystals";
            }
            else if (IsSvenska())
            {
                dustPurchaseButn.GetComponent<ItemScript>().desciption = "Inte nog med stjärnpuder!";
                dustPurchaseButn.GetComponent<ItemScript>().price = "pris: " + dustCost + " kristaller";
            }
        }
    }
    

    public int GetDustCost()
    {
        return dustCost;
    }


}
