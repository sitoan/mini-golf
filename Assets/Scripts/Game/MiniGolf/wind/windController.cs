using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windController : MonoBehaviour
{
    [SerializeField] private GameObject centerWind1;
    [SerializeField] private GameObject centerWind2;
    [SerializeField] private GameObject L2RWind1;
    [SerializeField] private GameObject L2RWind2;
    [SerializeField] private GameObject R2LWind1;
    [SerializeField] private GameObject R2LWind2;

    public int randomWind;

    public Vector2 center;
    public Vector2 L2R;
    public Vector2 R2L;
    public float windForce;

    private void OnEnable()
    {
        randomWind = Random.Range(1, 4);
        if (randomWind == 1)
        {
            centerWind1.SetActive(true);
        }
        else if (randomWind == 2)
        {
            L2RWind1.SetActive(true);
        }
        else if (randomWind == 3)
        {
            R2LWind1.SetActive(true);
        }

        center = Vector2.down.normalized;
        L2R = new Vector2(21.0f, -64.7f).normalized;
        R2L = new Vector2(-21.0f, -64.7f).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "centerWind")
        {
            centerWind2.SetActive(true);
        }
        else if(collision.gameObject.tag == "L2RWind")
        {
            L2RWind2.SetActive(true);
        }
        else if(collision.gameObject.tag == "R2LWind")
        {
            R2LWind2.SetActive(true);
        }
    }

    private void OnDisable()
    {
        centerWind1.SetActive(false);
        L2RWind1.SetActive(false);
        R2LWind1.SetActive(false);
        centerWind2.SetActive(false);
        L2RWind2.SetActive(false);
        R2LWind2.SetActive(false);
    }
}
