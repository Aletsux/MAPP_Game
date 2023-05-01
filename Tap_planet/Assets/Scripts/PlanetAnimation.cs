using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAnimation : MonoBehaviour
{
    private RectTransform planetPosition;
    public int toMove;
    [Space]
    private float timer = 0;
    [Space]
    public float timePerFrame = 0.8f;
    private float normalTimePerFrame;
    public float speedyTimePerFrame = 0.1f;
    [Space]
    private float duringClickTimer = 0;
    public float timeBeforeSlowDown;

    void Start()
    {
        planetPosition = gameObject.GetComponent<RectTransform>();
        normalTimePerFrame = timePerFrame;
    }

    void Update()
    {
        if (true) // kanske vill ha condition senare..
        {
            timer += Time.deltaTime;
            if (timer >= timePerFrame)
            {
                timer = 0f;
                planetPosition.anchoredPosition = new Vector2(planetPosition.anchoredPosition.x, planetPosition.anchoredPosition.y + toMove);
                SetDirection();
            }
        }
        if (timePerFrame == speedyTimePerFrame)
        {
            duringClickTimer += Time.deltaTime;
            if (duringClickTimer >= timeBeforeSlowDown)
            {
                duringClickTimer = 0f;
                SlowDown();
            }
        }
    }

    private void SetDirection()
    {
        toMove = -toMove;
    }

    public void SpeedUp()
    {
        timePerFrame = speedyTimePerFrame;
        duringClickTimer = 0;
    }

    private void SlowDown()
    {
        timePerFrame = normalTimePerFrame;
    }
}
