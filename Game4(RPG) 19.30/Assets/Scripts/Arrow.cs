using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 2000f;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4f);

    }

    private void FixedUpdate()
    {
        rb2d.velocity = transform.right * speed * Time.deltaTime;
    }
}
