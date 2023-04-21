using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public GameObject[] enemyList;

    private bool isListEmpty = false;
    private int nullCounter = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Mst kolla om den finns / redan åkt.
        //InScene

        for (int i = 0; i < enemyList.Length; i++)
        {
            nullCounter++;
        }

        while (enemyList.Length > 0) 
        {
            int i = Random.Range(0, enemyList.Length);

            enemyList[i].GetComponent<RaidEnemyMovement>().timeToMove = true;
            enemyList[i] = null;
            Debug.Log(enemyList[i].GetComponent<RaidEnemyMovement>().timeToMove);


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
            
     
