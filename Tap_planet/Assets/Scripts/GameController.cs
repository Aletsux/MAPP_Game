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

    void Start()
    {
        UpdateUI();
    }
    void Update()
    {
        
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
        ClickIncrease();
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


}
