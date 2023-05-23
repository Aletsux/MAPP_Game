using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour
{
    public float delay = 0.04f; //tid innan nya bokstaven/tecknet visas
    public string entireText; //hela texten som ska visas
    private string currentText = ""; //b�jar alltid tom

    public AudioClip[] typeWriterClips; //f�rkortat TPC
    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < entireText.Length + 1; i++) //kommer k�ra lika m�nga g�nger som antalet bokst�ver i texten
        {
            currentText = entireText.Substring(0, i); //kommer visa bokst�verna p� plats 0-i i entireText
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
