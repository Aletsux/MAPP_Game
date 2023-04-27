using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Reflection;
using Random = System.Random;

public class GameController : MonoBehaviour
{
    private static long crystals;
    public Text crystalAmount;
    private static long clickIncrease = 1;

    private static int stardust;
    public Text stardustAmount;
    private static int stardustMinerLevel;
    private static Random rnd = new Random();

    //private static string suffix = "";

    /*cost as method! for scaling purposes and maybe in store!*/
    [SerializeField] int tpuCost = 1; //tpu = timedPowerUp
    [SerializeField] private float tpuTimeBeforeReset; // hur många sekunder som powerup ska hålla på
    /* make method for scaling the amount!*/
    [SerializeField] public int tpuAddClicksBy;
    private bool isUsingTPU = false; 
    private long saveCurrentClickIncrease; //saves clickIncrease before the limited timed powerUp
    private float tpuTimer = 0f;

    /*name : clickUpgrade?*/  /*cost as method! for scaling purposes and maybe in store!*/
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
    private int numPerSec = 0;
    private int theNextUpdate = 1;
    public int secBeforeIdleClick = 75;

    //private int saveIfUsingIdle = 0;
    //private int saveIfLvlOne = 0;
    private bool isAtLevel = false;
    private int numPerTime = 1;

    private int nextUpdate = 1;
    public int lvlCounter = 5;
    
    public VolumeManager volumeManager;

    void Start()
    {
        //ResetForBuild();
        LoadGame();
        DisableTPU(); //om spelaren inte har någon timed powerup
    }

    void Update()
    {
        if (crystals < 0)
        {
            crystals = 0;
        }

        if (stardust < 0)
        {
            stardust = 0;
        }

        if (isUsingTPU == true)
        {
            tpuTimer += Time.deltaTime;
            if (tpuTimer >= tpuTimeBeforeReset)
            {
                tpuTimer = 0f;
                isUsingTPU = false;
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
                Debug.Log("crystal: " + 1);
                theNextUpdate = Mathf.FloorToInt(Time.time) + secBeforeIdleClick;

                //Det som ska ske varje sekund
                IdleClickSecApart();
            }
        }

        if (isAtLevel)//varje sekund
        {
            if (Time.time >= nextUpdate)
            {
                
                nextUpdate = Mathf.FloorToInt(Time.time) + 1;
                IdleClickPowerUp();

                //Debug.Log("crystal: " + 1);
            }
        }

        UpdateCrystals();
        UpdateStardust();
    }

    public static long GetCrystals()
    {
        return crystals;
    }

    public void ClickCrystal()
    {
        crystals += (1 * clickIncrease); // add crystals
        //setSuffix();
        UpdateCrystals(); // update amount in UI
    }

    public static void AddCrystals(long toAdd)
    {
        crystals += toAdd;
    }

    public static void DecreaseCrystals(long cost) // for example: to buy
    {
        cost = (cost > crystals) ? crystals : cost;
        cost = (cost < 0) ? 0 : cost;
        crystals -= cost;
    }

    private void UpdateCrystals()
    {
        crystalAmount.text = crystals.ToString();
    }

    public void ClickIncrease()
    {
        if (crystals >= permCost)
        {
            int toAdd = 1;
            if (clickIncrease % 10 == 0) // every 10 upgrades varje gång klickar på knapp i store
                toAdd = 5;  // the player gets a bonus
            clickIncrease += toAdd;

            //permCost
            //double higherCost = idleCost * 1.02; // lägger till 2% på kostnad
            //idleCost += (int)higherCost;

            double higherCost = permCost * 1.2;
            permCost += (int)higherCost;
        }
    }

    public static long ReturnClickIncrease()
    {
        return clickIncrease;
    }

    public void ClickStardust()
    {
        int rng = rnd.Next(1, 100);
        bool endMethod = false;
        for (int i = 1; i <= stardustMinerLevel; i++)
        {
            if (rng == i)
            {
                endMethod = true;
                AddStardust(rng);
            }
            if (endMethod)
                return;
        }
        UpdateStardust();
    }

    public static int GetStardust()
    {
        return stardust;
    }

    public static void AddStardust(int toAdd)
    {
        stardust += toAdd;
    }

    public static void DecreaseStardust(int cost) // for example: to buy
    {
        cost = (cost > stardust) ? stardust : cost;
        cost = (cost < 0) ? 0 : cost;
        stardust -= cost;
    }

    public void IncreaseStardustMinerLevel()
    {
        stardustMinerLevel += 1;
    }

    public static int GetStardustMinerLevel()
    {
        return stardustMinerLevel;
    }

    private void UpdateStardust()
    {
        stardustAmount.text = stardust.ToString();
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


    private void TimedPowerUp() // göra en individs klick starkare i några sekunder
    {
        if (isUsingTPU == false)
        {
            isUsingTPU = true;

            saveCurrentClickIncrease = clickIncrease;

            clickIncrease += tpuAddClicksBy;
        }
    }

    public void ResetClickIncrease() // sätt tillbaka klick till default
    {
        clickIncrease = saveCurrentClickIncrease;
    }

    public void DoPowerUp(string powerUpName) // olika knappar kan kalla på denna och skicka in en sträng, metoden väljer sen själv vilken powerup som ska göras
    {
        if (powerUpName.Equals("tpu"))
        {
            if (TPUAmount > 0 && isUsingTPU == false) // viktigt så spelaren inte kan råka använda tpu under poweruppen
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

        //double higherCost = permCost * 1.2;
        //permCost += (int)higherCost;

        double higherCost = tpuCost * 1.2;
        tpuCost += (int)higherCost;
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

    public void SaveGame()
    {
        PlayerPrefs.SetString("crystals", GetCrystals().ToString()); // save converted long to string
        PlayerPrefs.SetString("clickIncrease", ReturnClickIncrease().ToString()); // save converted long to string
        PlayerPrefs.SetInt("stardust", stardust);
        PlayerPrefs.SetInt("stardustMinerLevel", GetStardustMinerLevel());
        PlayerPrefs.SetInt("tpu", TPUAmount);
        PlayerPrefs.SetString("quitTime", System.DateTime.Now.ToBinary().ToString());
        PlayerPrefs.SetInt("saveIfUsingIdle", Convert.ToInt32(isUsingIdleClicker));
        PlayerPrefs.SetInt("saveIfLvlOne", Convert.ToInt32(isAtLevel));
        PlayerPrefs.SetInt("numPerSec", ReturnClicksPerSec());
        PlayerPrefs.SetInt("secBeforeIdleClick", ReturnSecBeforeClick());
        PlayerPrefs.SetInt("lvlCounter", ReturnTimesToLvlUp());
        PlayerPrefs.SetFloat("Volume", volumeManager.getVolume());
        PlayerPrefs.SetInt("tpuCost", GetTpuCost());
        PlayerPrefs.SetInt("idleCost", GetIdleCost());
        PlayerPrefs.SetInt("permCost", GetPermCost());
    }

    private void ResetForBuild()
    {
        PlayerPrefs.SetString("crystals", 0.ToString());
        PlayerPrefs.SetString("clickIncrease", 1.ToString());
        PlayerPrefs.SetInt("stardust", 0);
        PlayerPrefs.SetInt("stardustMinerLevel", 0);
        PlayerPrefs.SetInt("tpu", 0);
        PlayerPrefs.SetInt("saveIfUsingIdle", Convert.ToInt32(false));

        PlayerPrefs.SetInt("saveIfLvlOne", Convert.ToInt32(false));
        PlayerPrefs.SetInt("numPerSec", 0);
        PlayerPrefs.SetInt("secBeforeIdleClick", 75);
        PlayerPrefs.SetInt("lvlCounter", 5);
        PlayerPrefs.SetInt("tpuCost", 5);
        PlayerPrefs.SetInt("idleCost", 5);
        PlayerPrefs.SetInt("permCost", 5);
    }

    public void LoadGame()
    {
        crystals = (long)Convert.ToDouble(PlayerPrefs.GetString("crystals")); // convert saved string to double and then to long
        clickIncrease = (long)Convert.ToDouble(PlayerPrefs.GetString("clickIncrease"));// convert saved string to double and then to long
        TPUAmount = PlayerPrefs.GetInt("tpu");
        isUsingIdleClicker = Convert.ToBoolean(PlayerPrefs.GetInt("saveIfUsingIdle"));
        stardust = PlayerPrefs.GetInt("stardust");
        stardustMinerLevel = PlayerPrefs.GetInt("stardustMinerLevel");

        isUsingIdleClicker = Convert.ToBoolean(PlayerPrefs.GetInt("saveIfUsingIdle"));
        isAtLevel = Convert.ToBoolean(PlayerPrefs.GetInt("saveIfLvlOne"));
        numPerSec = PlayerPrefs.GetInt("numPerSec");
        secBeforeIdleClick = PlayerPrefs.GetInt("secBeforeIdleClick");
        lvlCounter = PlayerPrefs.GetInt("lvlCounter");
        PlayerPrefs.SetFloat("Volume", volumeManager.getVolume());
        tpuCost = PlayerPrefs.GetInt("tpuCost");
        idleCost = PlayerPrefs.GetInt("idleCost");
        permCost = PlayerPrefs.GetInt("permCost");

        LoadIdleClicks(calculateSecondsSinceQuit());
        UpdateTPU();
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
            //ResetForBuild();
        }
    }


    public void BuyIdle()
    {
        isUsingIdleClicker = true;
        UpdateIdleLevel();
    }

    public void UpdateIdleLevel()
    {
        //level up it adds cost with 2%
        //
        if (isAtLevel == false || lvlCounter > 0)
        {
            double higherCost = idleCost * 1.02; // lägger till 2% på kostnad
            idleCost += (int)higherCost;
            lvlCounter -= 1;
            Debug.Log("Lvl: " + lvlCounter);

            secBeforeIdleClick -= 15;
        }

        if (lvlCounter == 0)
        {
            isAtLevel = true;
            lvlCounter = 4;
            Debug.Log("Lvl: " + lvlCounter);

            secBeforeIdleClick = 60;
            numPerSec += 1;

        }
        Debug.Log("sec" + secBeforeIdleClick);
    }

    
    

    public bool IsIdleTrue()
    {
        return isUsingIdleClicker;
    }

    public bool IsIdleLvlTrue()
    {
        return isAtLevel;
    }


    //varje sekund
    public void IdleClickPowerUp()
    {
        crystals += numPerSec;
        crystalAmount.text = crystals + ""/*suffix*/;
        
    }
    //några sekunder mellan uppdatering
    public void IdleClickSecApart()
    {
        crystals += numPerTime;
        crystalAmount.text = crystals + ""/*suffix*/;
    }




    public int GetIdleCost()
    {
        return idleCost;
    }

    public int ReturnClicksPerSec()
    {
        return numPerSec;
    }

    public int ReturnClickPerTime()
    {
        return numPerTime;
    }

    public int ReturnSecBeforeClick()
    {
        return secBeforeIdleClick;
    }

    public int ReturnTimesToLvlUp()
    {
        return lvlCounter;
    }

    

    private void LoadIdleClicks(int secondsPassed)
    {
        if (isAtLevel) //varje sek
        {
            if (numPerSec == 1)
            {
                crystals += secondsPassed;
                crystalAmount.text = crystals + ""/*suffix*/;
            }
            else if(numPerSec > 1)
            {
                int result = secondsPassed * numPerSec;
                crystals += result;
                crystalAmount.text = crystals + ""/*suffix*/;
            }           
        }

        if (isUsingIdleClicker)//längre väntetid
        {
            if(numPerTime > 0)
            {
                double dResult = secondsPassed / secBeforeIdleClick;
                int result = (int)dResult;
                crystals += result;
                crystalAmount.text = crystals + ""/*suffix*/;
            }
        }
    }

    public float ReturnTPUTimeBeforeReset()
    {
        return tpuTimeBeforeReset;
    }

    public int ReturnTPUAddClicksBy()
    {
        return tpuAddClicksBy;
    }
    //

    
}
