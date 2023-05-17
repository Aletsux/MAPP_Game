using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuTransition : MonoBehaviour
{
    private float alpha = 0;
    private Image image;
    private bool startTransition;
    private SceneChange sceneChange = new SceneChange();

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        string sceneName = activeScene.name;

        if (startTransition)
        {
            var newColor = new Color(0, 0, 0, alpha);
            alpha += 0.01f;
            image.color = newColor;

            if (image.color == new Color(0, 0, 0, 1))
            {
                if(sceneName == "Cutscene")
                {
                    startTransition = false;
                    sceneChange.LoadMainMenu();
                } else
                {
                    startTransition = false;
                    SceneChange.LoadGame();
                }
            }
        }
    }

    public void StartTransition()
    {
        startTransition = true;
    }
}
