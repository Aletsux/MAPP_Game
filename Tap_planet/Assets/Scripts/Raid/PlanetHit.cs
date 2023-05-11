using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetHit : MonoBehaviour
{
    public GameObject hitSprite;
    private List<GameObject> hitSprites = new();

    private bool startBlinking;

    private float alpha;

    public int blinkAmount = 8; // hur m?nga blinkningar
    private int blinkCount;

    private float timer;
    public float timeBetweenBlinks; // hur l?ng tid mellan blinkningar

    protected virtual void Start()
    {
        GameObject[] activeSprites = GameObject.FindGameObjectsWithTag("PlanetSprite"); // hittar alla aktiva sprites (accessoarer + planeter)

        foreach (GameObject g in activeSprites)
        {
            GameObject newHitSprite = Instantiate(hitSprite, g.transform.position, g.transform.rotation, g.transform); // l?gger till en vit sprite som child till varje aktiv sprite (accessoarer + planeter)
            hitSprites.Add(newHitSprite); // l?gger till dem i en lista
            newHitSprite.GetComponent<Image>().color = new Color(1, 0, 0, 0); // g?r dem vita 
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
                blinkCount++;
                if (blinkCount >= blinkAmount)  // om den blinkat tillr?ckligt
                {
                    startBlinking = false;
                    blinkCount = 0;
                    ResetAlpha();
                }
            }
        }
    }

    public void StartBlinking() //kallas p? n?r spelaren blir tr?ffad
    {
        startBlinking = true;
    }

    private void ResetAlpha()  // g?r alla vita sprites  osynliga
    {
        foreach (GameObject g in hitSprites)
        {
            g.GetComponent<Image>().color = new Color(1, 0, 0, 0);
        }
    }

    private void ActivateHitSprites() // beroende p? om de syns eller inte ?ndrar alpha
    {
        foreach (GameObject g in hitSprites)
        {
            if (alpha == 0)
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
