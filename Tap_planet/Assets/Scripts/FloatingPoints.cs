using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloatingPoints : MonoBehaviour
{
    public GameObject pointParent;
    [Space]
    //Range for spawn position
    public float xMax = 0;
    public float xMin = 0; 
    public float yMax = 0;
    public float yMin = 0;
    [Space]
    //time to destroy after instantiated
    public float destroyTimer = 1f;
    public Color normal;
    public Color special;
    //public Color critical;
    private Text text;
    public GameObject gc;
    GameController gcScript;
    
    private void Start() {
        text = pointParent.GetComponentInChildren<Text>();
        gcScript = gc.GetComponent<GameController>();
    } 

    //calculate amount, isntantiate obj with random spawnLocation, destroy after 0.5f,
    public void showFloatingPoints() {
        long amount = 1 * GameController.GetClickLvl();

        Vector3 spawnLocation = gameObject.transform.position + new Vector3(Random.Range(xMax,xMin), Random.Range(yMax,yMin), 0f);

        GameObject points = Instantiate(pointParent, spawnLocation, Quaternion.identity, transform); 
        pointParent.GetComponentInChildren<Text>().text = amount.ToString();
        setTextColor();
        Destroy(points, destroyTimer);
        
    }
    
    private void setTextColor() { //Get wether poweup is active
        if(gcScript.getIsTpuActive()) {
            Debug.Log("CHANGE COLOR!");
            text.color = special;
        } else {
            text.color = normal; 
        }
       
    }




}
