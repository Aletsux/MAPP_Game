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
    private StoreScript store;
    [Space]
    //public GameObject DS;
    //private DescriptionState descriptionState;

    private int dustCost = 50;

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


        if (ID == 0)
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
        store = GameObject.FindAnyObjectByType<StoreScript>();
        //IsEngelska();

        //descriptionState = DS.GetComponent<DescriptionState>();

        //Debug.Log("Gamegontroller" + gameController.IsIdleTrue());
        //descriptionState.SetObjectActive();
    }

    // Update is called once per frame
    void Update()
    {



    }

    private void OnEnable() // nåt fel med setActive false i kanske descState, scriptet blev false oxå och startade inte upp förrän desc gjorde det
    {
        gameController = GC.GetComponent<GameController>();
    }


    public void ChangeTextIdle()// när spelaren trycker på blå knapp inte köp ska det reflektera  // fungerar fint
    {
        //Debug.Log("GameController text" + gameController.IsIdleTrue());
        print("edittext");
        if (GameController.IsIdleTrue() == false && IsEngelska() == true)
        {
            idleButton.GetComponent<ItemScript>().description = "Get 1 crystal each 60 sec! Every upgrade will decrease time 15 seconds.";
            idleButton.GetComponent<ItemScript>().price = "price: " + store.GetPrice("idle") + " crystals.";
            idleButton.GetComponent<ItemScript>().itemName = "Idle Clicker!";
        }
        else if (GameController.IsIdleTrue() && gameController.IsIdleLvlTrue() == false && IsEngelska() == true)
        {
            idleButton.GetComponent<ItemScript>().description = "You get " + (gameController.ReturnClicksPerSec() + 1) + " crystal per " + (gameController.ReturnSecBeforeClick()) + " seconds";
            idleButton.GetComponent<ItemScript>().price = "price: " + store.GetPrice("idle") + " crystals.";
            idleButton.GetComponent<ItemScript>().itemName = "Idle Clicker!";
        }
        else if (gameController.IsIdleLvlTrue() && GameController.IsIdleTrue() && IsEngelska() == true)
        {
            idleButton.GetComponent<ItemScript>().description = "You get " + gameController.ReturnClicksPerSec() + " crystals per second and " + gameController.ReturnClickPerTime() + " every " + (gameController.ReturnSecBeforeClick()) + " seconds";
            idleButton.GetComponent<ItemScript>().price = "price: " + store.GetPrice("idle") + " crystals.";
            idleButton.GetComponent<ItemScript>().itemName = "Idle Clicker!";
        }
        else if (GameController.IsIdleTrue() == false && IsSvenska() == true)
        {
            idleButton.GetComponent<ItemScript>().description = "Få 1 kristall varje sekund! Varje uppgradering tar bort 15 sekunder.";
            idleButton.GetComponent<ItemScript>().price = "pris: " + store.GetPrice("idle") + " Kristaller.";
            idleButton.GetComponent<ItemScript>().itemName = "Automatiserade Klick!";
        }
        else if (GameController.IsIdleTrue() && gameController.IsIdleLvlTrue() == false && IsSvenska() == true)
        {
            idleButton.GetComponent<ItemScript>().description = "Du får " + (gameController.ReturnClicksPerSec() + 1) + " kristaller för varje " + (gameController.ReturnSecBeforeClick()) + " sekunder.";
            idleButton.GetComponent<ItemScript>().price = "pris: " + store.GetPrice("idle") + " kristaller.";
            idleButton.GetComponent<ItemScript>().itemName = "Automatiserade Klick!";
        }
        else if (gameController.IsIdleLvlTrue() && GameController.IsIdleTrue() && IsSvenska() == true)
        {
            idleButton.GetComponent<ItemScript>().description = "Du får " + gameController.ReturnClicksPerSec() + " kristaller för varje sekund och " + gameController.ReturnClickPerTime() + " varje " + (gameController.ReturnSecBeforeClick()) + " sekunder.";
            idleButton.GetComponent<ItemScript>().price = "pris: " + store.GetPrice("idle") + " kristaller.";
            idleButton.GetComponent<ItemScript>().itemName = "Automatiserade Klick!";
        }
    }

    public void ChangeWhenBoughtIdle()
    {
        if (IsEngelska() == true)
        {
            idlePurchaseButn.GetComponent<ItemScript>().description = "You get " + gameController.ReturnClicksPerSec() + " crystals per second and " + gameController.ReturnClickPerTime() + " every " + gameController.ReturnSecBeforeClick() + " seconds.";
            idlePurchaseButn.GetComponent<ItemScript>().price = "price: " + store.GetPrice("idle") + " crystals.";
            idlePurchaseButn.GetComponent<ItemScript>().itemName = "Idle Clicker!";
        }
        else if (IsSvenska() == true)
        {
            idlePurchaseButn.GetComponent<ItemScript>().description = "Du får " + gameController.ReturnClicksPerSec() + " kristaller varje sekund och " + gameController.ReturnClickPerTime() + " varje " + gameController.ReturnSecBeforeClick() + " sekunder.";
            idlePurchaseButn.GetComponent<ItemScript>().price = "pris: " + store.GetPrice("idle") + " kristaller.";
            idlePurchaseButn.GetComponent<ItemScript>().itemName = "Automatiserade Klick!";
        }
    }

    public void ChangeTextPerm()// Your clicks will increased by 1 crystal with each purchase! Every 10 purchase will add 5.
    {
        if (IsEngelska())
        {
            permButton.GetComponent<ItemScript>().price = "Price: " + store.GetPrice("perm") + " crystals.";
            permButton.GetComponent<ItemScript>().description = "Your clicks will increased by 1 crystal with each purchase! Every 10 purchase will add 5.";
            permButton.GetComponent<ItemScript>().itemName = "Permanent Click Increaser!";
        }
        else if (IsSvenska())
        {
            permButton.GetComponent<ItemScript>().price = "Pris: " + store.GetPrice("perm") + " kristaller.";
            permButton.GetComponent<ItemScript>().description = "Du får en extra kristall för varje gång du klickar! Varje 10 köp lägger till 5.";
            permButton.GetComponent<ItemScript>().itemName = "Permanent Klick Ökare!";
        }

    }

    public void ChangeWhenBoughtPerm()
    {
        if (IsEngelska())
        {
            permPurchaseButn.GetComponent<ItemScript>().description = "Your clicks will now give you " + (GameController.ReturnClickLvl()) + " crystals!";
            permPurchaseButn.GetComponent<ItemScript>().price = "Price: " + store.GetPrice("perm") + " crystals.";
            permPurchaseButn.GetComponent<ItemScript>().itemName = "Permanent Click Increaser!";
        }
        else if (IsSvenska())
        {
            permPurchaseButn.GetComponent<ItemScript>().description = "Dina klicks ger dig " + (GameController.ReturnClickLvl()) + " kristaller!";
            permPurchaseButn.GetComponent<ItemScript>().price = "Pris: " + store.GetPrice("perm") + " kristaller.";
            permPurchaseButn.GetComponent<ItemScript>().itemName = "Permanent Klick Ökare!";
        }

        gameController.SaveGame();
    }

    public void ChangeTextTemp()
    {
        if (IsEngelska())
        {
            tempButton.GetComponent<ItemScript>().description = "Your clicks will be boosted with " + gameController.ReturnTPUAddClicksBy() + " for " + gameController.ReturnTPUTimeBeforeReset() + " seconds.";
            tempButton.GetComponent<ItemScript>().price = "price: " + store.GetPrice("tpu") + " crystals.";
            tempButton.GetComponent<ItemScript>().itemName = "Temporary Click Booster!";
        }
        else if (IsSvenska())
        {
            tempButton.GetComponent<ItemScript>().description = "Varje klick får du en boost med " + gameController.ReturnTPUAddClicksBy() + " kristaller under " + gameController.ReturnTPUTimeBeforeReset() + " sekunder.";
            tempButton.GetComponent<ItemScript>().price = "pris: " + store.GetPrice("tpu") + " kristaller.";
            tempButton.GetComponent<ItemScript>().itemName = "Temporär Klickar Boost!";
        }

    }

    public void ChangeWhenBoughtTemp()
    {
        if (IsEngelska())
        {
            print("editText");
            tempPurchaseButn.GetComponent<ItemScript>().description = "Your time boost will have to be activated. Go back into the game and press the activation button.";
            tempPurchaseButn.GetComponent<ItemScript>().price = "price: " + store.GetPrice("tpu") + " crystals.";
            tempPurchaseButn.GetComponent<ItemScript>().itemName = "Temporary Click Booster!";
        }
        else if (IsSvenska())
        {
            tempPurchaseButn.GetComponent<ItemScript>().description = "Din tidsbaserade boost måste aktiveras. Gå tillbaka till spelet och tryck på aktiveringsknappen.";
            tempPurchaseButn.GetComponent<ItemScript>().price = "pris: " + store.GetPrice("tpu") + " kristaller.";
            tempPurchaseButn.GetComponent<ItemScript>().itemName = "Temporär Klickar Boost!";
        }
        gameController.SaveGame();
    }


    public void ChangeTextDust()
    {
        if (IsEngelska())
        {
            dustButton.GetComponent<ItemScript>().description = "Bigger chance to find stardust when mining.";
            dustButton.GetComponent<ItemScript>().price = "price: " + store.GetPrice("dust") + " stardust";
            dustButton.GetComponent<ItemScript>().itemName = "Stardust Miner!";

        }
        else if (IsSvenska())
        {
            dustButton.GetComponent<ItemScript>().description = "större chans att finna stjärnpuder när du klickar!.";
            dustButton.GetComponent<ItemScript>().price = "pris: " + store.GetPrice("dust") + " stjärnpuder";
            dustButton.GetComponent<ItemScript>().itemName = "Sjärnpuder Grävare!";
        }
    }

    public void ChangeWhenBoughtDust()
    {

        if (GameController.GetStardustMinerLevel() == 20)
        {
            if (IsEngelska())
            {
                dustPurchaseButn.GetComponent<ItemScript>().description = "No more upgrades!";
                dustPurchaseButn.GetComponent<ItemScript>().price = "price: X";
                dustPurchaseButn.GetComponent<ItemScript>().itemName = "Stardust Miner!";
            }
            else if (IsSvenska())
            {
                dustPurchaseButn.GetComponent<ItemScript>().description = "Inga mer uppgraderingar!";
                dustPurchaseButn.GetComponent<ItemScript>().price = "pris: X";
                dustPurchaseButn.GetComponent<ItemScript>().itemName = "Stjärnpuder Grävare!";
            }

        }
        else if (GameController.GetStardust() >= store.GetPrice("dust"))
        {
            if (IsEngelska())
            {
                dustPurchaseButn.GetComponent<ItemScript>().description = GameController.GetStardustMinerLevel() + "% chance to find stardust!";
                dustPurchaseButn.GetComponent<ItemScript>().price = "price: " + store.GetPrice("dust") + " stardust";
                dustPurchaseButn.GetComponent<ItemScript>().itemName = "Stardust Miner!";
            }
            else if (IsSvenska())
            {
                dustPurchaseButn.GetComponent<ItemScript>().description = GameController.GetStardustMinerLevel() + "% större chans att finna stjärnpuder!";
                dustPurchaseButn.GetComponent<ItemScript>().price = "pris: " + store.GetPrice("dust") + " stjärnpuder";
                dustPurchaseButn.GetComponent<ItemScript>().itemName = "Stjärnpuder Grävare!";
            }
            gameController.SaveGame();
        }

    }
}
