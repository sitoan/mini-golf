using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class predictObject : MonoBehaviour
{
    public bool InHole;
    private Vector2 starpos;
    [SerializeField] private BotController botCon;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "hole")
        {
            InHole = true;            
        }
    }

    public void resetObj()
    {
        transform.localPosition = starpos;
        InHole = false;

    }

    private void Start()
    {
        starpos = new Vector3(-1.0f, 0.0f, 0.0f);
    }
}
