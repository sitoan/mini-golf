using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public bool RedTurn;
    public bool BlueTurn;
    public int TurnRecent;
    public Vector2 ballPos;
    private int newLevel;
    private int recentLevel;
    [SerializeField] public Transform ballTrans;
    [SerializeField] public Transform holeTrans;
    [SerializeField] private BallController ballCon;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject hole;
    [SerializeField] private List<GameObject> lstLevels;
    [SerializeField] private BotController bonCon;


    private void nextLevel()
    {
        lstLevels[recentLevel].SetActive(false);
        newLevel = Random.Range(0, 6);
        lstLevels[newLevel].SetActive(true);
        recentLevel = newLevel;
    }

   
    public void checkTurn()
    {
        if (RedTurn)
        {
            nextLevel();
            ballCon.StartRedTurn();
        }
        else
        {
            ballCon.StartBlueTurn();
        }

    }

    public void TurnEnd()
    {
        holeTrans.position = ballPos;
        ballPos *= -1;
        ballTrans.position = ballPos;
        bool flag = RedTurn;
        RedTurn = BlueTurn;
        BlueTurn = flag;
        checkTurn();
    }


    private void Start()
    {
        if(ball == null || hole == null)
        {
            ball = GameObject.FindGameObjectWithTag("Player");
            hole = GameObject.FindGameObjectWithTag("hole");
        }
        ballTrans = ball.GetComponent<Transform>();
        holeTrans = hole.GetComponent<Transform>();
        ballCon = ball.GetComponent<BallController>();
        ballPos = ballTrans.position;
        RedTurn = true;
        lstLevels[0].SetActive(true);
        recentLevel = 0;
        ballCon.onTurnEnd = TurnEnd;
        ballCon.StartRedTurn();
    }
}
