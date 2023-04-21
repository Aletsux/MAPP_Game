using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class RaidState : MonoBehaviour
{

    public int enemiesKilled = 0;
    public int enemiesMissed = 0;

    public Text enemiesKilledText;
    public Text enemiesMissedText;

    public Text resultText;

    public GameObject raidOverPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        raidOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        int sum = enemiesKilled + enemiesMissed;;

        if (sum == 20)
        {
            raidEnded();
        }
    }

    public void raidEnded()
    {
        raidOverPanel.SetActive(true);
        int result = enemiesKilled - enemiesMissed; 

        enemiesKilledText.text = enemiesKilled.ToString();
        enemiesMissedText.text = enemiesMissed.ToString();
        resultText.text = result.ToString();
    }
}
