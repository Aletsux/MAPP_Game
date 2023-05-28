using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private float shakeTime;
    private float shakeMagnitude;

    private Transform shakeTransform;

    private void Start()
    {
        shakeTransform = transform;
    }

    private void Update()
    {
        Vector3 newPosition = (Vector3)(Random.insideUnitCircle * shakeTime * shakeMagnitude);
        newPosition.z = shakeTransform.localPosition.z; 
        shakeTransform.localPosition = newPosition;

        if (shakeTime > 0)
            shakeTime -= Time.deltaTime;
        else
            shakeTime = 0f;
    }

    public void Shake()
    {
        shakeTime = 0.7f;
        shakeMagnitude = 5f;
    }
}
