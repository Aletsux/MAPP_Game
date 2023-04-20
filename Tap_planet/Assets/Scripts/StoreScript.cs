using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour
{
    public GameObject storeButton; // to open
    [Space]
    public GameObject upgradeTab; //different tabs
    public GameObject accessoryTab;
    public GameObject powerupTab;
    public GameObject planetTab;
    [Space]
    public GameObject GC; // gameController
    private GameController gameController; //actually gameController script!

    [Space]
    public Text UIText;
    [Space]
    [SerializeField] int tpuCost = 1; //tpu = timedPowerUp
    [Space]
    private float timer; //counts up until it reaches timeToDisplay
    private float timeToDisplay = 2; // time to display message
    private bool displayUIMessage = false;

    void Start()
    {
        UIText.gameObject.SetActive(false);
        CloseStore();
        gameController = GC.GetComponent<GameController>(); // gets access to methods
    }

    void Update()
    {
        if (displayUIMessage) // bool set to true in DisplayMessage()
        {
            timer += Time.deltaTime;
            if (timer >= timeToDisplay)
            {
                timer = 0f;
                UIText.gameObject.SetActive(false);
                displayUIMessage = false;
            }
        }
    }

    public void OpenStore()
    {
        gameObject.SetActive(true);
    }

    public void CloseStore()
    {
        gameObject.SetActive(false);
    }

    public void CloseTabsExcept(string tab) // different buttons send different arguments
    {
        upgradeTab.SetActive(CorrectTab(tab, "upgrade")); // only true if method was called with upgrade as argument
        accessoryTab.SetActive(CorrectTab(tab, "accessory")); // false if called with upgrade
        planetTab.SetActive(CorrectTab(tab, "planet"));
        powerupTab.SetActive(CorrectTab(tab, "powerup"));
    }

    private bool CorrectTab(string tab, string thisTab)
    {
        if (tab.Equals(thisTab)) // checks if strings match
            return true;
        return false;
    }

    public void BuyPowerUp(string powerUpName) // takes which powerup to buy
    {
        if (powerUpName.Equals("tpu")) // if tpu
        {
            if (gameController.GetCrystals() >= gameController.GetTpuCost()) // checks bank balance       
            {
                gameController.AddTPUAmount(); // adds 1 to tpuAmount
                gameController.DecreaseCrystals(gameController.GetTpuCost()); // reduces money in bank
                DisplayMessage("Timed boost purchased!"); // displays message
            }
            else
            {
                DisplayMessage("Not Enough Crystals!"); // if not enough crystals in bank
            }
        }
        else if (powerUpName.Equals("permanentClickPowerUp"))
        {
            if (gameController.GetCrystals() >= gameController.GetPermCost())
            {
                gameController.ClickIncrease();
                gameController.DecreaseCrystals(gameController.GetPermCost());
                DisplayMessage("Your clicks now give you " + gameController.ReturnClickIncrease() + " crystals!");
            }
            else
            {
                DisplayMessage("Not Enough Crystals!");
            }
        }
        else if (powerUpName.Equals("IdlePower")) {

            if(gameController.GetCrystals() >= gameController.GetIdleCost())
            {
                if (gameController.IsIdleTrue() == false)
                {
                    gameController.BuyIdle();
                    DisplayMessage("You will now recieve " + gameController.ReturnClicksPerSec() + " crystal per minute!");
                }
                else
                {
                    DisplayMessage("You will now recieve " + gameController.ReturnClicksPerSec() + " crystal per " + gameController.ReturnSecBeforeClick() + " seconds");
                }
                
                gameController.DecreaseCrystals(gameController.GetIdleCost());
                
            }
            else
            {
                DisplayMessage("Not Enough Crystals!");
            }
            
        }
        else
        {
            DisplayMessage("Error: No such item found!");
        }
    }

    public void DisplayMessage(string message) // called by actions the player makes such as buying upgrades or failing to buy upgrades
    {
        UIText.gameObject.SetActive(true); // important to activate and deactivate otherwise its always in the way
        UIText.text = message;
        displayUIMessage = true; // bool to tell timer to start
        ResetTimer(); //without this, the timer would run out and the message would dissappear even if player clicks multiple times after the initial
    }

    private void ResetTimer() 
    {
        timer = 0;
    }
}