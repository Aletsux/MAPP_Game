using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    private SceneChange sceneChange = new SceneChange();
    private StartMenuTransition startMenuTransition= new StartMenuTransition();
    public PlayableDirector timeline;

    void Start()
    {
        PlayerPrefs.DeleteKey("PlayedCutscene"); //TILL FÖR TESTNING AV CUTSCENEN
        if(PlayerPrefs.GetInt("PlayedCutscene") == 0)
        {
            timeline.Play();
            timeline.stopped += PlayedCutscene; //lägger till händelsehanterare (metoden playedCutscene) som hanterar när cutscenen spelat klart
        } else
        {
            sceneChange.LoadMainMenu();
            startMenuTransition.StartTransition();
        }
    }

    public void PlayedCutscene(PlayableDirector pd) //parametern gör att metoden får tillgång till infon om pb-objektet (timeline) som genererade händelsen (stopped)
    {
        PlayerPrefs.SetInt("PlayedCutscene", 1);
        sceneChange.LoadMainMenu();
        startMenuTransition.StartTransition();
    }
}
