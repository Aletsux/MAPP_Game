using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundAnimationAI : MonoBehaviour
{
    private float timer;
    public float timeBetweenAnimation;

    private float timerDuringClick;
    private float saveTimeBetween = 2;
    public float timeBetweenAnimationFast;

    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    public Color color1;
    public Color color2;
    public Color color3;
    public Color color4;
    public Color color5;
    public Color color6;

    private List<GameObject> prefabs;
    private List<Color> colors;

    List<GameObject> instantiatedStars;
    List<Vector2> occupiedPositions;

    public int objectCount;
    private int maxAttempts = 10;

    void Start()
    {
        prefabs = new List<GameObject> { prefab1, prefab1, prefab1, prefab2, prefab2, prefab3 };
        colors = new List<Color> { color1, color2, color3, color4, color5, color6 };
        float height = Screen.height; // skapar varibel och lagrar canvasens höjd
        float width = Screen.width;

        // Create a list to store the instantiated objects and positions
        instantiatedStars = new List<GameObject>();
        occupiedPositions = new List<Vector2>();

        int attempts = 0;
        while (instantiatedStars.Count < objectCount && attempts < maxAttempts)
        {
            // Generate a random position within the canvas boundaries
            Vector2 position = new Vector2(Random.Range(0f, width), Random.Range(0f, height));

            // Instantiate the object if the position is valid
            if (!occupiedPositions.Contains(position))
            {
                //GameObject newObject = Instantiate(prefab1, position, transform.rotation, transform);
                GameObject star = Instantiate(prefabs[Random.Range(0, prefabs.Count)], position, transform.rotation, transform);
                instantiatedStars.Add(star);
                occupiedPositions.Add(position);
                star.GetComponent<Image>().color = colors[Random.Range(0, colors.Count)];
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
            instantiatedStars[Random.Range(0, instantiatedStars.Count)].GetComponent<Animator>().SetTrigger("trigger");
        }

        if (timeBetweenAnimation == timeBetweenAnimationFast)
        {
            timerDuringClick += Time.deltaTime;
            if (timerDuringClick >= 1)
            {
                timerDuringClick = 0f;
                timeBetweenAnimation = saveTimeBetween;
            }
        }
    }

    public void SpeedUp()
    {
        timeBetweenAnimation = timeBetweenAnimationFast;
    }
}