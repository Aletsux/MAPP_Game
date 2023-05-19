using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidEnemyKillable : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject cameraShake;
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
        gameObject.GetComponent<EnemyDeathSound>().PlayDeathSound();
        EnemyAI.RemoveFromActiveList(gameObject);
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation, GameObject.FindAnyObjectByType<Canvas>().transform);// skapa explosion (viktigt: under canvas)
        explosion.transform.SetSiblingIndex(1);
        Debug.Log("Hit");
        gameObject.SetActive(false);
        gameObject.GetComponent<RaidEnemyMovement>().enemyCleared = true;
        RaidState.enemiesKilled++;
        cameraShake.GetComponent<ScreenShake>().Shake();
    }
}
