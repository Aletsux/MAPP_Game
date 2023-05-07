using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaidState : MonoBehaviour
{
    public static int enemiesKilled;

    public Text enemiesKilledText;
    public Text motherShipBonusText;
    public Text crystalsWon;
    public Text stardustWon;
    public Text crystalsLost;
    public Text stardustLost;
    public int motherShipBonus = 1;

    public GameObject raidOverPanel;
    public GameObject gameOverPanel;
    public EnemyAI enemyAI;

    public static bool beginRaid;
    private float raidTimer;
    public float raidTime = 15;

    public GameController gc;



    // Start is called before the first frame update
    void Start()
    {
        enemiesKilled = 0;
        beginRaid = false;
        raidOverPanel.SetActive(true);
        //raidOverPanel.GetComponent<PanelAnimation>().StretchPanel();

        //gameOverPanel.SetActive(true);
        //gameOverPanel.GetComponent<PanelAnimation>().StretchPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (beginRaid)
        {
            raidTimer += Time.deltaTime;
            if (raidTimer >= raidTime || MotherShip.HP <= 0 || PlanetState.HP <= 0)
            {
                print("Ended");
                RaidEnded();
                beginRaid = false;
                raidTimer = 0f;
            }
        }
    }

    public void RaidStart()
    {
        beginRaid = true;
        MotherShip.DisplayHealthBar(false);
        PlanetState.DisplayHealthBar(false);
    }

    public void RaidEnded()
    {
        beginRaid = false; // timer slutar räkna

        enemiesKilledText.text = enemiesKilled.ToString();


        if (MotherShip.HP <= 0)
        {
            if (enemiesKilled == 0)
            {
                enemiesKilled++;
            }
            enemiesKilled *= 100;
            motherShipBonusText.text = "x" + 100;
            raidOverPanel.GetComponent<PanelAnimation>().StretchPanel();
        }
        else if (PlanetState.HP <= 0)
        {
            enemiesKilled = -100;
            gameOverPanel.GetComponent<PanelAnimation>().StretchPanel();
        }
        else
        {
            raidOverPanel.GetComponent<PanelAnimation>().StretchPanel();
        }
        int crystals = enemiesKilled * 10 * GameController.GetClickLvl();
        int stardust = enemiesKilled;

        crystalsWon.text = crystalsLost.text = ReturnString(crystals);
        stardustWon.text = crystalsLost.text = ReturnString(stardust);

        //raidOverPanel.SetActive(true);zx

        GameController.AddCrystals(crystals);
        Debug.Log("HERE HERE HERE " + crystals * 10 * GameController.GetClickLvl());
        PlayerPrefs.SetString("crystals", GameController.GetCrystals().ToString());
        Debug.Log("HERE HERE HERE " + stardust);
        GameController.AddStardust(stardust);
        PlayerPrefs.SetInt("stardust", GameController.GetStardust());

        PlayerPrefs.SetInt("RaidToggle", 0);
        enemyAI.deactivate();
    }

    private string ReturnString(int amount)
    {
        double newString = amount;
        if (newString < 1000)
        {
            return newString.ToString();
        }
        else if (newString < 1000000)
        {
            return (newString / 1000).ToString("F3") + "k";
        }
        else if (newString < 1000000000)
        {
            return (newString / 1000000).ToString("F3") + "M";
        }
        else
        {
            return (newString / 1000000000).ToString("F3") + "B";
        }
    }
}