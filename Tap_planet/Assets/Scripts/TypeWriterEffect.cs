using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour
{
    public float delay = 0.08f; //tid innan nya bokstaven/tecknet visas
    public string entireText; //hela texten som ska visas
    private string currentText = ""; //böjar alltid tom

    void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < entireText.Length + 1; i++) //kommer köra lika många gånger som antalet bokstäver i texten
        {
            currentText = entireText.Substring(0, i); //kommer visa bokstäverna på plats 0-1 i entireText
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
