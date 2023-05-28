using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public AudioClip tapSound;
    private int pitch = 1;
    
    public void PlaySound()
    {
        audioSource.pitch = 1;
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayTapSound() {
        audioSource.pitch = Random.Range(0.9f,1.05f);
        audioSource.volume = 0.08f;
        audioSource.PlayOneShot(tapSound);
    }
}

