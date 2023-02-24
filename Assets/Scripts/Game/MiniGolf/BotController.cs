

using UnityEngine;
using DG.Tweening;
using System.Collections;

public class BotController : MonoBehaviour
{

    [SerializeField] private BallController ballCon;
    [SerializeField] private Animator botAnim;
    [SerializeField] private GameObject obj;
    public Vector2 direction;
    [SerializeField] private Transform ball;
    [SerializeField] private predictObject objCon;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    private float recentDirection;
    [SerializeField] private int rotateTime;

    public void StartTurn()
    {
        obj.SetActive(true);
        StartCoroutine(predict());
    }

    IEnumerator predict()
    {
        while (!objCon.InHole)
        {
            rb.isKinematic = true;
            ballRotate();
            objCon.resetObj();       
            direction = obj.transform.position - ball.position;
            launch(obj);
            yield return new WaitForSeconds(3.0f);
        }
        if (objCon.InHole)
        {
            rb.isKinematic = false;
            obj.SetActive(false);
            launch(gameObject);
            ballCon.isMoving = true;
            ballCon.sittingTime = 0f;
            objCon.InHole = false;
            ball.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
        }
    }

    private void ballRotate()
    { 
        ball.Rotate(0.0f, 0.0f, 45.0f);
        rotateTime+=1;
        if(rotateTime > 3)
        {
            ball.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
            rotateTime = 0;
        }
    }

    public void launch(GameObject gameObject)
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(direction.normalized * speed);
    }
   
 
    void Start()
    {
        objCon = obj.GetComponent<predictObject>();
    }

}
