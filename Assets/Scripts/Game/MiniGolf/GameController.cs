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
    [SerializeField] private BotController botCon;

    //private void SwitchTurn()
    //{

    //}

    private void nextLevel()
    {
        if (RedTurn)
        {
            lstLevels[recentLevel].SetActive(false);
            newLevel = Random.Range(0, 6);
            lstLevels[newLevel].SetActive(true);
            recentLevel = newLevel;
        }
    }

    IEnumerator SwitchTurn()
    {
        holeTrans.position = ballPos;
        ballPos *= -1;
        ballTrans.position = ballPos;
        bool flag = RedTurn;
        RedTurn = BlueTurn;
        BlueTurn = flag;
        yield return null;
    }

    IEnumerator checkTurn()
    {
        while (true)
        {
            yield return null;
            if (RedTurn)
            {
                ballCon.StartRedTurn();
            }
            else
            {
                ballCon.StartBlueTurn();
            }
        }
    }

    public void TurnEnd()
    {
        StartCoroutine(SwitchTurn());
        
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
        StartCoroutine(checkTurn());
    }

}
