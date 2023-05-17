using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    
    public float getVolume() {
        return volumeSlider.value;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeVolume() {
        AudioListener.volume = volumeSlider.value;
    }
}
