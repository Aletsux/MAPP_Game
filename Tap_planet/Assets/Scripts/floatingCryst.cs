using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingCryst : MonoBehaviour
{
    public GameObject floatingThing;
    private Transform trans;

    public GameObject crystalPos;
    private int nextUpdate = 1;
    public GameObject cryParent;

    public GameObject GC;
    private GameController gameController;

    private float timer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GC.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameController.IsIdleLvlTrue())//varje sekund
        {
            if (Time.time >= nextUpdate)
            {
                nextUpdate = Mathf.FloorToInt(Time.time) + 1;
                GameObject newFlot = GameObject.Instantiate(floatingThing, crystalPos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("CryParent").transform);
                CleanCry();

            }
        }


        if (GameController.IsIdleTrue())//varje sekund
        {
            if (Time.time >= nextUpdate)
            {
                nextUpdate = Mathf.FloorToInt(Time.time) + gameController.ReturnSecBeforeClick();

                GameObject newFlot = GameObject.Instantiate(floatingThing, crystalPos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("CryParent").transform);
                CleanCry();
            }
        }
    }


    public void Bought(Transform transform)
    {
        trans = transform;
       
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
}
