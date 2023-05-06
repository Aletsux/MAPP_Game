using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundAnimationAI : MonoBehaviour
{

    private float timer;
    public float timeBetweenAnimation;

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

    List<GameObject> instantiatedObjects;
    List<Vector2> instancePositions;

    public int objectCount;
    private int maxAttempts = 10;

    void Start()
    {
        prefabs = new List<GameObject> { prefab1, prefab1, prefab1, prefab2, prefab2, prefab3 };
        colors = new List<Color> { color1, color2, color3, color4, color5, color6 };
        float height = Screen.height; // skapar varibel och lagrar canvasens höjd
        float width = Screen.width;

        // Create a list to store the instantiated objects and positions
        instantiatedObjects = new List<GameObject>();
        instancePositions = new List<Vector2>();

        int attempts = 0;
        while (instantiatedObjects.Count < objectCount && attempts < maxAttempts)
        {
            // Generate a random position within the canvas boundaries
            Vector2 position = new Vector2(Random.Range(0f, width), Random.Range(0f, height));

            // Instantiate the object if the position is valid
            if (!instancePositions.Contains(position))
            {
                //GameObject newObject = Instantiate(prefab1, position, transform.rotation, transform);
                GameObject newObject = Instantiate(prefabs[Random.Range(0, prefabs.Count)], position, transform.rotation, transform);
                instantiatedObjects.Add(newObject);
                instancePositions.Add(position);
                newObject.GetComponent<Image>().color = colors[Random.Range(0, colors.Count)];
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
            instantiatedObjects[Random.Range(0, instantiatedObjects.Count)].GetComponent<Animator>().SetTrigger("trigger");
        }
    }
}