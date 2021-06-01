using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private Animator transition;
    [SerializeField]
    private GameObject menuButtons;
    [SerializeField]
    private GameObject creditsMenu;
    [SerializeField]
    private GameObject tutorialMenu;

    private GameObject currentMenu;

    public void CreditsMenu()
    {
        menuButtons.SetActive(false);
        creditsMenu.SetActive(true);
        currentMenu = creditsMenu;
    }

    public void HowToPlayMenu()
    {
        menuButtons.SetActive(false);
        tutorialMenu.SetActive(true);
        currentMenu = tutorialMenu;
    }

    public void BackButton()
    {
        currentMenu.SetActive(false);
        menuButtons.SetActive(true);
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LevelLoadeDelay(0));
    }

    public void LoadGameplay()
    {
        StartCoroutine(LevelLoadeDelay(1));
    }

    public void ReloadLevel()
    {
        StartCoroutine(LevelLoadeDelay(1));
    }

    public void GameOver()
    {
        StartCoroutine(LevelLoadeDelay(2));
    }

    public void GameWon()
    {
        StartCoroutine(LevelLoadeDelay(3));
    }

    IEnumerator LevelLoadeDelay(int levelIndex)
    {
        transition.SetBool("start", true);
        Time.timeScale = 1;

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(levelIndex);
    }

    public void AppQuit()
    {
        Application.Quit();
    }
}
