using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlanetSprites : MonoBehaviour
{

    public List<GameObject> planetObjects = new List<GameObject>();
    public List<GameObject> accessoryObjects = new List<GameObject>();

    void Awake()
    {
        for (int i = 0; i < accessoryObjects.Count; i++)
        {
            if (PlayerPrefs.GetInt("AccessoryEquipped_" + i) == 1)
            {
                accessoryObjects[i].SetActive(true);
                
            }
            else if (PlayerPrefs.GetInt("AccessoryEquipped_" + i) == 0 && PlayerPrefs.GetInt("AccessoryPurchased_" + i) == 1)
            {
                accessoryObjects[i].SetActive(false);
                
                PlayerPrefs.SetInt("AccessoryEquipped_" + i, 0);
                PlayerPrefs.Save();
            }
            else if (PlayerPrefs.GetInt("AccessoryEquipped_" + i) == 0 && PlayerPrefs.GetInt("AccessoryPurchased_" + i) == 0)
            {
                accessoryObjects[i].SetActive(false);
                
                PlayerPrefs.SetInt("AccessoryEquipped_" + i, 0);
                PlayerPrefs.Save();
            }

        }

        //Planeter
        int activePlanetIndex = PlayerPrefs.GetInt("ActivePlanetIndex", 0);

        for (int i = 0; i < planetObjects.Count; i++)
        {
            if (i == activePlanetIndex) //Om i är den aktiva planeten
            {
                planetObjects[i].SetActive(true);
            }
            else if (PlayerPrefs.GetInt("PlanetPurchased_" + i) == 1) //Om planeten har köpts tidigare
            {
                planetObjects[i].SetActive(false);
            }
            else //Om planeten ej har köpts tidigare
            {
                planetObjects[i].SetActive(false);
            }

        }

        //Om ingen planet är aktiverad, sätt startplaneten som aktiv
        if (activePlanetIndex == 0)
        {
            planetObjects[0].SetActive(true);
            PlayerPrefs.SetInt("PlanetPurchased_" + 0, 1);
            PlayerPrefs.Save();
        }
    }
}
