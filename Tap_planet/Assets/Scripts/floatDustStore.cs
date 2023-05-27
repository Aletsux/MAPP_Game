using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatDustStore : MonoBehaviour
{// changeScript Denne skall holla koll på köp i butik

    public GameObject floatingThing;

    private Transform trans;

    public GameObject GC;
    private GameController gameController;

    public GameObject dustParent;

    //från storescript
    public List<int> accessoryCosts = new List<int>();
    public List<int> planetCosts = new List<int>();


    private StoreScript storeScript;
    public GameObject SS;

    


    private float timer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GC.GetComponent<GameController>();

        //accessoryScript = AS.GetComponent<AccessoryScript>();
        storeScript = SS.GetComponent<StoreScript>();

        accessoryCosts = storeScript.GetAccessoryCost();
        planetCosts = storeScript.GetPlanetCost();
        

    }

    private void OnEnable()
    {
        gameController = GC.GetComponent<GameController>();

        //accessoryScript = AS.GetComponent<AccessoryScript>();

        accessoryCosts = storeScript.GetAccessoryCost();
        planetCosts = storeScript.GetPlanetCost();

    }

    // Update is called once per frame
    void Update()
    {
        

    }


    public void Bought(Transform transform)
    {
        trans = transform;
    }

    public void Clean()
    {

        int numKids = dustParent.transform.childCount;

        if (numKids > 0)
        {
            for (int i = 0; i < numKids; i++)
            {
                GameObject prefab = dustParent.transform.GetChild(i).gameObject;
                Destroy(prefab, timer);

            }
        }
    }





    // från storescript
    public void CheckPrice(string powerUpName)
    {
        if (powerUpName.Equals("temp")) // if tpu
        {
            if (GameController.GetCrystals() >= GetPrice(powerUpName)) // checks bank balance       
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("perm"))
        {
            if (GameController.GetCrystals() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("idle"))
        {
            if (GameController.GetCrystals() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("dust"))
        {
            int cost = (GameController.GetStardustMinerLevel() == 0) ? 20 : GameController.GetStardustMinerLevel() * 50;
            if (GameController.GetStardust() >= cost)
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("star"))
        {
            if (GameController.IsIdleTrue() && GameController.GetCrystals() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("raidWipe"))
        {
            if (GameController.GetStardust() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("doubletime"))
        {
            if (GameController.IsIdleTrue() && GameController.GetCrystals() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("test") && PlayerPrefs.GetInt("AccessoryPurchased_" + 0) == 0)
        {
            GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
            Clean();
        }
        else if (powerUpName.Equals("party") && PlayerPrefs.GetInt("AccessoryPurchased_" + 1) == 0)
        {
            if (GameController.GetStardust() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("cow") && PlayerPrefs.GetInt("AccessoryPurchased_" + 2) == 0)
        {
            if (GameController.GetStardust() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("halo") && PlayerPrefs.GetInt("AccessoryPurchased_" + 3) == 0)
        {
            if (GameController.GetStardust() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("cap") && PlayerPrefs.GetInt("AccessoryPurchased_" + 4) == 0)
        {
            if (GameController.GetStardust() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("glasses") && PlayerPrefs.GetInt("AccessoryPurchased_" + 5) == 0)
        {
            if (GameController.GetStardust() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("OrTie") && PlayerPrefs.GetInt("AccessoryPurchased_" + 6) == 0)
        {
            if (GameController.GetStardust() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("PurTie") && PlayerPrefs.GetInt("AccessoryPurchased_" + 7) == 0)
        {
            if (GameController.GetStardust() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("light") && PlayerPrefs.GetInt("AccessoryPurchased_" + 8) == 0)
        {
            if (GameController.GetStardust() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("leaf") && PlayerPrefs.GetInt("AccessoryPurchased_" + 9) == 0)
        {
            if (GameController.GetStardust() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("drip") && PlayerPrefs.GetInt("PlanetPurchased_" + 1) == 0)
        {
            if (GameController.GetStardust() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();

            }
        }
        else if (powerUpName.Equals("cookie") && PlayerPrefs.GetInt("PlanetPurchased_" + 2) == 0)
        {
            if(GameController.GetStardust() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("candy") && PlayerPrefs.GetInt("PlanetPurchased_" + 3) == 0)
        {
            if (GameController.GetStardust() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("melon") && PlayerPrefs.GetInt("PlanetPurchased_" + 4) == 0)
        {
            if (GameController.GetStardust() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("tomato") && PlayerPrefs.GetInt("PlanetPurchased_" + 5) == 0)
        {
            if (GameController.GetStardust() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }
        else if (powerUpName.Equals("swirl") && PlayerPrefs.GetInt("PlanetPurchased_" + 6) == 0)
        {
            if (GameController.GetStardust() >= GetPrice(powerUpName))
            {
                GameObject newFlot = GameObject.Instantiate(floatingThing, trans.position, Quaternion.identity, GameObject.FindGameObjectWithTag("parent").transform);
                Clean();
            }
        }

    }

    //Kopierrat från storeScript inte mitt
    public int GetPrice(string name)
    {
        //Powerups / upgrades
        if (name.Equals("idle"))
        {
            return gameController.GetIdleCost();
        }
        else if (name.Equals("perm"))
        {
            return GameController.GetClickLvl() * (5);
        }
        else if (name.Equals("temp"))
        {
            return gameController.GetTpuCost();
        }
        else if (name.Equals("dust"))
        {
            return (GameController.GetStardustMinerLevel() == 0) ? 50 : GameController.GetStardustMinerLevel() * 100;
        }
        else if (name.Equals("star"))
        {
            return PlayerPrefs.GetInt("IdleExtenderLvl") * PlayerPrefs.GetInt("IdleExtenderLvl") * 1000;
        }
        else if (name.Equals("doubletime"))
        {
            return (int)DoubleTime.GetCost();
        }
        else if (name.Equals("raidWipe"))
        {
            if (PlayerPrefs.GetInt("RaidWipeCost") == 0)
            {
                PlayerPrefs.SetInt("RaidWipeCost", 10);
            }
            return PlayerPrefs.GetInt("RaidWipeCost");
        }

        //Accessories
        else if (name.Equals("party"))
        {
            return accessoryCosts[1];
        }
        else if (name.Equals("cow"))
        {
            return accessoryCosts[2];
        }
        else if (name.Equals("halo"))
        {
            return accessoryCosts[3];
        }
        else if (name.Equals("cap"))
        {
            return accessoryCosts[4];
        }
        else if (name.Equals("glasses"))
        {
            return accessoryCosts[5];
        }
        else if (name.Equals("OrTie"))
        {
            return accessoryCosts[6];
        }
        else if (name.Equals("PurTie"))
        {
            return accessoryCosts[7];
        }
        else if (name.Equals("light"))
        {
            return accessoryCosts[8];
        } 
        else if (name.Equals("leaf"))
        {
            return accessoryCosts[9];
        }

        //Planets
        else if (name.Equals("drip"))
        {
            return planetCosts[1];
        }
        else if (name.Equals("cookie"))
        {
            return planetCosts[2];
        }
        else if (name.Equals("candy"))
        {
            return planetCosts[3];
        }
        else if (name.Equals("melon"))
        {
            return planetCosts[4];
        }
        else if (name.Equals("tomato"))
        {
            return planetCosts[5];
        }
        else if (name.Equals("swirl"))
        {
            return planetCosts[6];
        }
        
        return 0;
    }




}
