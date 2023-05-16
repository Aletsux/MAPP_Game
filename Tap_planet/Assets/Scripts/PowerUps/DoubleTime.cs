using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class DoubleTime : PowerUpHandler, IPowerUp
{
    [Space]
    [SerializeField] private string _name;
    [SerializeField] private float _duration;
    [SerializeField] private bool _isActive;
    [SerializeField] private int _cost;
    [SerializeField] private float _timer;
    [SerializeField] private long _multiplier; //in percentage, base: 100 = +0%, 120 = +20%...

    //Variables from Interface
    public string name { get; set; }
    public float duration {get; set; }
    public static bool isActive { get; set; }
    private static int cost { get; set; }
    public long defaultValue { get; set; }
    public float timer { get; set;}
    public long currentCrystals {get; set;}
    public long multiplier {get; set;}

    //Unique variables
    private bool activateDT = false;
    private int numPerSec;
    private int numPerTime;
    private float interval = 1;
    private float perSec = 1;

    // Start is called before the first frame update
    void Start()
    {
        gc = gameController.GetComponent<GameController>();
        name = _name; //DoubleTime
        isActive = _isActive;
        multiplier = _multiplier;
        cost = PlayerPrefs.GetInt("doubletimeCost", 100);
        
        timer = _timer;
        duration = _duration;
        currentCrystals = GameController.GetCrystals();
        
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
                GameController.AddCrystals(gc.ReturnClickPerTime() * multiplier/100);
            }
        }

        //Check for IdleLvl -> click each second
        if (gc.IsIdleLvlTrue())
        {
            if (Time.time >= perSec)
            {
                perSec = Mathf.FloorToInt(Time.time) + 1;
                GameController.AddCrystals(gc.ReturnClicksPerSec() * multiplier/100); 
                Debug.Log("Multiplier: " + (long) multiplier);
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

    public static void IncreaseCost() {
        cost = (int) Math.Ceiling(cost * 1.2); //increase cost by 20% 
    }
    public static int GetCost() {
        return cost;
    }

    public static void SetCost(int amount) {
        cost = amount;
    }
}
