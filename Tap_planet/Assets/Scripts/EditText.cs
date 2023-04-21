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

    // Start is called before the first frame update
    void Start()
    {
        FindButtonIdle();
        changeTextIdle();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void changeTextIdle()
    {
        button.GetComponent<ItemScript>().desciption = "Test";

        
    }
}
