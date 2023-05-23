using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour
{
    public float delay = 0.04f; //tid innan nya bokstaven/tecknet visas
    public string entireText; //hela texten som ska visas
    private string currentText = ""; //böjar alltid tom

    public AudioClip[] typeWriterClips; //förkortat TPC
    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < entireText.Length + 1; i++) //kommer köra lika många gånger som antalet bokstäver i texten
        {
            currentText = entireText.Substring(0, i); //kommer visa bokstäverna på plats 0-i i entireText
            this.GetComponent<Text>().text = currentText;
            playRandomTPC();
            yield return new WaitForSeconds(delay);
        }
    }

    public void playRandomTPC()
    {
        audioSource.clip = typeWriterClips[Random.Range(0, typeWriterClips.Length)];
        audioSource.Play();
    }
}
