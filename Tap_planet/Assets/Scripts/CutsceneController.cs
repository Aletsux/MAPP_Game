using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector timeline;
    public SceneTransitionFader transition;
    void Start()
    {
        if (PlayerPrefs.GetInt("PlayedCutscene") == 0 || PlayerPrefs.GetInt("FromStartMenu") == 1)
        {
            timeline.Play();
            timeline.stopped += PlayedCutscene; //lägger till händelsehanterare (metoden playedCutscene) som hanterar när cutscenen spelat klart
            PlayerPrefs.SetInt("FromStartMenu", 0);
        } else
        {
            SceneManager.LoadScene("StartMenu");
        }
    }

    public void PlayedCutscene(PlayableDirector pd) //parametern gör att metoden får tillg�ng till infon om pb-objektet (timeline) som genererade händelsen (stopped)
    {
        PlayerPrefs.SetInt("PlayedCutscene", 1);
        transition.FadeToScene("StartMenu");
    }
}