using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    
    private int wallCollisionCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Invoke("StartBall", 5);
    }

    private void StartBall()
    {
        float randomDir = Random.Range(0, 2);
        if (randomDir < 1)
        {
            rb.AddForce(new Vector2(30, 0));
        }
        else
        {
            rb.AddForce(new Vector2(-30, 0));
        }
    }

    private void PlayBall()
    {        
        if(GameManager.instance.ballForRed == true)
        {
            rb.AddForce(new Vector2(25, 0));
            GameManager.instance.ballForRed = false;
        }
        else if(GameManager.instance.ballForBlue == true)
        {
            rb.AddForce(new Vector2(-25, 0));
            GameManager.instance.ballForBlue = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float randomDirY = Random.Range(-10, 10);

        if(collision.gameObject.tag == "Player")
        {
            rb.AddForce(new Vector2(15, randomDirY));
            StartCoroutine(CameraManager.instance.ShakeCamera(GameManager.instance.playerCollisionDuration, GameManager.instance.playerCollisionMagnitude));
            wallCollisionCount = 0;
        }
        else if (collision.gameObject.tag == "Player2")
        {
            rb.AddForce(new Vector2(-15, randomDirY));
            StartCoroutine(CameraManager.instance.ShakeCamera(GameManager.instance.playerCollisionDuration, GameManager.instance.playerCollisionMagnitude));
            wallCollisionCount = 0;
        }
        else
        {
            wallCollisionCount++;
            Debug.Log("Wall collision count: " + wallCollisionCount);
            if(wallCollisionCount >= 5)
            {
                StartBall();
                wallCollisionCount = 0;
            }           
        }
    }

    public void StopBall()
    {
        rb.velocity = Vector2.zero;
    }

    public void ResetBallPosition()
    {
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(0, -2);
        wallCollisionCount = 0;

        if(GameManager.instance.weHaveAWinner == false)
        {
            Invoke("PlayBall", 2f);            
        }
        else
        {
            Destroy(gameObject, 1);
        }
    }    
}
