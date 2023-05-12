using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public GameObject[] enemyList;
    public GameObject shipParent;

    private static List<GameObject> activeEnemies = new();

    //private bool isListEmpty = false;
    //private int nullCounter = 0;
    private bool launch = false;
    private int shipCount;

    private float minDelay = 0.5f;
    private float maxDelay = 1f;

    public void deactivate() {
        Debug.Log("Deactivated!");
        //Debug.Log(enemyList.Length);
        launch = false;
        shipParent.SetActive(false);

        for (int i = 0; i < enemyList.Length; i++) {
            if(enemyList[i] != null) {
                GameObject ship = enemyList[i];
                ship.SetActive(false);
            }
        }
    }

    void Start()
    {
        //Set default value for amount of ships
        shipCount = enemyList.Length;

        for (int i = 0; i < enemyList.Length; i++)
        {
            if (enemyList[i] != null)
            {
                GameObject ship = enemyList[i];
                ship.SetActive(true);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    //Triggers from begin raid button

    //Start a recursive call
    public void startLaunching()
    {
        Debug.Log("LAUNCH!");
        launch = true;
        //startRaid();
        launchShips();
    }

    private void launchShips() {
        if(launch) {
            
            float delay = Random.Range(minDelay, maxDelay);
            StartCoroutine(launchWithDelay(delay));
            //Debug.Log(shipCount);
            
        }
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
   

    IEnumerator launchWithDelay(float delay)
    {
        //Only available ships
        int j = Random.Range(0, shipCount);

        if (enemyList[j] != null)
        {
            AddToActiveList(enemyList[j]);
            enemyList[j].GetComponent<RaidEnemyMovement>().timeToMove = true;
            enemyList[j] = null;
            //Use the same delay as the initial delay
            yield return new WaitForSeconds(delay);
        } else {
            j = Random.Range(0, shipCount);
        }
        
        if(checkShips() > 0) {
            launchShips();
        }
        
    }
    
    public int checkShips() {
        List<int> availableShips = new List<int>();
        for(int i = 0; i < enemyList.Length; i++) {
            if(enemyList[i] != null) {
                availableShips.Add(i);
            }
        }
        return availableShips.Count;
    }


    public static void AddToActiveList(GameObject toAdd)
    {
        activeEnemies.Add(toAdd);
    }
    public static void RemoveFromActiveList(GameObject toRemove)
    {
        activeEnemies.Remove(toRemove);
    }
    public void DestroyActiveEnemies()
    {
        while (activeEnemies.Count != 0)
        {
            activeEnemies[0].GetComponent<RaidEnemyKillable>().destroyEnemy();
        }
    }
}
//int number = Random.Range(0, enemyList.Length);

//this.enemyList[number].GetComponent<RaidEnemyMovement>().timeToMove = true;



//for (int i = 0; i<enemyList.Length; i++)

//int random = Random.Range(0, enemyList.Length);


//if (enemyList[random] != null && !enemyList[random].GetComponent<RaidEnemyMovement>().timeToMove) 

//  enemyList[random].GetComponent<RaidEnemyMovement>().timeToMove = true;
//Debug.Log(enemyList[random]);


