using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControll : MonoBehaviour
{
    public AudioSource source;
    public AudioClip sound;

    // Start is called before the first frame update
    void Start()
    {
        source.clip = sound;
        source.loop = true;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //source.clip = sound;
        //source.loop = true;
    }
}
