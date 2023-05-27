using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCutscene : MonoBehaviour
{
    private SceneChange sceneChange = new SceneChange();

    void Start()
    {
        if (PlayerPrefs.GetInt("PlayedCutscene") == 1)
        {
            gameObject.SetActive(true);
        }
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
