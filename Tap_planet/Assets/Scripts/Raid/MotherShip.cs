using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotherShip : MonoBehaviour
{
    public static int HP;
    public int maxHP;
    private static Slider healthBar;
    private Button button;
    private bool attacked;
    private float healthBarTimer;

    void Start()
    {
        button = gameObject.GetComponent<Button>();
        healthBar = gameObject.GetComponentInChildren<Slider>();
        HP = maxHP;
        healthBar.value = HP;
        DisplayHealthBar(true);
        button.onClick.AddListener(OnButtonClick);
    }

    void Update()
    {
        if (attacked)
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

    private void OnButtonClick()
    {
        if (RaidState.beginRaid)
        {
            DisplayHealthBar(true);
            HP--;
            healthBar.value = HP;
            print(HP);
            attacked = true;
            healthBarTimer = 0f;
        }
    }

    public static void DisplayHealthBar(bool b)
    {
        healthBar.gameObject.SetActive(b);
    }
}