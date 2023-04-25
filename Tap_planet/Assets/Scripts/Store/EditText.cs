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



    //public class MyClass2
    //{
    //    public static int num = 20000;
    //}
    ////test
    //public void AddStarDustTest()
    //{
    //    MyClass2 mc2 = new MyClass2();
    //    int i = MyClass2.num;
    //    GameController.AddStardust(i = MyClass2.num);
    //}



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

    public void ChangeTextIdle()// när spelaren trycker på blå knapp inte köp ska det reflektera  // fungerar fint
    {
        if (gameController.IsIdleTrue() == false && IsEngelska() == true)
        {
            idleButton.GetComponent<ItemScript>().desciption = "Get 1 crystal each 60 sec! Every upgrade will decrease time 15 seconds.";
            idleButton.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
            idleButton.GetComponent<ItemScript>().itemName = "Idle Clicker!";
        }
        else if (gameController.IsIdleTrue() && gameController.IsIdleLvlTrue() == false && IsEngelska() == true)
        {
            idleButton.GetComponent<ItemScript>().desciption = "You get " + (gameController.ReturnClicksPerSec() + 1) + " crystal per " + (gameController.ReturnSecBeforeClick()) + " seconds";
            idleButton.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
            idleButton.GetComponent<ItemScript>().itemName = "Idle Clicker!";
        }
        else if (gameController.IsIdleLvlTrue() && gameController.IsIdleTrue() && IsEngelska() == true)
        {
            idleButton.GetComponent<ItemScript>().desciption = "You get " + gameController.ReturnClicksPerSec() + " crystals per second and " + gameController.ReturnClickPerTime() + " every " + (gameController.ReturnSecBeforeClick()) + " seconds";
            idleButton.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
            idleButton.GetComponent<ItemScript>().itemName = "Idle Clicker!";
        }
        else if(gameController.IsIdleTrue() == false && IsSvenska() == true)
        {
            idleButton.GetComponent<ItemScript>().desciption = "Få 1 kristall varje sekund! Varje uppgradering tar bort 15 sekunder.";
            idleButton.GetComponent<ItemScript>().price = "pris: " + gameController.GetIdleCost() + " Kristaller.";
            idleButton.GetComponent<ItemScript>().itemName = "Automatiserade Klick!";
        }
        else if (gameController.IsIdleTrue() && gameController.IsIdleLvlTrue() == false && IsSvenska() == true)
        {
            idleButton.GetComponent<ItemScript>().desciption = "Du får " + (gameController.ReturnClicksPerSec() + 1) + " kristaller för varje " + (gameController.ReturnSecBeforeClick()) + " sekunder.";
            idleButton.GetComponent<ItemScript>().price = "pris: " + gameController.GetIdleCost() + " kristaller.";
            idleButton.GetComponent<ItemScript>().itemName = "Automatiserade Klick!";
        }
        else if (gameController.IsIdleLvlTrue() && gameController.IsIdleTrue() && IsSvenska() == true)
        {
            idleButton.GetComponent<ItemScript>().desciption = "Du får " + gameController.ReturnClicksPerSec() + " kristaller för varje sekund och " + gameController.ReturnClickPerTime() + " varje " + (gameController.ReturnSecBeforeClick()) + " sekunder.";
            idleButton.GetComponent<ItemScript>().price = "pris: " + gameController.GetIdleCost() + " kristaller.";
            idleButton.GetComponent<ItemScript>().itemName = "Automatiserade Klick!";
        }
    }

  
    public void ChangeWhenBoughtIdle()
    {
        if (GameController.GetCrystals() >= gameController.GetIdleCost())
        {
            GameController.DecreaseCrystals(gameController.GetIdleCost());
            gameController.BuyIdle();

            if(IsEngelska() == true)
            {
                idlePurchaseButn.GetComponent<ItemScript>().desciption = "You get " + gameController.ReturnClicksPerSec() + " crystals per second and " + gameController.ReturnClickPerTime() + " every " + gameController.ReturnSecBeforeClick() + " seconds.";
                idlePurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
                idlePurchaseButn.GetComponent<ItemScript>().itemName = "Idle Clicker!";
            }
            else if(IsSvenska() == true)
            {
                idlePurchaseButn.GetComponent<ItemScript>().desciption = "Du får " + gameController.ReturnClicksPerSec() + " kristaller varje sekund och " + gameController.ReturnClickPerTime() + " varje " + gameController.ReturnSecBeforeClick() + " sekunder.";
                idlePurchaseButn.GetComponent<ItemScript>().price = "pris: " + gameController.GetIdleCost() + " kristaller.";
                idlePurchaseButn.GetComponent<ItemScript>().itemName = "Automatiserade Klick!";
            }
        }
        else
        {
            if (IsEngelska() == true)
            {
                idlePurchaseButn.GetComponent<ItemScript>().desciption = "Not enough crystals.";
                idlePurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetIdleCost() + " crystals.";
                idlePurchaseButn.GetComponent<ItemScript>().itemName = "Idle Clicker!";
            }
            else if (IsSvenska() == true)
            {
                idlePurchaseButn.GetComponent<ItemScript>().desciption = "Inte nog med kristaller.";
                idlePurchaseButn.GetComponent<ItemScript>().price = "pris: " + gameController.GetIdleCost() + " kristaller.";
                idlePurchaseButn.GetComponent<ItemScript>().itemName = "Automatiserade Klick!";
            }
        }
    }

    public void ChangeTextPerm()// Your clicks will increased by 1 crystal with each purchase! Every 10 purchase will add 5.
    {
        if (IsEngelska())
        {
            permButton.GetComponent<ItemScript>().price = "Price: " + gameController.GetPermCost() + " crystals.";
            permButton.GetComponent<ItemScript>().desciption = "Your clicks will increased by 1 crystal with each purchase! Every 10 purchase will add 5.";
            permButton.GetComponent<ItemScript>().itemName = "Permanent Click Increaser!";
        }
        else if (IsSvenska())
        {
            permButton.GetComponent<ItemScript>().price = "Pris: " + gameController.GetPermCost() + " kristaller.";
            permButton.GetComponent<ItemScript>().desciption = "Du får en extra kristall för varje gång du klickar! Varje 10 köp lägger till 5.";
            permButton.GetComponent<ItemScript>().itemName = "Permanent Klick Ökare!";
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
                permPurchaseButn.GetComponent<ItemScript>().itemName = "Permanent Click Increaser!";
            }
            else if(IsSvenska())
            {
                permPurchaseButn.GetComponent<ItemScript>().desciption = "Dina klicks ger dig " + (GameController.ReturnClickIncrease()) + " kristaller!";
                permPurchaseButn.GetComponent<ItemScript>().price = "Pris: " + gameController.GetPermCost() + " kristaller.";
                permPurchaseButn.GetComponent<ItemScript>().itemName = "Permanent Klick Ökare!";
            }

            
        }
        else
        {
            if (IsEngelska())
            {
                permPurchaseButn.GetComponent<ItemScript>().desciption = "Not enough crystals.";
                permPurchaseButn.GetComponent<ItemScript>().price = "Price: " + gameController.GetPermCost() + " crystals.";
                permPurchaseButn.GetComponent<ItemScript>().itemName = "Permanent Click Increaser!";
            }
            else if (IsSvenska())
            {
                permPurchaseButn.GetComponent<ItemScript>().desciption = "Inte nog med kristaller.";
                permPurchaseButn.GetComponent<ItemScript>().price = "Pris: " + gameController.GetPermCost() + " kristaller.";
                permPurchaseButn.GetComponent<ItemScript>().itemName = "Permanent Klick Ökare!";
            }
            
        }
    }

    public void ChangeTextTemp()
    {
        if (IsEngelska())
        {
            tempButton.GetComponent<ItemScript>().desciption = "Your clicks will be boosted with " + gameController.ReturnTPUAddClicksBy() + " for " + gameController.ReturnTPUTimeBeforeReset() + " seconds.";
            tempButton.GetComponent<ItemScript>().price = "price: " + gameController.GetTpuCost() + " crystals.";
            tempButton.GetComponent<ItemScript>().itemName = "Temporary Click Booster!";
        }
        else if (IsSvenska())
        {
            tempButton.GetComponent<ItemScript>().desciption = "Varje klick får du en boost med " + gameController.ReturnTPUAddClicksBy() + " kristaller under " + gameController.ReturnTPUTimeBeforeReset() + " sekunder.";
            tempButton.GetComponent<ItemScript>().price = "pris: " + gameController.GetTpuCost() + " kristaller.";
            tempButton.GetComponent<ItemScript>().itemName = "Temporär Klickar Boost!";
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
                tempPurchaseButn.GetComponent<ItemScript>().itemName = "Temporary Click Booster!";
            }
            else if (IsSvenska())
            {
                tempPurchaseButn.GetComponent<ItemScript>().desciption = "Din tidsbaserade boost måste aktiveras. Gå tillbaka till spelet och tryck på aktiveringsknappen.";
                tempPurchaseButn.GetComponent<ItemScript>().price = "pris: " + gameController.GetTpuCost() + " kristaller.";
                tempPurchaseButn.GetComponent<ItemScript>().itemName = "Temporär Klickar Boost!";
            }
      
        }
        else
        {
            if (IsEngelska())
            {
                tempPurchaseButn.GetComponent<ItemScript>().desciption = "Not enough crystals.";
                tempPurchaseButn.GetComponent<ItemScript>().price = "price: " + gameController.GetTpuCost() + " crystals.";
                tempPurchaseButn.GetComponent<ItemScript>().itemName = "Temporary Click Booster!";
            }
            else if (IsSvenska())
            {
                tempPurchaseButn.GetComponent<ItemScript>().desciption = "Inte nog med kristaller.";
                tempPurchaseButn.GetComponent<ItemScript>().price = "pris: " + gameController.GetTpuCost() + " kristaller.";
                tempPurchaseButn.GetComponent<ItemScript>().itemName = "Temporär Klickar Boost!";
            }
        }
    }


    public void ChangeTextDust()
    {
        if (IsEngelska())
        {
            dustButton.GetComponent<ItemScript>().desciption = "Bigger chance to find stardust when mining.";
            dustButton.GetComponent<ItemScript>().price = "price: " + GetDustCost() + " stardust";
            dustButton.GetComponent<ItemScript>().itemName = "Stardust Miner!";

        }
        else if (IsSvenska())
        {
            dustButton.GetComponent<ItemScript>().desciption = "större chans att finna stjärnpuder när du klickar!.";
            dustButton.GetComponent<ItemScript>().price = "pris: " + GetDustCost() + " stjärnpuder";
            dustButton.GetComponent<ItemScript>().itemName = "Sjärnpuder Grävare!";
        }
    }

    public void ChangeWhenBoughtDust()
    {

        if (GameController.GetStardustMinerLevel() == 20)
        {
            if (IsEngelska())
            {
                dustPurchaseButn.GetComponent<ItemScript>().desciption = "No more upgrades!";
                dustPurchaseButn.GetComponent<ItemScript>().price = "price: X";
                dustPurchaseButn.GetComponent<ItemScript>().itemName = "Stardust Miner!";
            }
            else if (IsSvenska())
            {
                dustPurchaseButn.GetComponent<ItemScript>().desciption = "Inga mer uppgraderingar!";
                dustPurchaseButn.GetComponent<ItemScript>().price = "pris: X";
                dustPurchaseButn.GetComponent<ItemScript>().itemName = "Stjärnpuder Grävare!";
            }
 
        }
        else if (GameController.GetStardust() >= GetDustCost())
        {
            gameController.IncreaseStardustMinerLevel();
            GameController.DecreaseStardust(GetDustCost());

            dustCost = (GameController.GetStardustMinerLevel() == 0) ? 250 : GameController.GetStardustMinerLevel() * 100;

            if (IsEngelska())
            {
                dustPurchaseButn.GetComponent<ItemScript>().desciption = GameController.GetStardustMinerLevel() + "% chance to find stardust!";
                dustPurchaseButn.GetComponent<ItemScript>().price = "price: " + GetDustCost() + " stardust";
                dustPurchaseButn.GetComponent<ItemScript>().itemName = "Stardust Miner!";
            }
            else if (IsSvenska())
            {
                dustPurchaseButn.GetComponent<ItemScript>().desciption = GameController.GetStardustMinerLevel() + "% större chans att finna stjärnpuder!";
                dustPurchaseButn.GetComponent<ItemScript>().price = "pris: " + GetDustCost() + " stjärnpuder";
                dustPurchaseButn.GetComponent<ItemScript>().itemName = "Stjärnpuder Grävare!";
            }


        }
        else
        {
            if (IsEngelska())
            {
                dustPurchaseButn.GetComponent<ItemScript>().desciption = "Not Enough Stardust!";
                dustPurchaseButn.GetComponent<ItemScript>().price = "price: " + GetDustCost() + " stardust";
                dustPurchaseButn.GetComponent<ItemScript>().itemName = "Stardust Miner!";
            }
            else if (IsSvenska())
            {
                dustPurchaseButn.GetComponent<ItemScript>().desciption = "Inte nog med stjärnpuder!";
                dustPurchaseButn.GetComponent<ItemScript>().price = "pris: " + GetDustCost() + " stjärnpuder";
                dustPurchaseButn.GetComponent<ItemScript>().itemName = "Stjärnpuder Grävare!";
            }
        }

        //dustCost = (GameController.GetStardustMinerLevel() == 0) ? 250 : GameController.GetStardustMinerLevel() * 100;
    }
    

    public int GetDustCost()
    {
        return dustCost;
    }






    


}
