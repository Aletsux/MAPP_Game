using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotherShipHit : MonoBehaviour
{
    private Image hitSprite;

    private bool startBlinking;
    private bool isVisible;

    private float alpha;

    private int blinkAmount = 8;
    private int blinkCount;

    private float timer;
    public float timeBetweenBlinks;

    void Start()
    {
        hitSprite = transform.GetChild(0).GetComponent<Image>();
        print(hitSprite.name);
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
                hitSprite.color = new Color(1, 1, 1, alpha);
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

    public void StartBlinking()
    {
        startBlinking = true;
    }

    private void ResetAlpha()
    {
        hitSprite.color = new Color(1, 1, 1, 0);
    }
}
