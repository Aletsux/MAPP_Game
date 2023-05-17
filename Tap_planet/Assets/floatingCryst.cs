using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingCryst : MonoBehaviour
{
    public GameObject floatingCrystal;
    public GameObject floatingStarDust;

    private Transform crystalTrans;
    private Transform dustTrans;


    public GameObject crystalPos;
    public GameObject dustPos;

    private int nextUpdate = 1;
    public GameObject cryParent;
    public GameObject dustParent;

    public GameObject GC;
    private GameController gameController;

    private int starDust;
    private int currentDust;

    private float timer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GC.GetComponent<GameController>();
        starDust = GameController.GetStardust();
    }

    //private void OnEnable()
    //{
    //    gameController = GC.GetComponent<GameController>();
    //}

    // Update is called once per frame
    void Update()
    {

        if (gameController.IsIdleLvlTrue())//varje sekund
        {
            if (Time.time >= nextUpdate)
            {
                nextUpdate = Mathf.FloorToInt(Time.time) + 1;
                GameObject newFlot = GameObject.Instantiate(floatingCrystal, crystalPos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("CryParent").transform);
                CleanCry();

            }
        }


        if (GameController.IsIdleTrue())//varje sekund
        {
            if (Time.time >= nextUpdate)
            {
                nextUpdate = Mathf.FloorToInt(Time.time) + gameController.ReturnSecBeforeClick();

                GameObject newFlot = GameObject.Instantiate(floatingCrystal, crystalPos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("CryParent").transform);
                CleanCry();
            }
        }

        currentDust = GameController.GetStardust();
        if (currentDust > starDust)
        {
            GameObject newFlot = GameObject.Instantiate(floatingStarDust, dustPos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("DustParent").transform);
            CleanDust();
            starDust = currentDust;
        }
    }

    public void DustTrans(Transform transform)
    {
        dustTrans = transform;
    }


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

    public void CleanDust()
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
}
