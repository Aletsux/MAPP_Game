using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{

    private int crystals;
    public Text crystalAmount;
    private int clickIncrease = 1;
    //private static string suffix = "";

    [SerializeField] int tpuCost = 1; //tpu = timedPowerUp
    [SerializeField] private float tpuTimeBeforeReset; // hur många sekunder som powerup ska hålla på
    [SerializeField] public int tpuAddClicksBy;
    private bool isUsingPowerUp = false;
    private int saveCurrentClick; //saves clickIncrease before the limited timed powerUp
    private float timer = 0f;

    [SerializeField] public int permCost; //perm = permanent, så att om spelaren köper kommer de alltid ha extra klicks

    //powerups
    [Space]
    public GameObject TPU; // timed powerup objekt med knapp som ligger på spelskärmen
    //public Image TPUImage;
    public Text TPUText;
    private int TPUAmount = 0;


    [SerializeField] public int idleCost;//the cost for the idle click powerup
    public float clicksPerSecond = 1f;
    private bool isUsingIdleClicker = false;
    private int numPerSec = 1;
    private int theNextUpdate = 1;




    void Start()
    {
        DisableTPU(); //om spelaren inte har någon timed powerup
    }

    void Update()
    {
        if (isUsingPowerUp == true)
        {
            timer += Time.deltaTime;
            if(timer >= tpuTimeBeforeReset)
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
        crystalAmount.text = crystals + "" /*suffix*/;
    }

    public void ClickIncrease()
    {
        if(crystals >= permCost)
        {
            int toAdd = 1;
            if (clickIncrease % 10 == 0) // every 10 upgrades varje gång klickar på knapp i store
                toAdd = 5;  // the player gets a bonus
            clickIncrease += toAdd;
        }
    }

    private void DoIdleOnStart(int secondsPassed)
    {
        crystals += secondsPassed;
    }

    public int ReturnClickIncrease()
    {
        return clickIncrease;
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

    public void DecreaseCrystals(int cost) // for example: to buy
    {
        crystals -= cost;
        UpdateUI();
    }

    private void TimedPowerUp() // göra en individs klick starkare i några sekunder
    {
        if(isUsingPowerUp == false)
        {
            isUsingPowerUp = true;

            saveCurrentClick = clickIncrease;

            clickIncrease += tpuAddClicksBy;
        }
    }

    public void ResetClickIncrease() // sätt tillbaka klick till default
    {
        clickIncrease = saveCurrentClick;
    }

    public void DoPowerUp(string powerUpName) // olika knappar kan kalla på denna och skicka in en sträng, metoden väljer sen själv vilken powerup som ska göras
    {
        if (powerUpName.Equals("tpu"))
        {
            if (TPUAmount > 0 && isUsingPowerUp == false) // viktigt så spelaren inte kan råka använda tpu under poweruppen
            {
                TimedPowerUp();
                TPUAmount--;
                UpdateTPU();
                print("Timed PowerUp activated!");
            }
            
            if (TPUAmount == 0)
                TPU.SetActive(false);
        }
        else
        {
            print("No PowerUp found!");
        }
    }

    public int GetTpuCost()
    {
        return tpuCost;
    }
    public int GetPermCost()
    {
        return permCost;
    }

    public void AddTPUAmount()
    {
        if (TPUAmount == 0)
            TPU.SetActive(true);
        TPUAmount++;
        UpdateTPU();
    }

    private void DisableTPU()
    {
        if (TPUAmount == 0)
            TPU.SetActive(false);
    }

    private void UpdateTPU() //om spelaren inte har någon timed powerup
    {
        TPUText.text = "TPU: " + TPUAmount;
        DisableTPU();
    }

    private void SaveGame()
    {
        PlayerPrefs.SetInt("crystals", GetCrystals());
        PlayerPrefs.SetInt("clickIncrease", ReturnClickIncrease());
        PlayerPrefs.SetInt("tpu", TPUAmount);
        PlayerPrefs.SetString("quitTime", System.DateTime.Now.ToBinary().ToString());
    }

    private void LoadGame()
    {
        crystals = PlayerPrefs.GetInt("crystals");
        clickIncrease = PlayerPrefs.GetInt("clickIncrease");
        TPUAmount = PlayerPrefs.GetInt("tpu");
        UpdateTPU();
        UpdateUI();
        DoIdleOnStart(calculateSecondsSinceQuit());

    }

    private int calculateSecondsSinceQuit()
    {
        DateTime currentDate = System.DateTime.Now; //Store the current time
        long temp = Convert.ToInt64(PlayerPrefs.GetString("quitTime")); //Grab the old time from the player prefs as a long
        DateTime quitTime = DateTime.FromBinary(temp); //Convert the old time from binary to a DataTime variable
        TimeSpan difference = currentDate.Subtract(quitTime); //Use the Subtract method and store the result as a timespan variable
        return (int)difference.TotalSeconds; // return the difference as an int
    }

    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            LoadGame();
        }
        else
        {
            SaveGame();
        }
    }


    public void BuyIdle()
    {// just nu kan man bara köpa en annars dras det bara av mer när man köper igen utan någon sorts belöning
        if (crystals >= idleCost && isUsingIdleClicker == false)
        {
            isUsingIdleClicker = true;
            DecreaseCrystals(idleCost);
            //IdleClickPowerUp();
        }
    }

    //sätt inte decrease här utan ny metod annars dras det av varje sekund och man får inget.
    public void IdleClickPowerUp()
    {
        crystals += numPerSec;
        crystalAmount.text = crystals + ""/*suffix*/;
    }
}
