using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ManagerScene
{
    private static int lastSceneIndex;
    public static void RestartLevel()
    {
        ItemsCountHolder.AppleDecreaseToStart();
        ItemsCountHolder.KeyDecreaseToStart();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public static void ScoreScene()
    {
        lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("ScoreCounter");
    }

    public static void NextScene()
    {
        SceneManager.LoadScene(lastSceneIndex +1);
    }

}
