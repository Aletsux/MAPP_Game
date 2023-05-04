using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingPoints : MonoBehaviour
{
    public GameObject pointParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Make number appear on click position
    //Input.mousePosition probably not work on touch?
    public void showFloatingPoints() {
        long amount = 1 * GameController.GetClickLvl();
        Vector3 spawnLocation = new Vector3(Random.Range(-500,500), 0f, 0f); 
        
        Vector3 clickPos = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(clickPos);
        worldPosition.y = 0;

        pointParent.GetComponentInChildren<TextMeshProUGUI>().text = amount.ToString();

        GameObject points = Instantiate(pointParent, spawnLocation, Quaternion.identity) as GameObject; //instantiate text object
        points.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false); //Sets position?

        //points.transform.localPosition += worldPosition + spawnLocation; //Values change, no difference visually
        Debug.Log("Pos: " + points.transform.localPosition);
        Destroy(points, 1f);
    }
}
