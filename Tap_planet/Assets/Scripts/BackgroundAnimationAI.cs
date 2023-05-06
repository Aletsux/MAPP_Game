using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundAnimationAI : MonoBehaviour
{
    private float timer;
    public float timeBetweenAnimation;

    private List<GameObject> prefabs;
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;

    private List<Color> colors;
    public Color color1;
    public Color color2;
    public Color color3;
    public Color color4;
    public Color color5;
    public Color color6;


    private List<GameObject> instantiatedStars;
    private List<Vector2> occupiedPositions;

    public int amountOfStars;
    private int maxAttempts = 10;

    void Start()
    {
        prefabs = new List<GameObject> { prefab1, prefab1, prefab1, prefab2, prefab2, prefab3 }; // går att ändra chans för olika typer
        colors = new List<Color> { color1, color2, color3, color4, color5, color6 };
        float height = Screen.height; // skapar varibel och lagrar skärmens storlek
        float width = Screen.width;

        instantiatedStars = new List<GameObject>();
        occupiedPositions = new List<Vector2>();

        int attempts = 0;
        while (instantiatedStars.Count < amountOfStars && attempts < maxAttempts)
        {
            Vector2 position = new Vector2(Random.Range(0f, width), Random.Range(0f, height)); // random plats på skärmen

            if (!occupiedPositions.Contains(position)) // om ingen stjärna redan finns där
            {
                GameObject star = Instantiate(prefabs[Random.Range(0, prefabs.Count)], position, transform.rotation, transform); // skapa random stjärna
                instantiatedStars.Add(star); // sparar stjrärna
                occupiedPositions.Add(position); // sparar positionen
                star.GetComponent<Image>().color = colors[Random.Range(0, colors.Count)]; // random färg
            }
            else
            {
                attempts++;
            }
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAnimation)
        {
            timer = 0f;
            instantiatedStars[Random.Range(0, instantiatedStars.Count)].GetComponent<Animator>().SetTrigger("trigger"); // animera random stjärna ur listan
        }
    }
}