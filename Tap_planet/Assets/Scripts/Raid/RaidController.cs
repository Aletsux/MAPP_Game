using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidController : MonoBehaviour
{
    public SceneChange sceneChange;
    public ActivatePanel activateRaidPanel;
    public ActivatePanel activateMissedRaidPanel;

    //new ActivatePanel activatePanel;
    //new MissedRaid missedRaid;

    public DateTime lastSaveTime;
    public int timeSinceQuit;

    public int timeBeforeRaid = 30;
    public int timeBeforeMiss = 3600;

    public static int howManyRaids;

    public static bool raidToggle;


    //public GameObject raidPanel;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnApplicationFocus(bool focus)
    {
        if (focus) //if player enters game
        {
            timeSinceQuit = calculateSecondsSinceQuit();
            Debug.Log("TimeSincequit: " + timeSinceQuit);
            Debug.Log(PlayerPrefs.GetInt("RaidToggle"));
            //PlayerPrefs.SetInt("RaidToggle");

            if (timeSinceQuit > timeBeforeRaid && timeSinceQuit < timeBeforeMiss || PlayerPrefs.GetInt("RaidToggle") == 1 && timeSinceQuit < timeBeforeMiss) // if time since last save is larger than tBR (def: 30) & less than tBM
            {
                activateRaidPanel.Toggle(true);
                activateMissedRaidPanel.Toggle(false);
                PlayerPrefs.SetInt("RaidToggle", 1);
                //raidPanel.SetActive(true);
                // You have been raided popup.
                toggleRaid();
                Debug.Log("You logged in during the raid and have to defend");
            }

            else if (timeSinceQuit < timeBeforeRaid) // LOGIN BEFORE RAID START
            {
                Debug.Log("You logged in before a raid begun, nice!");
                return;
            }

            else if (timeSinceQuit > timeBeforeMiss) // LOGIN AFTER RAID DURATION (MISS)
            {
                howManyRaids = 1;

                if (timeSinceQuit >= 2*timeBeforeMiss)
                {
                    howManyRaids = 2;
                }
                if (timeSinceQuit >= 3 * timeBeforeMiss)
                {
                    howManyRaids = 3;
                }
                if (timeSinceQuit >= 20 * timeBeforeMiss)
                {
                    howManyRaids = 4;
                }

                activateMissedRaidPanel.Toggle(true);
                activateRaidPanel.Toggle(false);
                //missedRaidPanel.SetActive(true);
                gameObject.GetComponent<MissedRaid>().CalculateRaidLoss(howManyRaids);
                Debug.Log("how many raids:" + howManyRaids);
                Debug.Log("You missed the raid, 2 lazy");
            }

            else
            {
                return;
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

    private static void toggleRaid()
    {
        if (raidToggle == false)
        {
            raidToggle = true;
        }
    }
}
