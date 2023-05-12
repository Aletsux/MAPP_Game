using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WipeEnemiesPowerup : MonoBehaviour
{
    private Text amount;

    void Start()
    {
        if (PlayerPrefs.GetInt("WipeEnemiesAmount") == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            amount.text = PlayerPrefs.GetInt("WipeEnemiesAmount").ToString();
        }
    }
}
