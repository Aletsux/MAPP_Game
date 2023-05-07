using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    public void PlaySound()
    {
        audioSource.PlayOneShot(audioClip);
    }
}

