using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public GameObject DS;
    private DescriptionScript descriptionScript;

    //public GameObject itemName;
    public TMP_Text itemName;
    public TMP_Text desc;
    public TMP_Text price;
    public Image sprite;
    public Image newSprite;

    private int dustCost = 50;

    private static int clickLvl = 1;

    private int numPerm;


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

        descriptionScript = DS.GetComponent<DescriptionScript>();

        //Debug.Log("Gamegontroller" + gameController.IsIdleTrue());
        //descriptionState.SetObjectActive();
    }

    private void OnEnable() // nåt fel med setActive false i kanske descState, scriptet blev false oxå och startade inte upp förrän desc gjorde det
    {
        gameController = GC.GetComponent<GameController>();
        descriptionScript = DS.GetComponent<DescriptionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        

        


    }



    public void ChangeTextIdle()// när spelaren trycker på blå knapp inte köp ska det reflektera  // fungerar fint
    {
        //Debug.Log("GameController text" + gameController.IsIdleTrue());
        print("edittext");
        if (GameController.IsIdleTrue() == false && IsEngelska() == true)
        {
            idleButton.GetComponent<ItemScript>().description = "Get 1 crystal every 60 sec! Each upgrade will decrease time by 15 seconds.";
            idleButton.GetComponent<ItemScript>().price = "price: " + store.GetPrice("idle") + " crystals.";
            idleButton.GetComponent<ItemScript>().itemName = "Mr Twinkie";
        }
        else if (GameController.IsIdleTrue() && gameController.IsIdleLvlTrue() == false && IsEngelska() == true)
        {
            idleButton.GetComponent<ItemScript>().description = "You get " + (gameController.ReturnClicksPerSec() + 1) + " crystals every " + (gameController.ReturnSecBeforeClick()) + " seconds.";
            idleButton.GetComponent<ItemScript>().price = "price: " + store.GetPrice("idle") + " crystals.";
            idleButton.GetComponent<ItemScript>().itemName = "Mr Twinkie";
        }
        else if (gameController.IsIdleLvlTrue() && GameController.IsIdleTrue() && IsEngelska() == true)
        {
            idleButton.GetComponent<ItemScript>().description = "You get " + gameController.ReturnClicksPerSec() + " crystals per second and " + gameController.ReturnClickPerTime() + " every " + (gameController.ReturnSecBeforeClick()) + " seconds.";
            idleButton.GetComponent<ItemScript>().price = "price: " + store.GetPrice("idle") + " crystals.";
            idleButton.GetComponent<ItemScript>().itemName = "Mr Twinkie";
        }
        else if (GameController.IsIdleTrue() == false && IsSvenska() == true)
        {
            idleButton.GetComponent<ItemScript>().description = "Få 1 kristall varje 60 sekunder! Varje uppgradering gör dig 15 sekunder snabbare.";
            idleButton.GetComponent<ItemScript>().price = "pris: " + store.GetPrice("idle") + " Kristaller.";
            idleButton.GetComponent<ItemScript>().itemName = "Herr Glimt";
        }
        else if (GameController.IsIdleTrue() && gameController.IsIdleLvlTrue() == false && IsSvenska() == true)
        {
            idleButton.GetComponent<ItemScript>().description = "Du får " + (gameController.ReturnClicksPerSec() + 1) + " kristaller efter " + (gameController.ReturnSecBeforeClick()) + " sekunder.";
            idleButton.GetComponent<ItemScript>().price = "pris: " + store.GetPrice("idle") + " kristaller.";
            idleButton.GetComponent<ItemScript>().itemName = "Herr Glimt";
        }
        else if (gameController.IsIdleLvlTrue() && GameController.IsIdleTrue() && IsSvenska() == true)
        {
            idleButton.GetComponent<ItemScript>().description = "Du får " + gameController.ReturnClicksPerSec() + " kristaller varje sekund och " + gameController.ReturnClickPerTime() + " efter " + (gameController.ReturnSecBeforeClick()) + " sekunder.";
            idleButton.GetComponent<ItemScript>().price = "pris: " + store.GetPrice("idle") + " kristaller.";
            idleButton.GetComponent<ItemScript>().itemName = "Herr Glimt";
        }
    }

    public void ChangeWhenBoughtIdle()
    {
        if (IsEngelska() == true)
        {
            //idlePurchaseButn.GetComponent<ItemScript>().description = "You get " + gameController.ReturnClicksPerSec() + " crystals per second and " + gameController.ReturnClickPerTime() + " every " + gameController.ReturnSecBeforeClick() + " seconds.";
            //idlePurchaseButn.GetComponent<ItemScript>().price = "price: " + store.GetPrice("idle") + " crystals.";
            //idlePurchaseButn.GetComponent<ItemScript>().itemName = "Idle Clicker!";

            if ((gameController.ReturnSecBeforeClick() - 15) == 0)
            {
                if (!GameController.IsIdleTrue())
                {
                    idleButton.GetComponent<ItemScript>().description = "You get " + gameController.ReturnClicksPerSec() + " crystals per second and " + gameController.ReturnClickPerTime() + " every 60 seconds.";
                }
                else
                {
                    idleButton.GetComponent<ItemScript>().description = "You get " + (gameController.ReturnClicksPerSec()+1) + " crystals per second and " + gameController.ReturnClickPerTime() + " every 60 seconds.";
                }
                
            }
            else
            {
                idleButton.GetComponent<ItemScript>().description = "You get " + gameController.ReturnClicksPerSec() + " crystals per second and " + gameController.ReturnClickPerTime() + " every " + (gameController.ReturnSecBeforeClick() - 15) + " seconds.";
            }
            idleButton.GetComponent<ItemScript>().price = "price: " + store.GetPrice("idle") + " crystals.";
            idleButton.GetComponent<ItemScript>().itemName = "Mr Twinkie";

        }
        else if (IsSvenska() == true)
        {
            if ((gameController.ReturnSecBeforeClick() - 15) == 0)
            {
                if (!GameController.IsIdleTrue())
                {
                    idleButton.GetComponent<ItemScript>().description = "Du får " + gameController.ReturnClicksPerSec() + " kristaller varje sekund och " + gameController.ReturnClickPerTime() + " kristaller efter 60 sekunder.";
                }
                else
                {
                    idleButton.GetComponent<ItemScript>().description = "Du får " + (gameController.ReturnClicksPerSec() + 1) + " kristaller varje sekund och " + gameController.ReturnClickPerTime() + " kristaller efter 60 sekunder.";
                }

            }
            else
            {
                idleButton.GetComponent<ItemScript>().description = "Du får " + gameController.ReturnClicksPerSec() + " kristaller per sekund och " + gameController.ReturnClickPerTime() + " efter " + (gameController.ReturnSecBeforeClick() - 15) + " sekunder.";
            }
            //idlePurchaseButn.GetComponent<ItemScript>().description = "Du får " + gameController.ReturnClicksPerSec() + " kristaller varje sekund och " + gameController.ReturnClickPerTime() + " varje " + gameController.ReturnSecBeforeClick() + " sekunder.";
            idleButton.GetComponent<ItemScript>().price = "pris: " + store.GetPrice("idle") + " kristaller.";
            idleButton.GetComponent<ItemScript>().itemName = "Herr Glimt";
        }
    }

    public void ChangeTextPerm()// Your clicks will increased by 1 crystal with each purchase! Every 10 purchase will add 5.
    {
        if (IsEngelska())
        {
            permButton.GetComponent<ItemScript>().price = "Price: " + store.GetPrice("perm") + " crystals.";
            permButton.GetComponent<ItemScript>().description = "Your clicks will increase by 1 crystal with each purchase. You get a bonus every 10th purchase!";
            permButton.GetComponent<ItemScript>().itemName = "Infinity Clicks";
        }
        else if (IsSvenska())
        {
            permButton.GetComponent<ItemScript>().price = "Pris: " + store.GetPrice("perm") + " kristaller.";
            permButton.GetComponent<ItemScript>().description = "Du får en extra kristall varje gång du klickar. Du får en bonus för var 10:e köp!";
            permButton.GetComponent<ItemScript>().itemName = "Starkare Klicks";
        }

    }

    public void ChangeWhenBoughtPerm()
    {
        if (IsEngelska())
        {
            permButton.GetComponent<ItemScript>().description = "Your clicks have gotten a permanent boost!";
            permButton.GetComponent<ItemScript>().price = "Price: " + store.GetPrice("perm") + " crystals.";
            permButton.GetComponent<ItemScript>().itemName = "Infinity Clicks";
        }
        else if (IsSvenska())
        {
            permButton.GetComponent<ItemScript>().description = "Dina klicks har fått en permanent boost!";
            permButton.GetComponent<ItemScript>().price = "Pris: " + store.GetPrice("perm") + " kristaller.";
            permButton.GetComponent<ItemScript>().itemName = "Starkare Klicks";
        }

        gameController.SaveGame();
    }

    public void ChangeTextTemp()
    {
        if (IsEngelska())
        {
            tempButton.GetComponent<ItemScript>().description = "Your clicks will be boosted with " + gameController.ReturnTPUAddClicksBy() + " for " + gameController.ReturnTPUTimeBeforeReset() + " seconds.";
            tempButton.GetComponent<ItemScript>().price = "price: " + store.GetPrice("tpu") + " crystals.";
            tempButton.GetComponent<ItemScript>().itemName = "Temporary Click Booster";
        }
        else if (IsSvenska())
        {
            tempButton.GetComponent<ItemScript>().description = "Varje klick får du en boost med " + gameController.ReturnTPUAddClicksBy() + " kristaller under " + gameController.ReturnTPUTimeBeforeReset() + " sekunder.";
            tempButton.GetComponent<ItemScript>().price = "pris: " + store.GetPrice("tpu") + " kristaller.";
            tempButton.GetComponent<ItemScript>().itemName = "Temporär Klickar Boost";
        }

    }

    public void ChangeWhenBoughtTemp()
    {
        if (IsEngelska())
        {
            print("editText");
            tempButton.GetComponent<ItemScript>().description = "Your time boost will have to be activated. Go back into the game and press the activation button.";
            tempButton.GetComponent<ItemScript>().price = "price: " + store.GetPrice("tpu") + " crystals.";
            tempButton.GetComponent<ItemScript>().itemName = "Temporary Click Booster";
        }
        else if (IsSvenska())
        {
            tempButton.GetComponent<ItemScript>().description = "Din tidsbaserade boost måste aktiveras. Gå tillbaka till spelet och tryck på aktiveringsknappen.";
            tempButton.GetComponent<ItemScript>().price = "pris: " + store.GetPrice("tpu") + " kristaller.";
            tempButton.GetComponent<ItemScript>().itemName = "Temporär Klickar Boost";
        }
        gameController.SaveGame();
    }


    public void ChangeTextDust()
    {
        if (IsEngelska())
        {
            dustButton.GetComponent<ItemScript>().description = "Bigger chance to find stardust when mining.";
            dustButton.GetComponent<ItemScript>().price = "price: " + store.GetPrice("dust") + " stardust";
            dustButton.GetComponent<ItemScript>().itemName = "Stardust Miner";

        }
        else if (IsSvenska())
        {
            dustButton.GetComponent<ItemScript>().description = "Större chans att finna stjärnpuder när du klickar!.";
            dustButton.GetComponent<ItemScript>().price = "pris: " + store.GetPrice("dust") + " stjärnstoft";
            dustButton.GetComponent<ItemScript>().itemName = "Stjärnstoft Grävare";
        }
    }

    public void ChangeWhenBoughtDust()
    {

        if (GameController.GetStardustMinerLevel() == 20)
        {
            if (IsEngelska())
            {
                dustButton.GetComponent<ItemScript>().description = "No more upgrades!";
                dustButton.GetComponent<ItemScript>().price = "price: X";
                dustButton.GetComponent<ItemScript>().itemName = "Stardust Miner";
            }
            else if (IsSvenska())
            {
                dustButton.GetComponent<ItemScript>().description = "Inga mer uppgraderingar!";
                dustButton.GetComponent<ItemScript>().price = "pris: X";
                dustButton.GetComponent<ItemScript>().itemName = "Stjärnstoft Grävare";
            }

        }
        else if (GameController.GetStardust() >= store.GetPrice("dust"))
        {
            if (IsEngelska())
            {
                dustButton.GetComponent<ItemScript>().description = (GameController.GetStardustMinerLevel() + 1) + "% chance to find stardust!";
                dustButton.GetComponent<ItemScript>().price = "price: " + store.GetPrice("dust") + " stardust";
                dustButton.GetComponent<ItemScript>().itemName = "Stardust Miner";
            }
            else if (IsSvenska())
            {
                dustButton.GetComponent<ItemScript>().description = (GameController.GetStardustMinerLevel() + 1) + "% större chans att finna stjärnstoft!";
                dustButton.GetComponent<ItemScript>().price = "pris: " + store.GetPrice("dust") + " stjärnstoft";
                dustButton.GetComponent<ItemScript>().itemName = "Stjärnstoft Grävareds";
            }
            gameController.SaveGame();
        }
    }
    public void Check()
    {
        if (IsSvenska())
        {
            itemName.text = "Välkommen!";
            desc.text = "Köp allt du har råd med.";
            price.text = "Priser kan variera.";
            sprite.sprite = newSprite.sprite;
        }
        else if (IsEngelska())
        {
            itemName.text = "Welcome!";
            desc.text = "Purchase whatever you can afford.";
            price.text = "Prices may vary.";
            sprite.sprite = newSprite.sprite;
        }
    }
}