using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public GameObject[] enemyList;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Mst kolla om den finns / redan åkt.
        //InScene

        int number = Random.Range(0, enemyList.Length);

        enemyList[number].GetComponent<RaidEnemyMovement>().timeToMove = true;




        //int number = Random.Range(0, enemyList.Length ); 
    }


            //for (int i = 0; i<enemyList.Length; i++)
        
            //int random = Random.Range(0, enemyList.Length);


            //if (enemyList[random] != null && !enemyList[random].GetComponent<RaidEnemyMovement>().timeToMove) 
            
              //  enemyList[random].GetComponent<RaidEnemyMovement>().timeToMove = true;
                //Debug.Log(enemyList[random]);
            
        
}
