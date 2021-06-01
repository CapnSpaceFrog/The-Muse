using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private Animator transition;

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

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(levelIndex);
    }

    public void AppQuit()
    {
        Application.Quit();
    }
}
