using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    //[HideInInspector]
    public bool isInvulnerablePlayer = false;
    [HideInInspector] public bool isAlive = true;

    public HealthBar healthBar;
    public LevelLoader levelLoader;

    private int currentHealth;
    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
    }
    public void TakeDamage(int damage)
    {
        if (isInvulnerablePlayer)
            return;
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        animator.SetTrigger("TakeDamage");
        CheckAlive();

    }
    private void CheckAlive()
    {
        if (currentHealth <= 0)
        {
            animator.SetBool("isDeath", true);
            rb.bodyType = RigidbodyType2D.Static;
            isAlive = false;
        }
        else
        {
            animator.SetBool("isDeath", false);
        }
    }

    //запуск из анмиации
    IEnumerator RestartInSec()
    {
        yield return new WaitForSeconds(0.5f);
        levelLoader.RestartLevel();
        //ManagerScene.RestartLevel();
    }

}
