using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiPatrol : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;
    [SerializeField, Range(1, 5)] private float moveSpeed;
    [SerializeField, Range(1, 5)] private float agroRange;
    [SerializeField, Range(0, 5)] private float meleeAttackRange;
    [SerializeField] private Transform meleeAtackPoint;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform leftBorderPos;
    [SerializeField] private Transform rightBorderPos;

    [SerializeField] private float atackRange;
    [SerializeField] private int meleeDamage;

    [SerializeField, Range(1,5)] private float whaitTime;

    private Rigidbody2D rb2d;
    private Transform enemyHomePos;
    private Animator animator;
    private EnemyHealth enemyHelath;
    private bool vertical = true;
    private float distancetoPlayer;
    private bool isAlive;
    private bool inLeftBorderZone, inRightBorderZone;

    private float _enemyHomePos_x;
    private float _localScale_x, _localScale_y;
    private const int patrolState = 0;
    private const int chaseState = 1;
    private const int revertState = 2;
    private int currentState;

    private const int moveToRightBorder = 0;
    private const int moveToLeftBorder = 1;
    private int currentMoveState;


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
        currentState = patrolState;
        currentMoveState = moveToRightBorder;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyRunAnimation();

        //чек расстояния до игрока (агро рейндж)
        distancetoPlayer = Vector2.Distance(transform.position, playerTransform.position);

        //enemyStatus
        isAlive = enemyHelath.enemyIsAlive;

        if (Mathf.Approximately(Mathf.Round(transform.position.y * 10f) * 0.1f, Mathf.Round(playerTransform.position.y * 10f) * 0.1f))
        {
            vertical = true;
        }
        else vertical = false;

        if (distancetoPlayer > agroRange)
             currentState = patrolState; //patrol
        else if (distancetoPlayer < agroRange && vertical)
             currentState = chaseState; //chase



        switch(currentState)
        {
            case patrolState: Patrol();
                break;
            case chaseState: Chase();
                break;
        }

         

    }

    private void Patrol()
    {
        switch (currentMoveState)
        {
            case moveToRightBorder:
                if (!inRightBorderZone)
                {
                    rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
                    transform.localScale = new Vector2(_localScale_x, _localScale_y);
                }

                break;

            case moveToLeftBorder:
                if (!inLeftBorderZone)
                {
                    rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
                    transform.localScale = new Vector2(-_localScale_x, _localScale_y);
                }
                break;
        }
    }

    private void Chase()
    {
        if (isAlive && vertical)
        {
                if (distancetoPlayer > meleeAttackRange )
                {
                    FollowPlayer();
                }
                if (distancetoPlayer <= meleeAttackRange)
                {
                    animator.SetTrigger("Attack");
                }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("LeftBorder" ))
        {
            inLeftBorderZone = true;
            rb2d.velocity = new Vector2(0,0);
            StartCoroutine(RevertMoveState());
        }
        if (collision.gameObject.tag.Equals("RightBorder"))
        {
            inRightBorderZone = true;
            rb2d.velocity = new Vector2(0, 0);
            StartCoroutine(RevertMoveState());
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("LeftBorder"))
        {
            inLeftBorderZone = false;
        }
        if (collision.gameObject.tag.Equals("RightBorder"))
        {
            inRightBorderZone = false;
        }
    }
    IEnumerator RevertMoveState()
    { //if (currentState != chaseState)
        {
            yield return new WaitForSeconds(whaitTime);
            if (currentMoveState == moveToRightBorder)
                currentMoveState = moveToLeftBorder;
            else
                currentMoveState = moveToRightBorder;
        }
    }

    private void EnemyRunAnimation()
    {
        animator.SetFloat("Velocity", Mathf.Abs(rb2d.velocity.x));
    }
    void FollowPlayer()
    {
        if (transform.position.x < playerTransform.position.x && !inRightBorderZone)
        {
            //player on the right side from enemy
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector2(_localScale_x, _localScale_y);
        }
        else if (transform.position.x > playerTransform.position.x && !inLeftBorderZone)
        {
            //player on the left side from enemy
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector2(-_localScale_x, _localScale_y);
        }
    }
    void AttackPlayer()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(meleeAtackPoint.position, atackRange, playerLayer);
        if (hitPlayer != null)
            hitPlayer.GetComponentInParent<PlayerHealth>().TakeDamage(meleeDamage);
    }
}
