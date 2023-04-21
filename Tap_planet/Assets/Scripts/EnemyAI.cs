using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public GameObject[] enemyList;

    private bool isListEmpty = false;
    private int nullCounter = 0;
    private bool launch = false;
    private int shipCount;

    private float minDelay = 0.3f;
    private float maxDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Mst kolla om den finns / redan �kt.
        //InScene

        trackShips();

        //Using nullCounter to track number of elements in realtime
        if (launch && shipCount > 0)
        {
            //launchEnemies();
            StartCoroutine(launchWithDelay());
        }

    }
    //Triggers from begin raid button
    public void startLaunching()
    {
        Debug.Log("LAUNCH!");
        launch = true;
    }

    public void launchEnemies()
    {

        while (shipCount > 0)
        {
            int i = Random.Range(0, enemyList.Length);
            Debug.Log("ShipNr " + i);

            enemyList[i].GetComponent<RaidEnemyMovement>().timeToMove = true;
            enemyList[i] = null;

            Debug.Log(enemyList[i].GetComponent<RaidEnemyMovement>().timeToMove);


        }
    }

    IEnumerator launchWithDelay()
    {
        int j = Random.Range(0, enemyList.Length);
        float delay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(delay);
        Debug.Log("ShipNr " + j);

        if (enemyList[j] != null)
        {
            enemyList[j].GetComponent<RaidEnemyMovement>().timeToMove = true;
            enemyList[j] = null;
        }

    }

    private int trackShips()
    {
        for (int i = 0; i < enemyList.Length; i++)
        {
            if (enemyList[i] == null)
            {
                GameObject[] newEnemyList = new GameObject[enemyList.Length - 1];
                int index = 0;
                for (int j = 0; j < enemyList.Length; j++)
                {
                    if (j != i && enemyList[j] != null)
                    {
                        newEnemyList[index] = enemyList[j];
                        index++;
                    }
                }
                enemyList = newEnemyList;
            }
        }
        shipCount = enemyList.Length;
        return shipCount;
    }

}
//int number = Random.Range(0, enemyList.Length);

//this.enemyList[number].GetComponent<RaidEnemyMovement>().timeToMove = true;



//for (int i = 0; i<enemyList.Length; i++)

//int random = Random.Range(0, enemyList.Length);


//if (enemyList[random] != null && !enemyList[random].GetComponent<RaidEnemyMovement>().timeToMove) 

//  enemyList[random].GetComponent<RaidEnemyMovement>().timeToMove = true;
//Debug.Log(enemyList[random]);


