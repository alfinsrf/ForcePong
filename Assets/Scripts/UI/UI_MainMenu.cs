using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] UI_DarkScreen darkScreen;

    private void Awake()
    {
        if(darkScreen != null)
        {
            darkScreen.gameObject.SetActive(true);
        }
    }

    public void PlayWithPlayer()
    {
        StartCoroutine(LoadSceneWithFadeEffect("MainGamePlayer", 2));
    }
    
    public void PlayWithComputer()
    {
        StartCoroutine(LoadSceneWithFadeEffect("MainGameComputer", 2));
    }

    IEnumerator LoadSceneWithFadeEffect(string _sceneName, float _delay)
    {        
        darkScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(_sceneName);
    }

    public void QuitGame()
    {        
        Application.Quit();
    }
}