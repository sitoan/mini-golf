using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private windController wind;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "centerWind")
        {
            rb.AddForce(wind.center * wind.windForce);
        }
        else if (collision.gameObject.tag == "L2RWind")
        {
            rb.AddForce(wind.L2R * wind.windForce);
        }
        else if (collision.gameObject.tag == "R2LWind")
        {
            rb.AddForce(wind.R2L * wind.windForce);
        }

    }
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
}
