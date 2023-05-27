using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCutscene : MonoBehaviour
{
    private SceneChange sceneChange = new SceneChange();

    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void skip()
    {
        sceneChange.LoadMainMenu();
    }
}
