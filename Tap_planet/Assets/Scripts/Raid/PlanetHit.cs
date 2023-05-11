using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetHit : MonoBehaviour
{
    public GameObject hitSprite;
    private List<GameObject> hitSprites = new();

    private bool startBlinking;
    private bool isVisible;

    private float alpha;

    private int blinkAmount = 8;
    private int blinkCount;

    private float timer;
    public float timeBetweenBlinks;
    
    protected virtual void Start()
    {
        GameObject[] activeSprites = GameObject.FindGameObjectsWithTag("PlanetSprite");

        foreach (GameObject g in activeSprites)
        {
            GameObject newHitSprite = Instantiate(hitSprite, g.transform.position, g.transform.rotation, g.transform);
            hitSprites.Add(newHitSprite);
            newHitSprite.GetComponent<Image>().color = new Color(1, 0, 0, 0);
        }
    }

    void Update()
    {
        if (startBlinking)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenBlinks)
            {
                timer = 0;
                ActivateHitSprites();
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
        foreach (GameObject g in hitSprites)
        {
            if (isVisible)
            {
                alpha = 0.5f;
            }
            else
            {
                alpha = 0f;
            }
            g.GetComponent<Image>().color = new Color(1, 0, 0, 0);
        }
    }

    private void ActivateHitSprites()
    {
        foreach (GameObject g in hitSprites)
        {
            if (isVisible)
            {
                alpha = 0.5f;
            }
            else
            {
                alpha = 0f;
            }
            g.GetComponent<Image>().color = new Color(1, 0, 0, alpha);
        }
    }
}
