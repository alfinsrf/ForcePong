using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private UI inGameUI;    

    public Ball ball;

    [Header("Player Score")]
    public int maxPlayerScore;
    [SerializeField] private int playerBlueScore = 0;
    [SerializeField] private int playerRedScore = 0;

    [HideInInspector] public bool ballForBlue;
    [HideInInspector] public bool ballForRed;
    [HideInInspector] public bool weHaveAWinner;

    [Header("Screen Shake Value")]
    public float playerScoreDuration;
    public float playerScoreMagnitude;
    [Space]
    public float playerCollisionDuration;
    public float playerCollisionMagnitude;    

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inGameUI = GameObject.Find("Canvas").GetComponent<UI>();

        inGameUI.playerBlueScoreUI.text = playerBlueScore.ToString();
        inGameUI.playerRedScoreUI.text = playerRedScore.ToString();        
    }        

    public void ScoreCheck()
    {
        if(playerBlueScore == maxPlayerScore)
        {            
            weHaveAWinner = true;
            inGameUI.playerGameWinner.text = "PLAYER 1";
            inGameUI.OnGameFinished();
        }
        else if(playerRedScore == maxPlayerScore)
        {            
            weHaveAWinner = true;
            inGameUI.playerGameWinner.text = "PLAYER 2";
            inGameUI.OnGameFinished();
        }
    }

    public void Score(string territoryId)
    {
        if(territoryId == "Territory Blue")
        {
            playerRedScore++;
            inGameUI.playerRedScoreUI.text = playerRedScore.ToString();
            ballForBlue = true;
            ScoreCheck();
            StartCoroutine(CameraManager.instance.ShakeCamera(playerScoreDuration, playerScoreMagnitude));
        }
        else if(territoryId == "Territory Red")
        {
            playerBlueScore++;
            inGameUI.playerBlueScoreUI.text = playerBlueScore.ToString();
            ballForRed = true;
            ScoreCheck();
            StartCoroutine(CameraManager.instance.ShakeCamera(playerScoreDuration, playerScoreMagnitude));
        }

        ball.ResetBallPosition();
    }
}
