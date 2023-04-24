using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidController : MonoBehaviour
{
    public SceneChange sceneChange;
    public ActivatePanel activateRaidPanel;
    public ActivatePanel activateMissedRaidPanel;

    public ActivatePanel activatePanel;
    public MissedRaid missedRaid;

    public GameObject missedRaidPanel;
    public GameObject raidPanel;

    public DateTime lastSaveTime;
    public int timeSinceQuit;

    public int timeBeforeRaid = 30;
    public int timeBeforeMiss = 3600;
    

    // Start is called before the first frame update
    void Start()
    {
        //timeBeforeMiss = timeBeforeRaid + 3600;
}

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationFocus(bool focus)
    {
        if (focus) //if player enters game
        {
            //timeSinceQuit = calculateSecondsSinceQuit();
            Debug.Log(timeSinceQuit);
            if ((timeSinceQuit > timeBeforeRaid) && (timeSinceQuit < timeBeforeMiss)) // if time since last save is larger than tBR (def: 30) & less than tBM
            {
                raidPanel.SetActive(true);// You have been raided popup.
                Debug.Log("You logged in during the raid and have to defend");
            }

            else if (timeSinceQuit < timeBeforeRaid) // LOGIN BEFORE RAID START
            {
                Debug.Log("You logged in before a raid begun, nice!");
                return;
            }

            else if (timeSinceQuit > timeBeforeMiss) // LOGIN AFTER RAID DURATION (MISS)
            {
                activateMissedRaidPanel.Toggle();
                //missedRaidPanel.SetActive(true);
                missedRaid.CalculateRaidLoss();
                Debug.Log("You missed the raid, 2 lazy");
            }

        }
        else
        {
            lastSaveTime = System.DateTime.Now;
            Debug.Log(lastSaveTime.ToString());
        }
    }

    private int calculateSecondsSinceQuit()
    {
        DateTime currentDate = System.DateTime.Now; //Store the current time
        long temp = Convert.ToInt64(PlayerPrefs.GetString("quitTime")); //Grab the old time from the player prefs as a long
        DateTime quitTime = DateTime.FromBinary(temp); //Convert the old time from binary to a DataTime variable
        TimeSpan difference = currentDate.Subtract(quitTime); //Use the Subtract method and store the result as a timespan variable
        return (int)difference.TotalSeconds; // return the difference as an int
    }
}
