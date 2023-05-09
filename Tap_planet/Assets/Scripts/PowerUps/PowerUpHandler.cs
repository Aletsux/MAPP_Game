using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpHandler : MonoBehaviour
{
    //Handle cost increase -> another script (storescript?)
    //Trigger the different PowerUps 
    
    //Serialize the references to obj, using IPowerUp
    //[SerializeReference] public IPowerUp doubleTimePowerUP;
    public GameObject gameController;
    protected GameController gc;
    public List<IPowerUp> allPowerups = new List<IPowerUp>();
    public Button testButton;
    private Text buttonText;
    IPowerUp powerUp;
    //public GameObject[] powerUpList = new GameObject[0];
    private void Start() {
        gc = gameController.GetComponent<GameController>();
        buttonText = testButton.GetComponentInChildren<Text>();
        if(buttonText == null) {
            Debug.Log("ButtonTextNull!");
        }
        LoadAllPowerUps();
    }

    private void Update() {
        
        UpdateText();
        
    }

    //Puts all scripts using the interface IPowerUp in a list (allPowerups)
    private void LoadAllPowerUps() {
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();
        for(int i = 0; i < allScripts.Length; i++) {
            if(allScripts[i] is IPowerUp) {
                allPowerups.Add(allScripts[i] as IPowerUp); //Adds all scripts using IPowerUp to list
            }
        }
        Debug.Log("allPowerups: " + allPowerups.Count);
    }

    //Reference for button activation
    public void OnClick(string name) {
        for(int i = 0; i < allPowerups.Count; i++) {
            powerUp = allPowerups[i];
            Debug.Log("Powerup name: " + powerUp.name);
            if(allPowerups[i].name.Equals(name)) {
                powerUp.Activate();
            }
        }
    }
    
    private void UpdateText() {
        if (IPowerUp.isActive)
        {
            buttonText.text = "Active";
            testButton.enabled = false;
        }
        else
        {
            buttonText.text = "TestPU";
            testButton.enabled = true;
        }
    }

    //Base for all powerups
    protected int GetClickLvl() {
        return 1 * GameController.GetClickLvl();
    }

    protected int GetCrystalsPerSec() {
        return gc.ReturnClicksPerSec();
    }

    protected int GetCrystalsPerInterval() {
        return gc.ReturnClickPerTime();
    }

 }
