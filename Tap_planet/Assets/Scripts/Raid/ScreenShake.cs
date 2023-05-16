using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private float shakeTime;
    private float shakeMagnitude;

    private Transform shakeTransform; // Reference to the Shake object's transform

    private void Start()
    {
        shakeTransform = transform;
    }

    private void Update()
    {
        Vector3 newPosition = (Vector3)(Random.insideUnitCircle * shakeTime * shakeMagnitude);
        newPosition.z = shakeTransform.localPosition.z; // Maintain the original z position of Shake

        shakeTransform.localPosition = newPosition;

        if (shakeTime > 0)
            shakeTime -= Time.deltaTime;
        else
            shakeTime = 0f;
    }

    public void Shake() {
        shakeTime = 0.7f;
        shakeMagnitude = 5f;
    }



    //public bool start = false;
    //public AnimationCurve curve;
    //public float duration = 1f;

    //void Update()
    //{
    //    if (start) {
    //        start = false;
    //        StartCoroutine(Shake());
    //    }

    //}

    //IEnumerator Shake() {
    //    Vector3 startPosition = transform.position;
    //    float elapsedTime = 0f;

    //    while(elapsedTime < duration) {
    //        elapsedTime += Time.deltaTime;
    //        float strength = curve.Evaluate(elapsedTime / duration);
    //        transform.position = startPosition + Random.insideUnitSphere * strength;
    //        yield return null;
    //    }

    //    transform.position = startPosition;
    //}
}
