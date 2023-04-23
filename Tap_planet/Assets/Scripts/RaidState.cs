using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class RaidState : MonoBehaviour
{
    public static int enemiesKilled;

    public Text enemiesKilledText;
    public Text enemiesMissedText;

    public Text resultText;

    public GameObject raidOverPanel;

    private bool beginRaid;
    private float raidTimer;
    private float raidTime = 10;

    // Start is called before the first frame update
    void Start()
    {
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
                raidTimer = 0f;
                RaidEnded();
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
        GameController.AddCrystals(result * 10 * GameController.ReturnClickIncrease());
        GameController.AddStardust(result);
        //GameController.AddCrystals(result * (100 ^ GameController.ReturnClickIncrease()));
        //GameController.AddStardust(result * GameController.GetStardustMinerLevel()+1);
    }
}
