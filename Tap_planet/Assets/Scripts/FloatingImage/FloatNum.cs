using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatNum : MonoBehaviour
{
    public GameObject floatingThing;
    private Transform trans;

    //private bool bought = false;
    private float timer = 1f;

    public GameObject parent;
    public GameObject cryParent;

    public GameObject GC;
    private GameController gameController;

    public List<int> accessoryCosts = new List<int>();
    public List<int> planetCosts = new List<int>();

    public GameObject crystalPos;
    private int nextUpdate = 1;



    // Start is called before the first frame update
    void Start()
    {
        gameController = GC.GetComponent<GameController>();
    }

    private void OnEnable()
    {
        gameController = GC.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

        //if (gameController.IsIdleLvlTrue())//varje sekund
        //{
        //    if (Time.time >= nextUpdate)
        //    {
        //        nextUpdate = Mathf.FloorToInt(Time.time) + 1;
        //        GameObject newFlot = GameObject.Instantiate(floatingThing, crystalPos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("CryParent").transform);
        //        CleanCry();

        //    }
        //}


        //if (GameController.IsIdleTrue())//varje sekund
        //{
        //    if (Time.time >= nextUpdate)
        //    {
        //        nextUpdate = Mathf.FloorToInt(Time.time) + gameController.ReturnSecBeforeClick();

        //        GameObject newFlot = GameObject.Instantiate(floatingThing, crystalPos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("CryParent").transform);
        //        CleanCry();
        //    }
        //}
    }

    public void Bought(Transform transform)
    {
        trans = transform;
        //bought = true;
    }

    public void Clean()
    {

        int numKids = parent.transform.childCount;

        if (numKids > 0)
        {
            for (int i = 0; i < numKids; i++)
            {
                GameObject prefab = parent.transform.GetChild(i).gameObject;
                Destroy(prefab, timer);

            }
        }
    }

    






    // från storescript inte mitt bara det jag la i if satserna
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
        return 0;
    }
}
