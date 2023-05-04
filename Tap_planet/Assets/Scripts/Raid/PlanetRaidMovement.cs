using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRaidMovement : MonoBehaviour
{
    //Added reference to enemy Ai to trigger enemies
    public GameObject enemyAi;
    public Transform target;

    //added reference to TriggerAi
    public GameObject triggerAi;
    //Original speed = 850
    public float speed;

    public RaidState raidState;

    //public bool isRaidActive = false;
    public bool raidBegins = false;
    public bool planetInPosition = false;
    public bool waveStart = true;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.transform.position = new Vector3(0, -1100, 0);

        //Stop the TriggerAi to move on start
        triggerAi.GetComponent<Rigidbody2D>().isKinematic = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (!planetInPosition)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            //Compare distance between planet and target position to threshold (0.01f)
            if (Vector3.Distance(transform.position, target.position) < 0.01f)
            {
                planetInPosition = true;
                beginRaid();
                raidState.RaidStart();
            }
        }
        


        //Doesn't work for some reason...
        /*   if (transform.position == target.position)
          {
              Debug.Log("In position!");
              planetInPosition = true;
          }
   */
    }

    public void beginRaid()
    {
        Debug.Log("PlanetPosition: " + planetInPosition);
        if (planetInPosition && waveStart)
        {
            raidBegins = true;

            //Added function to let button initate raid
            enemyAi.GetComponent<EnemyAI>().startLaunching();

            //Make TriggerAI start moving on button click
            triggerAi.GetComponent<Rigidbody2D>().isKinematic = false;
            waveStart = false;


        }

    }
}
