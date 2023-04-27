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
    public EnemyAI enemyAI;

    private bool beginRaid;
    private float raidTimer;
    public float raidTime = 15;

    public GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        enemiesKilled = 0;
        beginRaid = false;
        raidOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (beginRaid)
        {
            raidTimer += Time.deltaTime;
            if (raidTimer >= raidTime)
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
    }

    public void RaidEnded()
    {
        raidOverPanel.SetActive(true);
        int result = enemiesKilled - PlanetState.totalRaidDamage;

        enemiesKilledText.text = enemiesKilled + "";
        enemiesMissedText.text = PlanetState.totalRaidDamage.ToString();
        resultText.text = result.ToString();
        int lostCrystals = (result < GameController.GetCrystals()) ? 0 : result;
        int lostStardust = (result < GameController.GetStardust()) ? 0 : result;
        GameController.AddCrystals(lostCrystals * 10 * GameController.ReturnClickIncrease());
        PlayerPrefs.SetString("crystals", GameController.GetCrystals().ToString());

        GameController.AddStardust(lostStardust);
        PlayerPrefs.SetInt("stardust", GameController.GetStardust());

        PlayerPrefs.SetInt("ToggleRaid", 0);
        enemyAI.deactivate();
    }

}
