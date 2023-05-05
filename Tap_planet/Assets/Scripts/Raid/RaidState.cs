using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaidState : MonoBehaviour
{
    public static int enemiesKilled;

    public Text enemiesKilledText;
    public Text enemiesMissedText;

    public Text resultText;

    public GameObject raidOverPanel;
    public GameObject gameOverPanel;
    public EnemyAI enemyAI;

    public static bool beginRaid;
    private float raidTimer;
    public float raidTime = 15;

    public GameController gc;

    public MotherShip motherShip;


    // Start is called before the first frame update
    void Start()
    {
        enemiesKilled = 0;
        beginRaid = false;
        raidOverPanel.SetActive(true);

        gameOverPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (beginRaid)
        {
            raidTimer += Time.deltaTime;
            if (raidTimer >= raidTime || MotherShip.HP <= 0 || PlanetState.HP <= 0)
            {
                RaidEnded();
                beginRaid = false;
                raidTimer = 0f;
            }
        }
    }

    public void RaidStart()
    {
        beginRaid = true;
        //MotherShip.DisplayHealthBar(false);
        //PlanetState.DisplayHealthBar(false);
    }

    public void RaidEnded()
    {
        beginRaid = false; // timer slutar räkna
        int result = enemiesKilled;

        if (MotherShip.HP <= 0)
        {
            result = 100;
        }
        else if (PlanetState.HP <= 0)
        {
            result = -100;
            gameOverPanel.GetComponent<PanelAnimation>().StretchPanel();
        }

        enemiesKilledText.text = enemiesKilled + "";
        //enemiesMissedText.text = PlanetState.totalRaidDamage.ToString();

        // set you died message

        resultText.text = result.ToString();
        int lostCrystals = result;
        int lostStardust = result;

        //raidOverPanel.SetActive(true);
        raidOverPanel.GetComponent<PanelAnimation>().StretchPanel();

        GameController.AddCrystals(lostCrystals * 10 * GameController.GetClickLvl());
        Debug.Log("HERE HERE HERE " + lostCrystals * 10 * GameController.GetClickLvl());
        PlayerPrefs.SetString("crystals", GameController.GetCrystals().ToString());
        Debug.Log("HERE HERE HERE " + lostStardust);
        GameController.AddStardust(lostStardust);
        PlayerPrefs.SetInt("stardust", GameController.GetStardust());

        PlayerPrefs.SetInt("RaidToggle", 0);
        enemyAI.deactivate();
    }

}
