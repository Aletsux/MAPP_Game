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
    public GameObject tutorialPanel;
    public EnemyAI enemyAI;

    public static bool beginRaid;
    private float raidTimer;
    public float raidTime = 15;

    public GameController gc;

    void Start()
    {
        PlayerPrefs.SetInt("TutorialCleared", 0);

        enemiesKilled = 0;
        beginRaid = false;
        raidOverPanel.SetActive(true);
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
        if (PlayerPrefs.GetInt("TutorialCleared") == 0)
        {
            PanelManager.AddPanelToQueue(tutorialPanel);
            PlayerPrefs.SetInt("TutorialCleared", 1);

            raidOverPanel.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
            gameOverPanel.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);

            raidOverPanel.transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            gameOverPanel.transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
        }

        if (MotherShip.HP <= 0)
        {
            if (enemiesKilled == 0)
            {
                enemiesKilled++;
            }
            enemiesKilled *= 100;
            motherShipBonusText.text = "x" + 100;
            //raidOverPanel.GetComponent<PanelAnimation>().StretchPanel();
            PanelManager.AddPanelToQueue(raidOverPanel, true);
        }
        else if (PlanetState.HP <= 0)
        {
            enemiesKilled = -100;
            //gameOverPanel.GetComponent<PanelAnimation>().StretchPanel();
            PanelManager.AddPanelToQueue(gameOverPanel, true);
        }
        else
        {
            //raidOverPanel.GetComponent<PanelAnimation>().StretchPanel();
            PanelManager.AddPanelToQueue(raidOverPanel, true);
        }
        int crystals = enemiesKilled * 10 * GameController.GetClickLvl();
        int stardust = enemiesKilled;

        crystalsWon.text = crystalsLost.text = FormatNumbers.FormatInt(crystals);
        stardustWon.text = crystalsLost.text = FormatNumbers.FormatInt(stardust);

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
}