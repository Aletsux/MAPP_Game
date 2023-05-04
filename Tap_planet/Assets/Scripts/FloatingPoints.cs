using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public Color critical;
    private Material material;
    //private MeshRenderer renderer;
    private Color renderColor;
    public GameObject gc;
    GameController gcScript;
    private void Start() {
        material = pointParent.GetComponentInChildren<MeshRenderer>().material;
        //material = renderer.material;
        gcScript = gc.GetComponent<GameController>();
    } 
    private void Update() {
        //renderer.material.color = renderColor;
        setTextColor();
    }

    //calculate amount, isntantiate obj with random spawnLocation, destroy after 0.5f,
    public void showFloatingPoints() {
        long amount = 1 * GameController.GetClickLvl();
        
        Vector3 spawnLocation = gameObject.transform.position + new Vector3(Random.Range(xMax,xMin), Random.Range(yMax,yMin), 0f);

        Debug.Log("pointParent Pos: " + pointParent.transform.position);

        GameObject points = Instantiate(pointParent, spawnLocation, Quaternion.identity, transform); 
        
        pointParent.GetComponentInChildren<TextMeshProUGUI>().text = amount.ToString();

        Debug.Log("Pos: " + points.transform.position);
        Destroy(points, destroyTimer);
    }
    
    private void setTextColor() {
        if(gcScript.getIsUsingTPU()) {
            material.color = special;
        } 

        material.color = normal;
    }




}
