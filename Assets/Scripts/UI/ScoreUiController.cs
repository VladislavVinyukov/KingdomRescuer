using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUiController : MonoBehaviour
{
    // 1 apple = 100 score

    [SerializeField] private Text appleCountText;
    [SerializeField] private Text scoreCountText;
    [SerializeField] private LevelLoader levelLoader;
    private int appleCount;
    private static int totalScore =0;

    private float scoreTime = 1f;
    private float oneTickScore, _oneTickScore;
    private bool startCount = false;

    void Start()
    {
        //take count of apples from last complete level
        appleCount = ItemsCountHolder.appleSaved;
        appleCountText.text = appleCount.ToString();
        scoreCountText.text = totalScore.ToString();

        oneTickScore = scoreTime / appleCount;
        _oneTickScore = oneTickScore;


        StartCoroutine(StartCount());
    }
    IEnumerator StartCount()
    {
        yield return new WaitForSeconds(2f);
        startCount = true;
    }

    void FixedUpdate()
    {
        if (startCount)
        {
            oneTickScore -= Time.deltaTime;
            if (oneTickScore < 0)
            {
                if (appleCount > 0)
                {
                    appleCount--;
                    totalScore += 100;
                    appleCountText.text = appleCount.ToString();
                    scoreCountText.text = totalScore.ToString();
                }
                oneTickScore = _oneTickScore;
            }
            //all counting items = 0
            if(appleCount == 0)
            {
                ItemsCountHolder.appleSaved = 0;
                ItemsCountHolder.countKeysUp = 0;
                StartCoroutine(StartNextScene());
            }
        }
    }

    IEnumerator StartNextScene()
    {
        yield return new WaitForSeconds(2f);
        levelLoader.NextLevel();
    }

}
