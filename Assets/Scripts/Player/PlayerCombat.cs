using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Transform meleeAtackPoint;
    public LayerMask enemyLayers;

    public float atackRange;
    public int meleeDamage;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void MeleeAtack(float dir)
    {
        if (Mathf.Abs(dir) >= 0.01)
            animator.SetTrigger("RunAtack");
        if (Mathf.Abs(dir) < 0.01)
            animator.SetTrigger("IdleAtack");
    }
    private void OnDrawGizmos()
    {
        if (meleeAtackPoint == null)
            return;
        Gizmos.DrawWireSphere(meleeAtackPoint.position, atackRange);
    }
    //наносим урон по запуску из анимации
    public void DealDamage()
    {
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(meleeAtackPoint.position, atackRange, enemyLayers);
        SoundManager.PlaySound(SoundManager.Sound.SwordSlice, transform.position);

        foreach (Collider2D enemy in hitEnemy)
        {
            try
            {
                enemy.GetComponentInParent<EnemyHealth>().TakeDamage(meleeDamage);
                
            }
            catch { enemy.GetComponentInParent<BossEnemyHealth>().TakeDamage(meleeDamage); }

        }
    }
}
