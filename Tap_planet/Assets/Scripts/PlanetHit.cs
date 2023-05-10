using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetHit : MonoBehaviour
{
    private bool startBlinking;
    public float alpha;
    protected Image image;
    protected int blinkAmount = 6;
    public float timeBetweenBlinks;
    private float timer;
    private bool isVisible;
    private int blinkCount;

    protected virtual void Start()
    {
        image = GameObject.FindWithTag("ActivePlanet").GetComponent<Image>();
        
    }

    void Update()
    {
        if (startBlinking)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenBlinks)
            {
                timer = 0;
                if (isVisible)
                {
                    alpha = 0.5f;
                }
                else
                {
                    alpha = 0f;
                }
                image.color = new Color(1, 1, 1, alpha);
                isVisible = !isVisible;
                blinkCount++;
                if (blinkCount >= blinkAmount)
                {
                    startBlinking = false;
                    blinkCount = 0;
                    ResetAlpha();
                }
            }
        }
    }

    public void StartBlinking(){
        startBlinking = true;
    }
    private void ResetAlpha()
    {
        image.color = new Color(1, 1, 1, 1);
    }
}
