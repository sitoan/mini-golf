using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MB.PhysicsPrediction;
using static MB.PhysicsPrediction.PredictionSystem;
using static MB.PhysicsPrediction.PredictionSystem.Record;
using System;
using UnityEngine.WSA;

public class BallController : MonoBehaviour
{
    private LineRenderer powerLine;
    private Rigidbody2D rb;
    private Transform ballTransform;
    private Vector2 powerpos;
    private float speed;
    public bool isMoving;
    private bool redTurn;
    [SerializeField] private windController wind;
    [SerializeField] private float sittingTime;
    [SerializeField] private float power;
    [SerializeField] private Transform hole;
    [SerializeField] private Animator ballAnim;
    [SerializeField] private Animator holeAnim;
    [SerializeField] private BotController botCon;
    public Action onTurnEnd;

    public void StartRedTurn()
    {
        redTurn = true;
    }

    public void StartBlueTurn()
    {
        redTurn = false;
        botCon.StartTurn();
    }

    private void OnMouseDrag()
    {
        if (!isMoving && redTurn)
        {
            powerLine.enabled = true;
            powerpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            powerLine.SetPosition(1, powerpos);
            powerLine.SetPosition(0, ballTransform.position);
        }
    }

    private void OnMouseUp()
    {
        if (!isMoving && redTurn)
        {
            powerLine.enabled = false;
            Vector2 ballPos = ballTransform.position;
            Vector3 shootDirection = (ballPos - powerpos);
            rb.AddForce(shootDirection * power);
            isMoving = true;
            sittingTime = 0.0f;
        }
    }

    private void isStoping()
    {
        if (rb.velocity.magnitude < 0.1f)
        {
            sittingTime += Time.deltaTime;
            if (sittingTime > 0.50f)
            {
                StartCoroutine(End());
            }
        }
    }

    private IEnumerator End()
    {
        isMoving = false;
        holeAnim.SetBool("goal", true);
        ballAnim.SetBool("goal", true);
        yield return new WaitForSeconds(0.5f);
        holeAnim.SetBool("goal", false);
        ballAnim.SetBool("goal", false);
        rb.velocity = Vector2.zero;
        onTurnEnd?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "hole")
        {
            ballTransform.DOMove(hole.position, 0.5f);
            StartCoroutine(End());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "centerWind")
        {
            rb.AddForce(wind.center * wind.windForce);
        }
        else if(collision.gameObject.tag == "L2RWind")
        {
            rb.AddForce(wind.L2R * wind.windForce);
        }
        else if(collision.gameObject.tag == "R2LWind")
        {
            rb.AddForce(wind.R2L * wind.windForce);
        }

    }

    private void Update()
    {
        if (isMoving)
        {
            isStoping();
        }
    }

    private void Start()
    {
        ballTransform = GetComponent<Transform>();
        powerLine = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        isMoving = false;
    }
}
