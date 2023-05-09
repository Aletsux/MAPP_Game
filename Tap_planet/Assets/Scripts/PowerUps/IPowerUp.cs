using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUp 
{
    public string name {get; set;}
    public float duration {get; set;}
    public static bool isActive {get; set;}
    public long cost {get; set;}
    public long defaultValue{get; set;}
    public float timer {get; set;}
    public long currentCrystals {get; set;}

    public void Activate();
    
    public void Deactivate();

    public void RestoreState();

    public void IncreaseCost();

    //???
    public void UpdateDuration() {
        timer += Time.time;
        if(timer >= duration) {

        }
    }
}
