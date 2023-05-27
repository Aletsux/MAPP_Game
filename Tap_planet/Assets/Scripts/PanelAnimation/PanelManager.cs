using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    private static LinkedList<GameObject> panelQueue = new();
    private static bool currentIsOpen = false;
    private static PanelManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        panelQueue.Clear();
        currentIsOpen = false;
    }
    void Update()
    {
        if (panelQueue.Count != 0)
        {
            if (!currentIsOpen )
            {
                OpenNextPanel();
            }
        }
    }

    public static void AddPanelToQueue(GameObject panel)
    {
        if (!panelQueue.Contains(panel))
        {
            panelQueue.AddLast(panel);
        }    
    }

    public static void AddPanelToQueue(GameObject panel, bool addFirst)
    {
        if (!panelQueue.Contains(panel))
        {
            if (addFirst)
            {
                panelQueue.AddFirst(panel);
            }
            else
            {
                panelQueue.AddLast(panel);
            }
        }
    }

    private static void RemovePanelFromQueue()
    {
        panelQueue.RemoveFirst();
    }

    private void OpenNextPanel()
    {
        if (!panelQueue.First.Value.activeSelf)
        {
            panelQueue.First.Value.GetComponent<ActivatePanel>().Toggle(true);
        }
        panelQueue.First.Value.GetComponent<PanelAnimation>().StretchPanel();
        currentIsOpen = true;
    }

    public static void IAmDone()
    {
        currentIsOpen = false;
        RemovePanelFromQueue();
    }
}
