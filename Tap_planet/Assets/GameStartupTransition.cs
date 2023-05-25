using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStartupTransition : MonoBehaviour
{
    private float alpha = 1;
    private Image image;
    private bool startTransition;

    void Awake()
    {
        image = GetComponent<Image>();
        startTransition = true;
    }

    void Update()
    {
        if (startTransition)
        {
            var newColor = new Color(0, 0, 0, alpha);
            alpha -= 0.01f;
            image.color = newColor;

            if (image.color == new Color(0, 0, 0, 0))
            {
                startTransition = false;
                gameObject.SetActive(false);
            }
        }
    }
}

