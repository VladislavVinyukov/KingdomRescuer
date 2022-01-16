using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    private Animator animator;

    public bool isInvulnerableEnemy = false;
    public bool enemyIsAlive = true;
    public bool inRage = false;

    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        if (isInvulnerableEnemy)
            return;
        currentHealth -= damage;
        animator.SetTrigger("TakeDamage");
        CheckAlive();
        if (currentHealth < maxHealth / 2)
            inRage = true;

    }
    private void CheckAlive()
    {
        if (currentHealth <= 0)
        {
            enemyIsAlive = false;
        }
        else
        {
            enemyIsAlive = true;
        }
        animator.SetBool("isDeath", enemyIsAlive);
    }

    public void DestroyObj()
    {
            Destroy(gameObject);
    }
}
