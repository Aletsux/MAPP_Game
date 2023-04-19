using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    private static int crystals;
    public Text crystalAmount;
    private static int clickIncrease = 1;
    //private static string suffix = "";

    [SerializeField] int tpuCost = 1; //tpu = timedPowerUp
    [SerializeField] private float tpuTimeBeforeReset; // hur många sekunder som powerup ska hålla på
    [SerializeField] public int tpuAddClicksBy;
    private bool isUsingPowerUp = false;
    private int saveCurrentClick; //saves how many clicks the player has before their limited timed powerUp
    private float timer = 0f;

    [SerializeField] public int permCost;//perm = permanent, så att om spelaren köper kommer de alltid ha extra klicks

    [SerializeField] public int idleCost;//the cost for the idle click powerup
    public float clicksPerSecond = 1f;
    private bool isUsingIdleClicker = false;
    private int numPerSec = 1;
    private int theNextUpdate = 1;
    




    void Start()
    {
        UpdateUI();

    }

    void Update()
    {
        if (isUsingPowerUp == true)
        {
            timer += Time.deltaTime;
            if (timer >= tpuTimeBeforeReset)
            {
                timer = 0f;
                isUsingPowerUp = false;
                ResetClickIncrease();
            }
        }



        if (isUsingIdleClicker)
        {
            //om uppdateringen har skett
            if (Time.time >= theNextUpdate)
                //Time.time är the beginning of this frame
            {
                //ändra theNextUpdate (current second+1)
                //alltså lägg till en sekund så att den väntar
                //den väntar tills att uppdate
                theNextUpdate = Mathf.FloorToInt(Time.time) + 1;

                //Det som ska ske varje sekund
                IdleClickPowerUp();
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

    }

    private void UpdateUI()
    {
        crystalAmount.text = crystals + ""/*suffix*/;

    }

    public void ClickIncrease()
    {

        if (crystals >= permCost)
        {
            DecreaseCrystals(permCost); // kostar pengar för denna

            int toAdd = 1;
            if (clickIncrease % 10 == 0) // every 10 upgrades varje gång klikcar på knapp i store
                toAdd = 5;  // the player gets a bonus
            clickIncrease += toAdd;
        }
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
        if (isUsingPowerUp == false && crystals >= tpuCost)
        {
            isUsingPowerUp = true;

            saveCurrentClick = clickIncrease;

            clickIncrease += tpuAddClicksBy;

            DecreaseCrystals(tpuCost);

        }
    }

    public void ResetClickIncrease() // sätt tillbaka klick till det man hade tidigare
    {
        clickIncrease = saveCurrentClick;
    }



    //Kanske lägga till att det blir fler klick per sekund när man betarar för fler
    // och ett textfönster som visar upp hur många man får per sek
    
    public void BuyIdle()
    {// just nu kan man bara köpa en annars dras det bara av mer när man köper igen utan någon sorts belöning
        if (crystals >= idleCost && isUsingIdleClicker == false) 
        {
            isUsingIdleClicker = true;
            DecreaseCrystals(idleCost);
            IdleClickPowerUp();
        }
    }

    //sätt inte decrease här utan ny metod annars dras det av varje sekund och man får inget.
    public void IdleClickPowerUp()
    {
        crystals += numPerSec;
        crystalAmount.text = crystals + ""/*suffix*/;
    }

  


}
