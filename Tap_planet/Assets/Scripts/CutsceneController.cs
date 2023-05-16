using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    private SceneChange sceneChange = new SceneChange();
    public PlayableDirector timeline;

    void Start()
    {
        PlayerPrefs.DeleteKey("PlayedCutscene"); //TILL F�R TESTNING AV CUTSCENEN
        if(PlayerPrefs.GetInt("PlayedCutscene") == 0)
        {
            timeline.Play();
            timeline.stopped += PlayedCutscene; //l�gger till h�ndelsehanterare (metoden playedCutscene) som hanterar n�r cutscenen spelat klart
        } else
        {
            sceneChange.LoadMainMenu();
        }
    }

    public void PlayedCutscene(PlayableDirector pd) //parametern g�r att metoden f�r tillg�ng till infon om pb-objektet (timeline) som genererade h�ndelsen (stopped)
    {
        PlayerPrefs.SetInt("PlayedCutscene", 1);
        sceneChange.LoadMainMenu();
    }
}
