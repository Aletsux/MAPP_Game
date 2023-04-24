using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionState : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetObjectActive() {
        gameObject.SetActive(true);
    }
}
