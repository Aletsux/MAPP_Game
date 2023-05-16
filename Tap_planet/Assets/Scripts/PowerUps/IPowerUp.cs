using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUp 
{
    //Copy these variables into powerup script, declare variables in start method of navtively 
    public string name {get; set;}
    public float duration {get; set;}
    public static bool isActive {get; set;}
    public static int cost {get; set;}
    public long defaultValue{get; set;}
    public float timer {get; set;}
    public long currentCrystals {get; set;}
    public long multiplier {get; set;}

    public void Activate();
    
    public void Deactivate();

    public void RestoreState();

    public static void IncreaseCost() {}
    public static int GetCost() {return cost;}
}
