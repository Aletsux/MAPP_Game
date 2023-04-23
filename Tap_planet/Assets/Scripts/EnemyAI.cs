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

    private float minDelay = 1f;
    private float maxDelay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        //Set default value for amount of ships
        shipCount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //Mst kolla om den finns / redan �kt.
        //InScene

        //trackShipCount();


        if (launch && shipCount > 0)
        {
            Debug.Log("Start!");
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
/* 
    public void launchEnemies()
    {        while (shipCount > 0)
        {
            int i = Random.Range(0, enemyList.Length);
            Debug.Log("ShipNr " + i);

            enemyList[i].GetComponent<RaidEnemyMovement>().timeToMove = true;
            enemyList[i] = null;

            Debug.Log(enemyList[i].GetComponent<RaidEnemyMovement>().timeToMove);

        }
    } */

    IEnumerator launchWithDelay()
    {
        int j = Random.Range(0, enemyList.Length);

        Debug.Log("ShipNr " + j);

        if (enemyList[j] != null)
        {
            enemyList[j].GetComponent<RaidEnemyMovement>().timeToMove = true;
            enemyList[j] = null;
            shipCount--;
        }
        float delay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(delay);

    }



}
//int number = Random.Range(0, enemyList.Length);

//this.enemyList[number].GetComponent<RaidEnemyMovement>().timeToMove = true;



//for (int i = 0; i<enemyList.Length; i++)

//int random = Random.Range(0, enemyList.Length);


//if (enemyList[random] != null && !enemyList[random].GetComponent<RaidEnemyMovement>().timeToMove) 

//  enemyList[random].GetComponent<RaidEnemyMovement>().timeToMove = true;
//Debug.Log(enemyList[random]);


