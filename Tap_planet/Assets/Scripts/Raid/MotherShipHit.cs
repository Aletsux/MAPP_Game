using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotherShipHit : MonoBehaviour
{
    private Image hitSprite;

    private bool startBlinking;

    private float alpha;

    public int blinkAmount = 8;
    private int blinkCount;

    private float timer;
    public float timeBetweenBlinks; // hur l?l?ng tid mellan blinkningar

    void Start()
    {
        hitSprite = transform.GetChild(0).GetComponent<Image>(); // sparar den vita bilden
    }

    void Update()
    {
        if (startBlinking)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenBlinks)
            {
                timer = 0;
                if (alpha == 0) // byter fr?n synlig till osynlig baserat p?  om den redan syns eller inte
                {
                    alpha = 0.5f;
                }
                else
                {
                    alpha = 0f;
                }
                hitSprite.color = new Color(1, 1, 1, alpha); // s?tter f?rgen med alpha
                blinkCount++;
                if (blinkCount >= blinkAmount) // om den blinkat tillr?ckligt
                {
                    startBlinking = false;
                    blinkCount = 0;
                    ResetAlpha();
                }
            }
        }
    }

    public void StartBlinking() //kallas p? n?r spelaren trycker p? mothership
    {
        startBlinking = true;
    }

    private void ResetAlpha() // g?r den vita bilden osynlig
    {
        hitSprite.color = new Color(1, 1, 1, 0);
    }
}
