using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    private static int crystals = 0;
    public Text crystalAmount;
    private static int clickIncrease = 1;
    //private static string suffix = "";


    public Button PowerUpClicker; //knapp i affär som ska aktivera powerup när man klickar
    [SerializeField] private float timeBeforeReset; // hur många sekunder som powerup ska hålla på
    private bool isUsingPowerUp = false;
    private float timer = 0f;
    [SerializeField] private int addClicksBy;
    private static int defaultClick = 1;

    [SerializeField] int costToBuyPowerUp;



    void Start()
    {
        UpdateUI();

        PowerUpClicker.onClick.AddListener(TimedPowerUp);
    }

    void Update()
    {
        if(isUsingPowerUp == true)
        {
            timer += Time.deltaTime;
            if(timer >= timeBeforeReset)
            {
                timer = 0f;
                isUsingPowerUp = false;
                ResetClickIncrease();    
            }
        }
    }

    


    public int GetCrystals()
    {
        return crystals;
    }

    public void ClickCrystal()
    {
        crystals += (1 * clickIncrease); // add crystals
        //setSuffix();
        UpdateUI(); // update amount in UI




        //ClickIncrease();
    }

    private void UpdateUI()
    {
        crystalAmount.text = crystals + "" /*suffix*/;
    }

    public void ClickIncrease()
    {
        int toAdd = 1;
        if (clickIncrease % 10 == 0) // every 10 upgrades
            toAdd = 5;  // the player gets a bonus
        clickIncrease += toAdd;
    }

    //private string FormatCrystalAmount() // should convert from 1000 to 1k and so on
    //{
    //    if (getCrystals() < 1000)
    //        suffix = "";
    //    else if (getCrystals() < 1000000)
    //        suffix = "k";
    //    else
    //        suffix = "m";
    //    return suffix;
    //}

    public void DecreaseCrystals(int cost) // to buy
    {
        crystals -= cost;
        UpdateUI();
    }


    public void TimedPowerUp() // göra en individs klick starkare i några sekunder
    {
        if(isUsingPowerUp == false && crystals >= costToBuyPowerUp)
        {
            isUsingPowerUp = true;
            clickIncrease += addClicksBy;

            DecreaseCrystals(costToBuyPowerUp);

        }
    }

    public void ResetClickIncrease() // sätt tillbaka klick till default
    {
        clickIncrease = defaultClick;
    }




}
