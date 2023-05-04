using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimationAI : MonoBehaviour
{

    public float timer;
    public float playOneShot;

    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    private List<GameObject> prefabs;

List<GameObject> instantiatedObjects;
    List<Vector2> instancePositions;

    public int objectCount = 35;
    public int maxAttempts = 100;

    void Start()
    {
        prefabs = new List<GameObject> { prefab1, prefab1, prefab1, prefab1, prefab1, prefab1, prefab2, prefab3};
        float height = FindObjectOfType<Canvas>().GetComponent<RectTransform>().rect.height; // skapar varibel och lagrar canvasens höjd
        float width = FindObjectOfType<Canvas>().GetComponent<RectTransform>().rect.width;

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

                GameObject newObject = Instantiate(prefabs[Random.Range(0, prefabs.Count)], position, transform.rotation, transform);
                instantiatedObjects.Add(newObject);
                instancePositions.Add(position);
            }
            attempts++;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= playOneShot)
        {
            timer = 0f;
        }
    }
}