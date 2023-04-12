using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    private static int crystals = 980;
    public Text crystalAmount;
    private static int clickMultiplier = 1;
    //private static string suffix = "";

    void Start()
    {
        
    }
    void Update()
    {
        
    }


    public int getCrystals()
    {
        return crystals;
    }

    public void clickCrystal()
    {
        crystals += (1 * clickMultiplier); // add crystals
        //setSuffix();
        updateUI(); // update amount in UI
    }

    private void updateUI()
    {
        crystalAmount.text = crystals + "" /*suffix*/;
    }

    //private string setSuffix()
    //{
    //    if (getCrystals() < 1000)
    //        suffix = "";
    //    else if (getCrystals() < 1000000)
    //        suffix = "k";
    //    else
    //        suffix = "m";
    //    return suffix;
    //}
}
