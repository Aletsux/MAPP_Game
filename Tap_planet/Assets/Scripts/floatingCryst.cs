using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingCryst : MonoBehaviour
{
    public GameObject floatingCrystal;
    //public GameObject floatingStarDust;

    private Transform crystalTrans;
    //private Transform dustTrans;

    public GameObject crystalPos;
    public GameObject dustPos;

    private int nextUpdate = 1;
    public GameObject cryParent;
    public GameObject dustParent;
    //public GameObject tapCryParent;

    public GameObject GC;
    private GameController gameController;

    //private int starDust;
    //private int currentDust;

    //private long crystals;
    //private long currentCrystals;

    private float timer = 1f;

    //private bool idleLvl;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GC.GetComponent<GameController>();
        //starDust = GameController.GetStardust();

        //crystals = GameController.GetCrystals();

        //idleLvl = gameController.IsIdleLvlTrue();
    }

    //private void OnEnable()
    //{
    //    gameController = GC.GetComponent<GameController>();
    //}

    // Update is called once per frame
    void Update()
    {

        //currentDust = GameController.GetStardust();
        //if (currentDust > starDust)
        //{
        //    GameObject newFlot = GameObject.Instantiate(floatingStarDust, dustPos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("DustParent").transform);
        //    CleanDust();
        //    starDust = currentDust;
        //}

        //idleLvl = gameController.IsIdleLvlTrue();
        //if (idleLvl)
        //{
        //    Debug.Log("idle true");
        //}

        //if (idleLvl)//varje sekund
        //{
        //    if (Time.time >= nextUpdate)
        //    {
        //        nextUpdate = Mathf.FloorToInt(Time.time) + 1;
        //        GameObject newFlot = GameObject.Instantiate(floatingCrystal, crystalPos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("CrySecParent").transform);
        //        CleanCryTap();

        //    }
        //}


        if (GameController.IsIdleTrue() && !gameController.IsIdleLvlTrue())//varje sekund
        {
            if (gameController.ReturnSecBeforeClick() == 60)
            {
                if (Time.time >= nextUpdate)
                {
                    nextUpdate = Mathf.FloorToInt(Time.time) + 60;

                    GameObject newFlot = GameObject.Instantiate(floatingCrystal, crystalPos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("CryParent").transform);
                    CleanCry();
                }
            }
            else if (gameController.ReturnSecBeforeClick() == 45)
            {
                if (Time.time >= nextUpdate)
                {
                    nextUpdate = Mathf.FloorToInt(Time.time) + 45;

                    GameObject newFlot = GameObject.Instantiate(floatingCrystal, crystalPos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("CryParent").transform);
                    CleanCry();
                }
            }
            else if (gameController.ReturnSecBeforeClick() == 30)
            {
                if (Time.time >= nextUpdate)
                {
                    nextUpdate = Mathf.FloorToInt(Time.time) + 30;

                    GameObject newFlot = GameObject.Instantiate(floatingCrystal, crystalPos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("CryParent").transform);
                    CleanCry();
                }
            }
            else if (gameController.ReturnSecBeforeClick() == 15)
            {
                if (Time.time >= nextUpdate)
                {
                    nextUpdate = Mathf.FloorToInt(Time.time) + 15;

                    GameObject newFlot = GameObject.Instantiate(floatingCrystal, crystalPos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("CryParent").transform);
                    CleanCry();
                }
            }
        }

        //currentCrystals = GameController.GetCrystals();
        //if(currentCrystals > crystals)
        //{
        //    GameObject newFlot = GameObject.Instantiate(floatingCrystal, crystalPos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("CryParent").transform);
        //    CleanCryTap();
        //    crystals = currentCrystals;
        //}

    }

    //public void DustTrans(Transform transform)
    //{
    //    dustTrans = transform;
    //}


    public void CrystalTrans(Transform transform)
    {
        crystalTrans = transform;
    }

    public void CleanCry()
    {

        int numKids = cryParent.transform.childCount;

        if (numKids > 0)
        {
            for (int i = 0; i < numKids; i++)
            {
                GameObject prefab = cryParent.transform.GetChild(i).gameObject;
                Destroy(prefab, timer);

            }
        }
    }

    //public void CleanCryTap()
    //{

    //    int numKids = tapCryParent.transform.childCount;

    //    if (numKids > 0)
    //    {
    //        for (int i = 0; i < numKids; i++)
    //        {
    //            GameObject prefab = tapCryParent.transform.GetChild(i).gameObject;
    //            Destroy(prefab, timer);

    //        }
    //    }
    //}

    //public void CleanDust()
    //{

    //    int numKids = dustParent.transform.childCount;

    //    if (numKids > 0)
    //    {
    //        for (int i = 0; i < numKids; i++)
    //        {
    //            GameObject prefab = dustParent.transform.GetChild(i).gameObject;
    //            Destroy(prefab, timer);

    //        }
    //    }
    //}
}
