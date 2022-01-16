using System.Collections;
using UnityEngine;

public class DamageDealler : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField, Range(0.1f,3f)] private float lifeTimePlayerRangeAtack;
    private void Start()
    {
        StartCoroutine(LifeTime());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damageable"))
        {
            if(collision.gameObject.GetComponentInParent<EnemyHealth>())
            collision.gameObject.GetComponentInParent<EnemyHealth>().TakeDamage(damage);
            if(collision.gameObject.GetComponentInParent<BossEnemyHealth>())
            collision.gameObject.GetComponentInParent<BossEnemyHealth>().TakeDamage(damage);
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
