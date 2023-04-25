using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class MissedRaid : MonoBehaviour
{
    private static Random rnd = new Random();
    private float lostCrystals;
    private float lostStardust;
    public Text crystalText;
    public Text stardustText;

    public void CalculateRaidLoss(int raidAmount)
    {
     if (raidAmount == 1)
        {
            int rng = rnd.Next(1, 6);
            lostCrystals = rng * GameController.GetCrystals() / 10;
            GameController.DecreaseCrystals((int)lostCrystals);
            crystalText.text = "Lost Crystals: " + lostCrystals;

            lostStardust = rng * GameController.GetStardust() / 10;
            GameController.DecreaseStardust((int)lostStardust);
            stardustText.text = "Lost Stardust: " + lostStardust;
            PlayerPrefs.SetInt("ToggleRaid", 0);          
        }

        else if (raidAmount == 2)
        {
            int rng = rnd.Next(1, 6);
            lostCrystals = rng * GameController.GetCrystals() / 10;
            GameController.DecreaseCrystals((int)lostCrystals);
            crystalText.text = "Lost Crystals: " + lostCrystals;

            lostStardust = rng * GameController.GetStardust() / 10;
            GameController.DecreaseStardust((int)lostStardust);
            stardustText.text = "Lost Stardust: " + lostStardust;
            PlayerPrefs.SetInt("ToggleRaid", 0);
        }

        else if (raidAmount >= 3)
        {
            int rng = rnd.Next(1, 6);
            lostCrystals = rng * GameController.GetCrystals() / 10;
            GameController.DecreaseCrystals((int)lostCrystals);
            crystalText.text = "Lost Crystals: " + lostCrystals;

            lostStardust = rng * GameController.GetStardust() / 10;
            GameController.DecreaseStardust((int)lostStardust);
            stardustText.text = "Lost Stardust: " + lostStardust;
            PlayerPrefs.SetInt("ToggleRaid", 0);
        }
    }
}
