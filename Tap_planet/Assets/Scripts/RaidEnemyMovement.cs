using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidEnemyMovement : MonoBehaviour
{

    public float speed;
    public float newSpeed;

    private bool isHoming;
    public bool isMoving;
    public bool timeToMove = false;
    public bool enemyCleared = false;

    [SerializeField] private GameObject planet;

    public PlanetRaidMovement raidMovement;
    public PlanetState planetState;

    private float lateralPosition;

    //private Vector3 downTarget = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        lateralPosition = gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //When planet has landed in its place and raid begins, start moving downwards at speed speed.
        if (raidMovement.raidBegins && !isHoming && timeToMove)
        {
            isMoving = true;   
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -transform.position.y), step);
        }
        //When ship has passed Trigger 1, start moving towards planet at speed newSpeed.
        if (isHoming)
        {
            float newStep = newSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, planet.transform.position, newStep);
        }
    }

    //First Trigger 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SpeedIncrease")
        {
            Debug.Log("bam");
            isHoming = true;
        }

        if (collision.tag == "PlanetTrigger")
        {
            planetState.totalRaidDamage++;
            gameObject.GetComponent<RaidEnemyMovement>().enemyCleared = true;
        }
    }
}
