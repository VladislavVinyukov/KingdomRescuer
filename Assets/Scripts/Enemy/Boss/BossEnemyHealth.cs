using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class BossEnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private BossHealthBar healthBar;

    private int currentHealth;
    private Animator animator;

    public bool isInvulnerableEnemy = false;
    public bool enemyIsAlive;
    public bool inRage = false;

    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
    }
    public void TakeDamage(int damage)
    {
        if (isInvulnerableEnemy)
            return;
        currentHealth -= damage;
        animator.SetTrigger("TakeDamage");
        healthBar.SetHealth(currentHealth);
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
        animator.SetBool("isDeath", !enemyIsAlive);
    }

    public void DestroyObj()
    {
            Destroy(gameObject);
    }
}
