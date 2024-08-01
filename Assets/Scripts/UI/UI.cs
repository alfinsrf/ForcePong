using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    private bool gamePaused;

    [Header("Menu Game Objects")]
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject gameWinnerUI;
    [SerializeField] private UI_DarkScreen darkScreen;

    [Header("Player Score Text")]
    public TextMeshProUGUI playerBlueScoreUI;
    public TextMeshProUGUI playerRedScoreUI;

    [Header("Winner Screen Components")]
    public TextMeshProUGUI congratulations;
    public TextMeshProUGUI playerGameWinner;
    public TextMeshProUGUI winnerDescription;
    public GameObject buttonToMainMenu;

    private void Awake()
    {
        darkScreen.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameWinnerUI.activeInHierarchy)
            {
                return;
            }
            CheckIfNotPaused();
        }
    }

    public void PauseButton() => CheckIfNotPaused();

    private bool CheckIfNotPaused()
    {
        if (!gamePaused)
        {
            gamePaused = true;
            Time.timeScale = 0;
            SwitchUI(pauseUI);
            return true;
        }
        else
        {
            gamePaused = false;
            Time.timeScale = 1;
            SwitchUI(inGameUI);
            return false;
        }
    }

    public void OnGameFinished()
    {        
        SwitchUI(gameWinnerUI);
        StartCoroutine(GameFinishedCoroutine());
    }

    IEnumerator GameFinishedCoroutine()
    {
        congratulations.gameObject.SetActive(false);
        playerGameWinner.gameObject.SetActive(false);
        winnerDescription.gameObject.SetActive(false);
        buttonToMainMenu.SetActive(false);

        yield return new WaitForSeconds(1f);
        congratulations.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        playerGameWinner.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        winnerDescription.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        buttonToMainMenu.SetActive(true);
    }

    public void SwitchUI(GameObject uiMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            bool darkScreen = transform.GetChild(i).GetComponent<UI_DarkScreen>() != null;

            if (darkScreen == false)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        
        uiMenu.SetActive(true);        
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        if(GameManager.instance.ball != null)
        {
            GameManager.instance.ball.StopBall();
        }
        darkScreen.FadeOut();

        StartCoroutine(LoadMainMenuCoroutine());
    }

    IEnumerator LoadMainMenuCoroutine()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }
}
