using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public AudioClip tapSound;
    private float vol;
    
    private void Awake() {
        vol = audioSource.volume;
    }

    public void PlaySound()
    {
        audioSource.volume = vol;
        audioSource.pitch = 1;
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayTapSound() {
        audioSource.pitch = Random.Range(0.9f,1.05f);
        audioSource.volume = vol - 0.1f;
        audioSource.PlayOneShot(tapSound);
    }
}

