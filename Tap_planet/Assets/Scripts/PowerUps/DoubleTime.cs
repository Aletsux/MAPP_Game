using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

//Comment: Sort of works, looks scuffed, implement visual feedback
public class DoubleTime : PowerUpHandler, IPowerUp
{
    [Space]
    [SerializeField] private string _name;
    [SerializeField] private float _duration;
    [SerializeField] private bool _isActive;
    [SerializeField] private long _cost;
    [SerializeField] private float _timer;

    //Variables from Interface
    public string name { get; set; }
    public float duration {get; set; }
    public static bool isActive { get; set; }
    public long cost { get; set; }
    public long defaultValue { get; set; }
    public float timer { get; set;}
    public long currentCrystals {get; set;}


    //Unique variables
    private bool activateDT = false;
    private int numPerSec;
    private int numPerTime;
    private float interval = 1;
    private float perSec = 1;

    //Constructor set default values from Interface
    /* public DoubleTime() {
        name = "DoubleTime";
        isActive = false; 
        cost = 100;
        
        timer = 0f;
        duration = 10f;
        currentCrystals = GameController.GetCrystals();
    } */

    // Start is called before the first frame update
    void Start()
    {
        name = _name; //DoubleTime
        isActive = _isActive;
        cost = _cost;
        
        timer = _timer;
        duration = _duration;
        currentCrystals = GameController.GetCrystals();
        
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
        IPowerUp.isActive = true;
        DoubleIdleClick();
    }
    
    public void DoubleIdleClick() {
        //check whether IdleClick is being used
        if(GameController.IsIdleTrue()) {
            if(Time.time >= interval) {
                interval = Mathf.FloorToInt(Time.time) + gc.ReturnSecBeforeClick();
                GameController.AddCrystals(gc.ReturnClickPerTime() * 2); //add numPerTime * 2 each interval
            }
        }

        //Check for IdleLvl -> click each second
        if (gc.IsIdleLvlTrue())
        {
            if (Time.time >= perSec)
            {
                perSec = Mathf.FloorToInt(Time.time) + 1;
                GameController.AddCrystals(gc.ReturnClicksPerSec() * 2); //add numPerSec * 2 each second
            }
        }
    }

    public void Deactivate() {
        activateDT = false;
        isActive = false;
        IPowerUp.isActive = false;
    }

    public void RestoreState() {

    }

    public void IncreaseCost() {
        
    }
}
