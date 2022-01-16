using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleateTrigger : MonoBehaviour
{
    public LevelLoader levelLoader;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            levelLoader.LoadScoreLevel();
            //ManagerScene.ScoreScene();
        }
    }
}
