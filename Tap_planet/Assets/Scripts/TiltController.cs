using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltController : MonoBehaviour
{
    Rigidbody2D rb;
    float dx;
    public float moveSpeed = 50f;

    public float minX = -20f;
    public float maxX = 20f;
    private Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
        Input.gyro.enabled = true;
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        dx = Input.acceleration.x * moveSpeed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dx, 0f);
    }
}
