using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidEnemyKillable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Physics.queriesHitTriggers = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {

        //Debug.Log("Status");
        //gameObject.SetActive(false);
    }

    public void destroyEnemy()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<RaidEnemyMovement>().enemyCleared = true;
    }
}
