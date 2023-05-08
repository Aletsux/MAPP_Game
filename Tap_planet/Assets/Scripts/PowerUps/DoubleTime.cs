using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTime : MonoBehaviour, IPowerUp
{
    public string name { get; set; }
    public float duration { get; set; }
    public static bool isActive { get; set; }
    public long cost { get; set; }
    public long defaultValue { get; set; }
    public float timer { get; set;}
    public long currentCrystals {get;}

    public GameObject gameController;
    private GameController gc;
    private bool activateDT = false;
    private int numPerSec;
    private int numPerTime;
    private float interval = 1;
    private float perSec = 1;

    //Constructor set default values from Interface
    public DoubleTime() {
        name = "DoubleTime";
        isActive = false; 
        cost = 100;
        
        timer = 0f;
        duration = 10f;
        currentCrystals = GameController.GetCrystals();
    }

    public static bool getIsActive() {
        return isActive;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Entered start method!");
        gc = gameController.GetComponent<GameController>();
        
        numPerSec = gc.ReturnClicksPerSec();
        numPerTime = gc.ReturnClickPerTime();
    }

    private void Update() {
        if(activateDT) {
            Activate();
            
            timer += Time.deltaTime;
            if(timer >= duration) {
                Deactivate();
                timer = 0;
            }
        }
    }

    public void Activate() {
        activateDT = true;
        isActive = true;
        DoubleIdleClick();
    }
    
    public void DoubleIdleClick() {
        //check whether IdleClick is being used
        if(gc.IsIdleTrue()) {
            if(Time.time >= interval) {
                Debug.Log("INCREASE AT INTERVAL!");
                interval = Mathf.FloorToInt(Time.time) + gc.ReturnSecBeforeClick();
                GameController.AddCrystals(gc.ReturnClickPerTime() * 2); //add numPerTime * 2 each interval
                gc.crystalAmount.text = currentCrystals + ""/*suffix*/;  
            }
            
        }

        //Check for IdleLvl -> click each second
        if (gc.IsIdleLvlTrue())
        {
            Debug.Log("INCREASE PER SEC!");
            if (Time.time >= perSec)
            {
                perSec = Mathf.FloorToInt(Time.time) + 1;
                GameController.AddCrystals(gc.ReturnClicksPerSec() * 2); //add numPerSec * 2 each second
                gc.crystalAmount.text = currentCrystals + ""/*suffix*/;
            }
        }
    }

    public void Deactivate() {
        activateDT = false;
        isActive = false;
    }

    public void RestoreState() {

    }
}
