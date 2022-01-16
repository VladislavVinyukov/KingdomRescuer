using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel3TriggerControll : MonoBehaviour
{
    [SerializeField] private GameObject boss1;
    [SerializeField] private GameObject boss2;
    [SerializeField] private GameObject boss3;
    [SerializeField] private GameObject boss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            boss1.SetActive(true);
            boss2.SetActive(true);
            boss3.SetActive(true);
        }
    }
    private void Update()
    {
        if(!boss.GetComponentInChildren<BossEnemyHealth>().enemyIsAlive)
        {
            StartCoroutine(StartScoreScene());
        }
    }
    IEnumerator StartScoreScene()
    {
        yield return new WaitForSeconds(2);
            ManagerScene.ScoreScene();
    }

}
