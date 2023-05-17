using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    //implement going back to start menu 
    
    public GameObject menuButton;
    public GameObject exitButton;
    public GameObject panel;
    private bool visible;
    private static bool muted;

    private Button openSettings;

    public static bool getIsMuted() {
        return muted;
    }  
    public static void setIsMuted(bool mute) {
        muted = mute;
    }

    void Start()
    {
        openSettings = gameObject.GetComponent<Button>();
        //gameObject.SetActive(false);
        visible = false;
        if(PlayerPrefs.GetFloat("Volume", 1) <= 0) {
            muted = true;
            return;
        }
        muted = false;
    }

    void Update()
    {
       //closeOnClick();
    }

    private void closeOnClick() {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Get the click position in screen coordinates
            Vector3 clickPosition = Input.mousePosition;

            // Convert the screen coordinates to world coordinates
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(clickPosition);

            // Check if the click position is outside the panel's bounding box
            if (!RectTransformUtility.RectangleContainsScreenPoint(panel.GetComponent<RectTransform>(), clickPosition, Camera.main))
            {
                // Clicked outside the panel, close it
                toggleMenu();
            }
        }
    }
    
    //Open and close menu
    public void toggleMenu() {
        gameObject.SetActive(!visible);
        visible = !visible;

    }

    //Go back to start menu
    public void onExitClick() {
        SceneManager.LoadScene(0);
    }

    public static void onMuteClick() {
        AudioListener.pause = !muted;
        muted = !muted;
        
        if(muted) {
            Debug.Log("Audio muted!");
        } else { 
            Debug.Log("Audio playing!");
        }
    }

    public void SetButtonActive()
    {
        if (openSettings.enabled == false)
        {
            openSettings.enabled = true;
        }
        else
        {
            openSettings.enabled = false;
        }
    }
}
