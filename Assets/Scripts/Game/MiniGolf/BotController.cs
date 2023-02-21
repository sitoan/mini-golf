using System;
using System.Collections;
using System.Collections.Generic;
using MB.PhysicsPrediction;
using UnityEngine;

public class BotController : MonoBehaviour
{
    BallController ballCon;
    [SerializeField] private GameController gameCon;
    [SerializeField] private Animator botAnim;
    PredictionTimeline timeLine;
    [SerializeField] private GameObject predictLine;
 

    [SerializeField]
    PredictionProperty prediction = default;
    [Serializable]
    public class PredictionProperty
    {
        [SerializeField]
        public int iterations = 40;
        public int Iterations => iterations;

        [SerializeField]
        LineRenderer line = default;
        public LineRenderer Line => line;
    }


    private void botLaunch(GameObject gameObject)
    {
        PredictionSystem.Record.Prefabs.Remove(timeLine);
    }

    public void StartTurn()
    {
        StartCoroutine(produce());
    }


    IEnumerator produce()
    {
        prediction.Line.enabled = true;
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            predict();
        }
    }

    void predict()
    {
        PredictionSystem.Simulate(20);
        TrajectoryPredictionDrawer.ShowAll();
        botAnim.SetBool("bot", true);
        timeLine = PredictionSystem.Record.Prefabs.Add(gameObject, botLaunch);
        prediction.Line.positionCount = timeLine.Count;
        for (int i = 0; i < timeLine.Count; i++)
        {
             prediction.Line.SetPosition(i, timeLine[i].Position);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
