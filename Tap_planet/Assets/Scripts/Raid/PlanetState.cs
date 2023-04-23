using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetState : MonoBehaviour
{
    public static int totalRaidDamage;

    public Text damageCounter;

    // Start is called before the first frame update
    void Start()
    {
        totalRaidDamage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (totalRaidDamage == 0)
        {
            damageCounter.text = "";
        }
        else
        damageCounter.text = totalRaidDamage.ToString();
    }
}
