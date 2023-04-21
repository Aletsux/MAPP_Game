using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{

    public GameObject[] enemyList;

    private bool isListEmpty = false;
    private int nullCounter = 0;

    public Button startButton;

    private float waitTime = 0.1f;
    private float timer = 0.0f;

    static int initialLength;

    int attackIndex = 0;



    // Start is called before the first frame update
    void Start()
    {

     initialLength = enemyList.Length;

    }

// Update is called once per frame
void Update()
    {

        timer += Time.deltaTime;


        if(timer > waitTime)
        {
            beginEnemyAttack();
            timer = timer - (waitTime + 0.5f);
        }

        bool isEmpty = !enemyList.Any();
        if (isEmpty)
        {
            isListEmpty = true;
        }

    }

    public void beginEnemyAttack()
    {
        enemyList[attackIndex].GetComponent<RaidEnemyMovement>().timeToMove = true;
        attackIndex++;


    }


    public void beginEnemyAI()
    {
        while (!isListEmpty)
        {
            
                int i = Random.Range(0, initialLength);


                if (enemyList[i] != null)
                {
                    enemyList[i].GetComponent<RaidEnemyMovement>().timeToMove = true;
                    enemyList[i] = null;
                }
                else
                {
                    return;

                    //GameObject[] newEnemyList = new GameObject[enemyList.Length - 1];

                    //for (int j = 0; j < newEnemyList.Length; j++)
                    //{
                    //    if (enemyList[j] != null)
                    //    {
                    //        newEnemyList[i] = enemyList[j];
                    //    }
                    //}

                    //enemyList = newEnemyList;
                    //initialLength = newEnemyList.Length;

                }

    


        }
    }


    //int i = Random.Range(0, initialLength);
    //enemyList[i].GetComponent<RaidEnemyMovement>().timeToMove = true;

    //enemyList[i] = enemyList[initialLength];
    //enemyList[initialLength] = null;
    //initialLength--;




}
//int number = Random.Range(0, enemyList.Length);

//this.enemyList[number].GetComponent<RaidEnemyMovement>().timeToMove = true;



//for (int i = 0; i<enemyList.Length; i++)

//int random = Random.Range(0, enemyList.Length);


//if (enemyList[random] != null && !enemyList[random].GetComponent<RaidEnemyMovement>().timeToMove) 

//  enemyList[random].GetComponent<RaidEnemyMovement>().timeToMove = true;
//Debug.Log(enemyList[random]);


