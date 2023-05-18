using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip explosionSFX;

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
    }

    public void PlayDeathSound()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(explosionSFX);
    }

}
