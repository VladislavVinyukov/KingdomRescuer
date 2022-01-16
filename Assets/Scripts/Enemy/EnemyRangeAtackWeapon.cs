using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAtackWeapon : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField, Range(0.1f, 3f)] private float lifeTimePlayerRangeAtack;
    private void Start()
    {
        StartCoroutine(LifeTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else Destroy(gameObject);
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTimePlayerRangeAtack);
        Destroy(gameObject);
    }

}
