using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetState : MonoBehaviour
{
    public static int HP;
    public int maxHP = 3;
    private static Slider healthBar;
    public static bool attacked;
    private float healthBarTimer;

    void Start()
    {
        healthBar = gameObject.GetComponentInChildren<Slider>();
        HP = maxHP;
        healthBar.value = HP;
        DisplayHealthBar(true);
    }

    void Update()
    {
        if (attacked && RaidState.beginRaid)
        {
            healthBarTimer += Time.deltaTime;
            if (healthBarTimer >= 1)
            {
                DisplayHealthBar(false);
                healthBarTimer = 0f;
                attacked = false;
            }
        }
    }

    public static void DecreaseHP()
    {
        DisplayHealthBar(true);
        HP--;
        healthBar.value = HP;
        attacked = true;
    }

    public static void DisplayHealthBar(bool b)
    {
        healthBar.gameObject.SetActive(b);
    }
}
