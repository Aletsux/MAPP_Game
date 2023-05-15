using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaidPowerup : MonoBehaviour
{
    public int healthBoostAmount;
    public PlanetState planetState;
    public static Slider healthBar;
    public int newHP = 5;
    public Button healthBoostButton;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("healthBoostAmount", 1);
        healthBoostAmount = PlayerPrefs.GetInt("healthBoostAmount");
        healthBar = gameObject.GetComponentInChildren<Slider>();

        if (healthBoostAmount <= 0)
        {
            healthBoostButton.interactable = false;
        }
        else
        {
            healthBoostButton.interactable = true;
            healthBoostButton.GetComponentInChildren<Text>().text = healthBoostAmount.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateHealthBoost()
    {
            healthBoostAmount--;
            planetState.maxHP = newHP;
            planetState.BoostHP(newHP);
            healthBar.maxValue = newHP;
            healthBoostButton.interactable = false;
            healthBoostAmount--;
            PlayerPrefs.SetInt("healthBoostAmount", healthBoostAmount);

    }
}
