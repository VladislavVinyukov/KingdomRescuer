using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIControll : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField, Range(1,5)] private float moveSpeed;
    [SerializeField, Range(1,5)] private float agroRange;
    [SerializeField, Range(0,5)] private float meleeAttackRange;
    [SerializeField] private Transform meleeAtackPoint;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private float atackRange;
    [SerializeField] private int meleeDamage;

    private Rigidbody2D rb2d;
    private Transform enemyHomePos;
    private Animator animator;
    private EnemyHealth enemyHelath;
    private bool vertical = true;
    
    private float _enemyHomePos_x;
    private float _localScale_x, _localScale_y;
    private float waitTime = 1f;
    private float _waitTime = 1f;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyHomePos = GetComponent<Transform>();
        enemyHelath = GetComponent<EnemyHealth>();
        _localScale_x = transform.localScale.x;
        _localScale_y = transform.localScale.y;
    }

    private void Start()
    {
        _enemyHomePos_x = enemyHomePos.position.x;
    }
    
    void Update()
    {
        //чек расстояния до игрока (агро рейндж)
        float distancetoPlayer = Vector2.Distance(transform.position, playerTransform.position);

        //enemyStatus
        bool isAlive = enemyHelath.enemyIsAlive;

        if (Mathf.Approximately(Mathf.Round(transform.position.y * 10f) * 0.1f, Mathf.Round(playerTransform.position.y * 10f) * 0.1f))
        {
            vertical = true;
        }
        else vertical = false;



        if (isAlive)
        {
            if (distancetoPlayer < agroRange && vertical)
            {
                waitTime = _waitTime;
                if (distancetoPlayer > meleeAttackRange)
                {
                    FollowPlayer();
                }
                if (distancetoPlayer <= meleeAttackRange)
                {
                    animator.SetTrigger("Attack");
                }
            }
            else if (distancetoPlayer > agroRange || !vertical)
            {
                EnemyRunAnimation();
                waitTime = waitTime - Time.deltaTime;
                if (waitTime < 0)
                {
                    StopFollowingPlayer();
                }
            }
        }
    }

    private void EnemyRunAnimation()
    {
        animator.SetFloat("Velocity", Mathf.Abs(rb2d.velocity.x));
    }


    void FollowPlayer()
    {
        EnemyRunAnimation();
        if(transform.position.x < playerTransform.position.x)
        {
            //player on the left side from enemy
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(_localScale_x, _localScale_y);
        }
        else if(transform.position.x > playerTransform.position.x)
        {
            //player on the right side from enemy
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-_localScale_x, _localScale_y);
        }
    }
    void StopFollowingPlayer()
    {
        EnemyRunAnimation();
        if (transform.position.x < _enemyHomePos_x - 0.5f)
        {
            rb2d.velocity = new Vector2(moveSpeed / 2, 0);
            transform.localScale = new Vector2(_localScale_x,_localScale_y);
        }
        else if (transform.position.x > _enemyHomePos_x + 0.5f)
        {
            rb2d.velocity = new Vector2(-moveSpeed / 2, 0);
            transform.localScale = new Vector2(-_localScale_x,_localScale_y);
        }
        else return;
    }
    void AttackPlayer()
    {
        SoundManager.PlaySound(SoundManager.Sound.SwordSlice, transform.position);
        Collider2D hitPlayer = Physics2D.OverlapCircle(meleeAtackPoint.position, atackRange, playerLayer);
        if(hitPlayer != null)
        hitPlayer.GetComponentInParent<PlayerHealth>().TakeDamage(meleeDamage);
    }
    private void OnDrawGizmos()
    {
        if (meleeAtackPoint == null)
            return;
        Gizmos.DrawWireSphere(meleeAtackPoint.position, atackRange);
    }
}
