using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void LoadRaid()
    {
        SceneManager.LoadScene("RaidTest");
    }
}

//om vi ville ha en utscrollande meny istället för en ändrad scen, kan man ha menyn under själva spelet och sedan ha
//ett osynligt objekt som åker ut som man sätter till en child. då åker den ovanför spelskärmen och sen?? eller jag vet inte 
