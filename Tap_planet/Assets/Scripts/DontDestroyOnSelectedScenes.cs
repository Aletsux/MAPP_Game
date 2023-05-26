using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnSelectedScenes : MonoBehaviour
{
    public List<string> sceneNames;

    public string instanceName;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    { 
        CheckForDuplicateInstances();
        CheckIfSceneInList();
    }

    void CheckForDuplicateInstances()
    {
        DontDestroyOnSelectedScenes[] collection = FindObjectsOfType<DontDestroyOnSelectedScenes>();

        foreach (DontDestroyOnSelectedScenes obj in collection)
        {
            if (obj != this)
            {
                if (obj.instanceName == instanceName)
                {
                    DestroyImmediate(obj.gameObject);
                }
            }
        }
    }

    private void CheckIfSceneInList()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (sceneNames.Contains(currentScene))
        {
            
        }
        else
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            DestroyImmediate(this.gameObject);
        }
    }
}

