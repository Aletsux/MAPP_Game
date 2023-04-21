using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditText : MonoBehaviour
{

    GameObject canvas;
    GameObject storePanel;
    GameObject powerUpTab;
    GameObject scroll;
    GameObject panel;
    GameObject button;

    public GameObject GC;
    private GameController gameController;
    
    

    // Start is called before the first frame update
    void Start()
    {
        gameController = GC.GetComponent<GameController>();
        FindButtonIdle();
        ChangeTextIdle();

        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeTextIdle();

    }

    public void FindButtonIdle()
    {
        canvas = GameObject.Find("Canvas");
        storePanel = canvas.transform.GetChild(6).gameObject;
        powerUpTab = storePanel.transform.GetChild(3).gameObject;
        scroll = powerUpTab.transform.GetChild(0).gameObject;
        panel = scroll.transform.GetChild(0).gameObject;
        button = panel.transform.GetChild(0).gameObject;
    }

    public void ChangeTextIdle()
    {
        if(gameController.IsIdleTrue() == false)
        {
            button.GetComponent<ItemScript>().desciption = "Get 1 crystal each 60 sec! Every upgrade will decrease time 15 seconds.";
        }
        else
        {
            button.GetComponent<ItemScript>().desciption = "testyTest";
        }

      
        
    }

   
}
