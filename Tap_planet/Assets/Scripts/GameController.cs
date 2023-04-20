using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Reflection;
using Random = System.Random;

public class GameController : MonoBehaviour
{

    private int crystals;
    public Text crystalAmount;
    private int clickIncrease = 1;

    private int stardust;
    public Text stardustAmount;
    private int stardustMinerLevel;
    private static Random rnd = new Random();

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
    private int numPerSec = 0;
    private int theNextUpdate = 1;
    public int secBeforeIdleClick = 75;

    //Accessoar
    public List<GameObject> accessoryObjects = new List<GameObject>();
    public List<Button> accessoryButtons;
    private int saveIfUsingIdle = 0;
    private int saveIfLvlOne = 0;
    private bool isAtLevel = false;
    private int numPerTime = 1;

    private int nextUpdate = 1;
    public int lvlCounter = 5;



    void Start()
    {
        DisableTPU(); //om spelaren inte har någon timed powerup

        //PlayerPrefs.DeleteAll(); //Till för testning av Accessories - ta bort om köp ska minnas efter omstart av spel, eller om det finns andra PlayerPrefs du inte vill ska påverkas
        for (int i = 0; i < accessoryObjects.Count; i++)
        {
            if (PlayerPrefs.GetInt("AccessoryEquipped_" + i) == 1)
            {
                accessoryObjects[i].SetActive(true);
                SetAccessoryButtonLabel(i, "Unequip");
            }
            else if (PlayerPrefs.GetInt("AccessoryEquipped_" + i) == 0 && PlayerPrefs.GetInt("AccessoryPurchased_" + i) == 1)
            {
                accessoryObjects[i].SetActive(false);
                SetAccessoryButtonLabel(i, "Equip");
                PlayerPrefs.SetInt("AccessoryEquipped_" + i, 0);
                PlayerPrefs.Save();
            }
            else if (PlayerPrefs.GetInt("AccessoryEquipped_" + i) == 0 && PlayerPrefs.GetInt("AccessoryPurchased_" + i) == 0)
            {
                accessoryObjects[i].SetActive(false);
                SetAccessoryButtonLabel(i, "Buy");
                PlayerPrefs.SetInt("AccessoryEquipped_" + i, 0);
                PlayerPrefs.Save();
            }

            accessoryButtons[i].onClick.RemoveAllListeners();
            accessoryButtons[i].onClick.AddListener(() =>
            {
                EquipAccessory(i);
            });
        }

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

                Debug.Log("crystal: " + 1);
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
        UpdateCrystals(); // update amount in UI
    }

    public void DecreaseCrystals(int cost) // for example: to buy
    {
        crystals -= cost;
        UpdateCrystals();
    }

    private void UpdateCrystals()
    {
        crystalAmount.text = crystals + "" /*suffix*/;
    }

    public void ClickIncrease()
    {
        if (crystals >= permCost)
        {
            int toAdd = 1;
            if (clickIncrease % 10 == 0) // every 10 upgrades varje gång klickar på knapp i store
                toAdd = 5;  // the player gets a bonus
            clickIncrease += toAdd;
        }
    }

    public int ReturnClickIncrease()
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

    public int GetStardust()
    {
        return stardust;
    }

    public void AddStardust(int toAdd)
    {
        stardust += toAdd;
    }

    public void IncreaseStardustMinerLevel()
    {
        stardustMinerLevel += 1;
    }

    public void DecreaseStardust(int cost) // for example: to buy
    {
        stardust -= cost;
        UpdateStardust();
    }

    public int GetStardustMinerLevel()
    {
        return stardustMinerLevel;
    }

    private void UpdateStardust()
    {
        stardustAmount.text = stardust + "" /*suffix*/;
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
        if (isUsingPowerUp == false)
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
        PlayerPrefs.SetInt("stardust", stardust);
        PlayerPrefs.SetInt("stardustMinerLevel", GetStardustMinerLevel());
        PlayerPrefs.SetInt("tpu", TPUAmount);
        PlayerPrefs.SetString("quitTime", System.DateTime.Now.ToBinary().ToString());
        PlayerPrefs.SetInt("saveIfUsingIdle", Convert.ToInt32(isUsingIdleClicker));
        PlayerPrefs.SetInt("saveIfLvlOne", Convert.ToInt32(isAtLevel));
        PlayerPrefs.SetInt("numPerSec", ReturnClicksPerSec());
        PlayerPrefs.SetInt("secBeforeIdleClick", ReturnSecBeforeClick());
        PlayerPrefs.SetInt("lvlCounter", ReturnTimesToLvlUp()); 
    }

    private void ResetForBuild()
    {
        PlayerPrefs.SetInt("crystals", 0);
        PlayerPrefs.SetInt("clickIncrease", 1);
        PlayerPrefs.SetInt("stardust", 200000);
        PlayerPrefs.SetInt("stardustMinerLevel", 0);
        PlayerPrefs.SetInt("tpu", 0);
        PlayerPrefs.SetInt("saveIfUsingIdle", Convert.ToInt32(false));

        PlayerPrefs.SetInt("saveIfLvlOne", Convert.ToInt32(false));
        PlayerPrefs.SetInt("numPerSec", 0);
        PlayerPrefs.SetInt("secBeforeIdleClick", 75);
        PlayerPrefs.SetInt("lvlCounter", 5);
    }

    private void LoadGame()
    {
        crystals = PlayerPrefs.GetInt("crystals");
        clickIncrease = PlayerPrefs.GetInt("clickIncrease");
        TPUAmount = PlayerPrefs.GetInt("tpu");
        isUsingIdleClicker = Convert.ToBoolean(PlayerPrefs.GetInt("saveIfUsingIdle"));
        stardust = PlayerPrefs.GetInt("stardust");
        stardustMinerLevel = PlayerPrefs.GetInt("stardustMinerLevel");

        isUsingIdleClicker = Convert.ToBoolean(PlayerPrefs.GetInt("saveIfUsingIdle"));
        isAtLevel = Convert.ToBoolean(PlayerPrefs.GetInt("saveIfLvlOne"));
        numPerSec = PlayerPrefs.GetInt("numPerSec");
        secBeforeIdleClick = PlayerPrefs.GetInt("secBeforeIdleClick");
        lvlCounter = PlayerPrefs.GetInt("lvlCounter");

        LoadIdleClicks(calculateSecondsSinceQuit());
        UpdateTPU();
        UpdateCrystals();
        UpdateStardust();
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
            ResetForBuild();
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
            double higherCost = idleCost * 1.02;
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

    public void EquipAccessory(int index) //anropas vid klick av accessories-köpknapp
    {
        if (index >= accessoryObjects.Count)
        {
            Debug.LogError("Invalid index: " + index);
            return;
        }

        bool hasPurchased = PlayerPrefs.GetInt("AccessoryPurchased_" + index, 0) == 1;

        if (!hasPurchased)
        {
            PurchaseAccessory(index);
        } else
        {
            ToggleAccessory(index);
        }
    }

    private void PurchaseAccessory(int index)
    {
        int cost = 1;
        DecreaseCrystals(cost);
        PlayerPrefs.SetInt("AccessoryPurchased_" + index, 1);
        PlayerPrefs.Save();
        SetAccessoryButtonLabel(index, "Equip");
    }

    private void ToggleAccessory(int index) //ifall accessoaren är aktiverad inaktiveras den och vice versa
    {
        if (index >= accessoryObjects.Count)
        {
            Debug.LogError("Invalid index: " + index);
            return;
        }

        bool isEquipped = accessoryObjects[index].activeSelf; //om accessoar-gameobjectet är aktiverat
        Debug.Log(index);

        //sätter för den klickade knappen
        if (isEquipped)
        {
            accessoryObjects[index].SetActive(false);
            SetAccessoryButtonLabel(index, "Equip");
            PlayerPrefs.SetInt("AccessoryEquipped_" + index, 0);
            PlayerPrefs.Save();
        }
        else
        {
            accessoryObjects[index].SetActive(true);
            SetAccessoryButtonLabel(index, "Unequip");
            PlayerPrefs.SetInt("AccessoryEquipped_" + index, 1);
            PlayerPrefs.Save();
        }
        //sätter för de andra knapparna
        for (int i = 0; i < accessoryObjects.Count; i++)
        {
            if (i != index && PlayerPrefs.GetInt("AccessoryPurchased_" + i) == 1)
            {
                accessoryObjects[i].SetActive(false);
                SetAccessoryButtonLabel(i, "Equip");
                PlayerPrefs.SetInt("AccessoryEquipped_" + i, 0);
                PlayerPrefs.Save();
            }
            else if (i != index && PlayerPrefs.GetInt("AccessoryPurchased_" + i) == 0)
            {
                accessoryObjects[i].SetActive(false);
                SetAccessoryButtonLabel(i, "Buy");
                PlayerPrefs.SetInt("AccessoryEquipped_" + i, 0);
                PlayerPrefs.Save();
            }
        }
    }

    private void SetAccessoryButtonLabel(int index, string label)
    {
        accessoryButtons[index].GetComponentInChildren<Text>().text = label;
    }
}
