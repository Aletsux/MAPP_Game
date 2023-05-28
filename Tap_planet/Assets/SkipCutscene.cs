using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipCutscene : MonoBehaviour
{
    private SceneChange sceneChange;

    void Awake()
    {
        sceneChange = FindObjectOfType<SceneChange>();
        if (PlayerPrefs.GetInt("PlayedCutscene") != 1)
        {
            gameObject.SetActive(false);
        } else if (PlayerPrefs.GetInt("PlayedCutscene") == 1)
        {
            gameObject.SetActive(true);
        }
        Debug.Log(PlayerPrefs.GetInt("PlayedCutscene"));
    }
}
