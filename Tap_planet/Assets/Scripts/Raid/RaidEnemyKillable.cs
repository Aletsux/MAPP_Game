using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidEnemyKillable : MonoBehaviour
{
    public GameObject explosionPrefab;
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
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation, GameObject.FindAnyObjectByType<Canvas>().transform); // skapa explosion (viktigt: under canvas)
        explosion.transform.SetAsFirstSibling(); // s? att de inte ?r iv?gen
        Debug.Log("Hit");
        gameObject.SetActive(false);
        gameObject.GetComponent<RaidEnemyMovement>().enemyCleared = true;
        RaidState.enemiesKilled++;
    }
}
