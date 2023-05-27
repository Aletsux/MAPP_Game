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
        //PlayerPrefs.DeleteKey("PlayedCutscene"); //TILL FR TESTNING AV CUTSCENEN
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt("PlayedCutscene") != 1)
        {
            gameObject.SetActive(false);
        } else if (PlayerPrefs.GetInt("PlayedCutscene") == 1)
        {
            gameObject.SetActive(true);
        }
        Debug.Log(PlayerPrefs.GetInt("PlayedCutscene"));
    }

    public void Skip()
    {
        //sceneChange.LoadMainMenu();
        SceneManager.LoadScene("StartMenu");
    }
}
