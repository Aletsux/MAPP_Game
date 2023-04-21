using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    //implement going back to start menu 
    
    public GameObject menuButton;
    public GameObject exitButton;
    private bool visible;
    private bool muted;
    private void Start() {
        gameObject.SetActive(false);
        visible = false;
        muted = false;
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

    public void onMuteClick() {
        AudioListener.pause = muted;
        muted = !muted;

        if(muted) {
            Debug.Log("Audio muted!");
        } else { 
            Debug.Log("Audio playing!");
        }
    }


}
