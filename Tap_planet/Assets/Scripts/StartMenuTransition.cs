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

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (startTransition)
        {
            var newColor = new Color(0, 0, 0, alpha);
            alpha += 0.01f;
            image.color = newColor;

            if (image.color == new Color(0, 0, 0, 1))
            {
                    startTransition = false;
                    SceneChange.LoadGame();
            }
        }
    }

    public void StartTransition()
    {
        startTransition = true;
    }
}
