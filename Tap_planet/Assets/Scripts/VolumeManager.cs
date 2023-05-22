using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    public ToggleMute tm;
    
    public float getVolume() {
        return volumeSlider.value;
    }
    private void Start() {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1);
    }

    public void ChangeVolume() {
        AudioListener.volume = volumeSlider.value;
        //Set muted to true when slider is 0
        if(AudioListener.volume <= 0) {
            MenuScript.onMuteClick();
        } else {
            MenuScript.setIsMuted(true);
            MenuScript.onMuteClick(); //Toggles muted
        }
        
        tm.ChangeSprite(); 
    }
}
