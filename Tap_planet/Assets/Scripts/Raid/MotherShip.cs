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

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        healthBar = gameObject.GetComponentInChildren<Slider>();

        HP = maxHP;
        healthBar.value = HP;
        DisplayHealthBar(true);
        button.onClick.AddListener(OnButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (RaidState.beginRaid)
        {
            DisplayHealthBar(false);
        }
        if (attacked)
        {
            if (healthBarTimer >= 1)
            {
                DisplayHealthBar(false);
                healthBarTimer = 0f;
                attacked = false;
            }
        }
        
    }

    public void OnButtonClick()
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