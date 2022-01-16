using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void LoadScoreLevel()
    {
        StartCoroutine(LoadScoreLevelCorutine());
    }
    public void RestartLevel()
    {
        StartCoroutine(RestartLevelCorutine());
    }
    public void NextLevel()
    {
        StartCoroutine(NextLevelCorutine());
    }
    IEnumerator LoadScoreLevelCorutine()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        ManagerScene.ScoreScene();
    }
    IEnumerator RestartLevelCorutine()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        ManagerScene.RestartLevel();
    }
    IEnumerator NextLevelCorutine()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        ManagerScene.NextScene();
    }

}
