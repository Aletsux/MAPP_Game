using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    private SceneChange sceneChange = new SceneChange();
    private StartMenuTransition startMenuTransition  = new StartMenuTransition();
    public PlayableDirector timeline;
    private 

    void Awake()
    {
        
    }
    void Start()
    {
        
        //PlayerPrefs.SetInt("PlayedCutscene", 0);
        //PlayerPrefs.SetInt("FromStartMenu", 0);
        Debug.Log("Innan att spelat: " + PlayerPrefs.GetInt("PlayedCutscene"));
        if (PlayerPrefs.GetInt("PlayedCutscene") == 0 || PlayerPrefs.GetInt("FromStartMenu") == 1)
        {
            timeline.Play();
            timeline.stopped += PlayedCutscene; //l�gger till h�ndelsehanterare (metoden playedCutscene) som hanterar n�r cutscenen spelat klart
            PlayerPrefs.SetInt("FromStartMenu", 0);
        } else
        {
            sceneChange.LoadMainMenu();
            //startMenuTransition.StartTransition();
        }
    }

    public void PlayedCutscene(PlayableDirector pd) //parametern g�r att metoden f�r tillg�ng till infon om pb-objektet (timeline) som genererade h�ndelsen (stopped)
    {
        PlayerPrefs.SetInt("PlayedCutscene", 1);
        Debug.Log("Efter att spelat: " + PlayerPrefs.GetInt("PlayedCutscene"));
        sceneChange.LoadMainMenu();
        //startMenuTransition.StartTransition();
    }
}
