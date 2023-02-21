using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    Rigidbody2D ballPhysic;
    Vector2 knockBack;
    Vector2 ballVelocity;
    private float speed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed = ballVelocity.magnitude;
        knockBack = Vector2.Reflect(ballVelocity.normalized, collision.contacts[0].normal);
        ballPhysic.velocity = knockBack * speed;
    }


    void Start()
    {
        ballPhysic = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ballVelocity = ballPhysic.velocity;
    }
}
