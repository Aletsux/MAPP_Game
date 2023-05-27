using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCutscene : MonoBehaviour
{
    private SceneTransitionFader transitionEffect;

    void Start()
    {
        transitionEffect = GameObject.FindAnyObjectByType<SceneTransitionFader>();
        StartCoroutine("Fade");
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(5);
        PlayerPrefs.SetInt("FromStartMenu", 1);
        transitionEffect.FadeToScene("Cutscene");
    }
}
