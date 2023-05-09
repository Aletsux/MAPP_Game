using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Reflection;
using Random = System.Random;
using System.Globalization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization.Components;

public class GameController : MonoBehaviour
{
    private static long crystals;
    public Text crystalAmount;
    private static int clickLvl = 1;

    private static int stardust;
    public Text stardustAmount;
    private static int stardustMinerLevel;
    private static Random rnd = new Random();

    [SerializeField] int tpuCost = 1; //tpu = timedPowerUp
    [SerializeField] private float tpuTimeBeforeReset; // hur många sekunder som powerup ska hålla på

    [SerializeField] public int tpuAddClicksBy;
    private bool isUsingTPU = false; 
    private int saveCurrentClickLvl; //saves clickLvl before the limited timed powerUp
    private float tpuTimer = 0f;


    //powerups
    [Space]
    public GameObject TPU; // timed powerup objekt med knapp som ligger på spelskärmen
    //public Image TPUImage;
    public Text TPUText;
    private int TPUAmount = 0;

    [SerializeField] public int idleCost = 5;//the cost for the idle click powerup
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

    public GameObject idleCollectedPanel;

    public VolumeManager volumeManager;

    void Awake()
    {
        //ResetForBuild();
        if (PlayerPrefs.GetInt("getMoney") == 1)
        {
            GetMoney();
        }
        else if (PlayerPrefs.GetInt("reset") == 1)
        {
            PlayerPrefs.DeleteAll();
            ResetForBuild();
        }
        PlayerPrefs.SetInt("reset", 0);
        PlayerPrefs.SetInt("getMoney", 0);
        LoadGame();
        DisableTPU(); //om spelaren inte har någon timed powerup
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            PlayerPrefs.SetInt("getMoney", 1);
            PlayerPrefs.SetInt("reset", 0);
            print("getmoney " + PlayerPrefs.GetInt("getMoney"));
            print("reset " + PlayerPrefs.GetInt("reset"));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerPrefs.SetInt("reset", 1);
            PlayerPrefs.SetInt("getMoney", 0);
            print("getmoney " + PlayerPrefs.GetInt("getMoney"));
            print("reset " + PlayerPrefs.GetInt("reset"));
        }
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
                RestoreClickLvl();
            }
        }

        if (isUsingIdleClicker && !DoubleTime.isActive)
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

        if (isAtLevel && !DoubleTime.isActive)//varje sekund
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

    public bool getIsTpuActive() {
        return isUsingTPU;
    }

    public static long GetCrystals()
    {
        return crystals;
    }

    public void ClickCrystal()
    {
        crystals += (1 * clickLvl); // add crystals
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

    public void ClickLevelUp()
    {
        double toAdd = 1;
        if (clickLvl % 10 == 0) // every 10 upgrades varje gång klickar på knapp i store
            toAdd = clickLvl;  // the player gets a bonus
        clickLvl += 1 + (int)(clickLvl * 0.1);
    }

    public static int ReturnClickLvl()
    {
        return clickLvl;
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

    private void TimedPowerUp() // göra en individs klick starkare i några sekunder
    {
        if (isUsingTPU == false)
        {
            isUsingTPU = true;

            saveCurrentClickLvl = clickLvl;

            clickLvl *= 2;
        }
    }

    public void RestoreClickLvl() // sätt tillbaka klick till default
    {
        clickLvl = saveCurrentClickLvl;
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
    public static int GetClickLvl()
    {
        return clickLvl;
    }

    public void AddTPUAmount()
    {
        if (TPUAmount == 0)
            TPU.SetActive(true);
        TPUAmount++;
        UpdateTPU();

        //double higherCost = clickLvl * 1.2;
        //clickLvl += (int)higherCost;

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
        PlayerPrefs.SetInt("clickLvl", GetClickLvl()); // save converted long to string
        PlayerPrefs.SetInt("stardust", stardust);
        PlayerPrefs.SetInt("stardustMinerLevel", GetStardustMinerLevel());
        PlayerPrefs.SetInt("tpu", TPUAmount);
        PlayerPrefs.SetInt("quitTime", (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
        PlayerPrefs.SetInt("saveIfUsingIdle", Convert.ToInt32(isUsingIdleClicker));
        PlayerPrefs.SetInt("saveIfLvlOne", Convert.ToInt32(isAtLevel));
        PlayerPrefs.SetInt("numPerSec", ReturnClicksPerSec());
        PlayerPrefs.SetInt("secBeforeIdleClick", ReturnSecBeforeClick());
        PlayerPrefs.SetInt("lvlCounter", ReturnTimesToLvlUp());
        PlayerPrefs.SetFloat("Volume", volumeManager.getVolume());
        PlayerPrefs.SetInt("tpuCost", GetTpuCost());
        PlayerPrefs.SetInt("idleCost", GetIdleCost());
        PlayerPrefs.SetInt("clickLvl", GetClickLvl());
        PlayerPrefs.Save();
    }

    private void ResetForBuild()
    {
        PlayerPrefs.SetString("crystals", 0.ToString());
        PlayerPrefs.SetInt("clickLvl", 1);
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
        PlayerPrefs.SetInt("clickLvl", 1);

        PlayerPrefs.SetInt("IdleExtenderLvl", 0);
        PlayerPrefs.Save();
    }

    private void GetMoney()
    {
        PlayerPrefs.SetString("crystals", 1000000.ToString());
        PlayerPrefs.SetInt("clickLvl", 1);
        PlayerPrefs.SetInt("stardust", 1000000);
        PlayerPrefs.SetInt("stardustMinerLevel", 0);
        PlayerPrefs.SetInt("tpu", 0);
        PlayerPrefs.SetInt("saveIfUsingIdle", Convert.ToInt32(false));

        PlayerPrefs.SetInt("saveIfLvlOne", Convert.ToInt32(false));
        PlayerPrefs.SetInt("numPerSec", 0);
        PlayerPrefs.SetInt("secBeforeIdleClick", 75);
        PlayerPrefs.SetInt("lvlCounter", 5);
        PlayerPrefs.SetInt("tpuCost", 5);
        PlayerPrefs.SetInt("idleCost", 5);
        PlayerPrefs.SetInt("clickLvl", 1);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        if (long.TryParse(PlayerPrefs.GetString("crystals"), out long getCrystals))
        {
            crystals = getCrystals;
            print("yes");
        }
        else
        {
            crystals = 0;
            print("no");
        }
        clickLvl = PlayerPrefs.GetInt("clickLvl");
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
        clickLvl = PlayerPrefs.GetInt("clickLvl");

        if (PlayerPrefs.GetInt("IdleExtenderLvl") == 0) // ser till att level inte är 0
        {
            PlayerPrefs.SetInt("IdleExtenderLvl", 1);
        }

        if (calculateSecondsSinceQuit() > 1800 && calculateSecondsSinceQuit() <= 1800 * PlayerPrefs.GetInt("IdleExtenderLvl")) // om spelaren kommer in efter 30 min men innan idle extenders gräns
        {
            idleCollectedPanel.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = FormatNumbers.FormatInt(ReturnIdleClicks(calculateSecondsSinceQuit()));
            idleCollectedPanel.GetComponent<PanelAnimation>().StretchPanel();
        }
        else if (calculateSecondsSinceQuit() > 1800 * PlayerPrefs.GetInt("IdleExtenderLvl")) // kommer in efter idle extenders gräns
        {
            idleCollectedPanel.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = LocalizationSettings.StringDatabase.GetLocalizedString("IdleStartPanel", "FellAsleep"); // hämtar översättning


            idleCollectedPanel.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = FormatNumbers.FormatInt(ReturnIdleClicks(calculateSecondsSinceQuit()));
            idleCollectedPanel.GetComponent<PanelAnimation>().StretchPanel();
        }
        LoadIdleClicks(calculateSecondsSinceQuit());

        UpdateTPU();
    }

    public static int calculateSecondsSinceQuit()
    {
        return (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds - PlayerPrefs.GetInt("quitTime");
        
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
    {
        isUsingIdleClicker = true;
        UpdateIdleLevel();
    }

    public void UpdateIdleLevel()
    {
        if (isAtLevel == false || lvlCounter > 0)
        {
            double num = Math.Ceiling(idleCost * 1.02); // lägger till 2% på kostnad
            idleCost = (int)num;
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
        if (secondsPassed > PlayerPrefs.GetInt("IdleExtenderLvl") * 1800) 
        {
            secondsPassed = PlayerPrefs.GetInt("IdleExtenderLvl") * 1800; // maxgräns för vad spelaren kan tjäna
        }

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

    private int ReturnIdleClicks(int secondsPassed) // returnerar det LoadIdleClicks adderar till spelarens total
    {
        int idleClicks = 0;
        if (secondsPassed > PlayerPrefs.GetInt("IdleExtenderLvl") * 1800)
        {
            secondsPassed = PlayerPrefs.GetInt("IdleExtenderLvl") * 1800;
        }

        if (isAtLevel) //varje sek
        {
            if (numPerSec == 1)
            {
                idleClicks += secondsPassed;
            }
            else if (numPerSec > 1)
            {
                int result = secondsPassed * numPerSec;
                idleClicks += result;
            }
        }

        if (isUsingIdleClicker)//längre väntetid
        {
            if (numPerTime > 0)
            {
                double dResult = secondsPassed / secBeforeIdleClick;
                int result = (int)dResult;
                crystals += result;
                crystalAmount.text = crystals + ""/*suffix*/;
            }
        }
        return idleClicks;
    }

    public float ReturnTPUTimeBeforeReset()
    {
        return tpuTimeBeforeReset;
    }

    public int ReturnTPUAddClicksBy()
    {
        return tpuAddClicksBy;
    }
}
