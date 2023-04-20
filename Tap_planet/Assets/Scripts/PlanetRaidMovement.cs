using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRaidMovement : MonoBehaviour
{
    public Transform target;
    public float speed;

    public bool isRaidActive = false;
    public bool raidBegins = false;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.transform.position = new Vector3(0, -1100, 0);

    }

    // Update is called once per frame
    void Update()
    {

        if (isRaidActive)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

        if (transform.position == target.position)
        {
            raidBegins = true;
        }
    }

    public void beginRaid()
    {
        raidBegins = true;
    }
}
