using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingDustSec : MonoBehaviour
{

    public GameObject tapCryParent;
    private float timer = 1f;

    public GameObject GC;
    private GameController gameController;

    public GameObject floatingCrystal;

    private bool idleLvl;

    private int nextUpdate = 1;

    private Transform crystalTrans;

    public GameObject crystalPos;


    // Start is called before the first frame update
    void Start()
    {
        gameController = GC.GetComponent<GameController>();
        idleLvl = gameController.IsIdleLvlTrue();
    }

    // Update is called once per frame
    void Update()
    {
        if (idleLvl)//varje sekund
        {
            if (Time.time >= nextUpdate)
            {
                nextUpdate = Mathf.FloorToInt(Time.time) + 1;
                GameObject newFlot = GameObject.Instantiate(floatingCrystal, crystalPos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("CrySecParent").transform);
                CleanCryTap();

            }
        }
    }

    public void CrystalTrans(Transform transform)
    {
        crystalTrans = transform;
    }

    public void CleanCryTap()
    {

        int numKids = tapCryParent.transform.childCount;

        if (numKids > 0)
        {
            for (int i = 0; i < numKids; i++)
            {
                GameObject prefab = tapCryParent.transform.GetChild(i).gameObject;
                Destroy(prefab, timer);

            }
        }
    }
}
