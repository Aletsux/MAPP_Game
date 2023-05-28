using System;
using UnityEngine;
using Random = System.Random;


public class RaidController : MonoBehaviour
{
    private static Random rnd = new Random();
    public SceneChange sceneChange;
    
    public GameObject raidPanel;
    public GameObject missedRaidPanel;

    public DateTime lastSaveTime;
    public int timeSinceQuit;

    public static int timeBeforeRaid = 10;
    public int timeBeforeMiss = 60;

    public static int howManyRaids;

    public static bool raidToggle;

    private float autoRaidTimer;
    public float raidTime = 100f;

    private bool tutorialClear = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("TutorialCleared") == 0)
        {
            raidTime = 60;
        }
        else
        {
            raidTime = rnd.Next(60, 180);
            timeBeforeRaid = rnd.Next(3600, 10800);
        }
        autoRaidTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        autoRaidTimer += Time.deltaTime;

        if (autoRaidTimer >= raidTime)
        {
            activateAutoRaid();
        }
    }

    void OnApplicationFocus(bool focus)
    {
        if (focus && tutorialClear) //if player enters game
        {
            timeBeforeRaid = PlayerPrefs.GetInt("timeBeforeRaid");

            timeSinceQuit = GameController.calculateSecondsSinceQuit();
            Debug.Log("TimeSincequit: " + timeSinceQuit);
            Debug.Log(PlayerPrefs.GetInt("RaidToggle"));
            //PlayerPrefs.SetInt("RaidToggle");

            if (timeSinceQuit > timeBeforeRaid && timeSinceQuit < timeBeforeMiss || ((PlayerPrefs.GetInt("RaidToggle") == 1 && timeSinceQuit < timeBeforeMiss))) // if time since last save is larger than tBR (def: 30) & less than tBM
            {
                PanelManager.AddPanelToQueue(raidPanel);
                PlayerPrefs.SetInt("RaidToggle", 1);
                // You have been raided popup.
                toggleRaid();
                Debug.Log("You logged in during the raid and have to defend");
            }

            else if (timeSinceQuit < timeBeforeRaid) // LOGIN BEFORE RAID START
            {
                Debug.Log("You logged in before a raid begun, nice!");
                PlayerPrefs.SetInt("RaidToggle", 0);
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
                gameObject.GetComponent<MissedRaid>().CalculateRaidLoss(howManyRaids);
                PanelManager.AddPanelToQueue(missedRaidPanel, true);
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
            print("raidcontroller");
            lastSaveTime = System.DateTime.Now;
            Debug.Log(lastSaveTime.ToString());
        }
    }

    private static void toggleRaid()
    {
        if (raidToggle == false)
        {
            raidToggle = true;
        }
    }

    private void activateAutoRaid()
    {
        PanelManager.AddPanelToQueue(raidPanel, true);
        PlayerPrefs.SetInt("RaidToggle", 1);
        toggleRaid();
        autoRaidTimer = 0;
        Debug.Log("bang");
    }

    // set random time 
}

