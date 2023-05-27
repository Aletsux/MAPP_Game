using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    private SceneChange sceneChange;
    public PlayableDirector timeline;

    void Start()
    {
        sceneChange = FindObjectOfType<SceneChange>();
        //PlayerPrefs.SetInt("PlayedCutscene", 0);
        //PlayerPrefs.SetInt("FromStartMenu", 0);
        Debug.Log("Innan att spelat: " + PlayerPrefs.GetInt("PlayedCutscene"));
        if (PlayerPrefs.GetInt("PlayedCutscene") == 0 || PlayerPrefs.GetInt("FromStartMenu") == 1)
        {
            timeline.Play();
            timeline.stopped += PlayedCutscene; //lägger till händelsehanterare (metoden playedCutscene) som hanterar när cutscenen spelat klart
            PlayerPrefs.SetInt("FromStartMenu", 0);
        } else
        {
            //sceneChange.LoadMainMenu();
            SceneManager.LoadScene("StartMenu");
        }
    }

    public void PlayedCutscene(PlayableDirector pd) //parametern gör att metoden får tillg�ng till infon om pb-objektet (timeline) som genererade händelsen (stopped)
    {
        PlayerPrefs.SetInt("PlayedCutscene", 1);
        Debug.Log("Efter att spelat: " + PlayerPrefs.GetInt("PlayedCutscene"));
        //sceneChange.LoadMainMenu();
        SceneManager.LoadScene("StartMenu");
    }
}
