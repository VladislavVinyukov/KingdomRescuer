using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireMageAIControll : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private GameObject rangeAtack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float firespeed;
    [SerializeField] private float agroRange;
    [SerializeField] private float distRangeAtack;

    [SerializeField] private bool isRotate = true;
    [SerializeField] private float coolDownRangeAttack;
    
    
    private EnemyHealth enemyHealth;
    private Animator animator;
    private Vector2 boundsPlayer;
    private Vector3 boundsPlayer_v3;
    private RaycastHit2D raycastHit2D;

    private bool isCooldownRangeAtack = false;
    private float _localScale_x, _localScale_y;

    private void Awake()
    {
        boundsPlayer = player.GetComponentInChildren<CapsuleCollider2D>().bounds.extents;
        boundsPlayer_v3 = player.GetComponentInChildren<CapsuleCollider2D>().bounds.extents;
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
        _localScale_x = transform.localScale.x;
        _localScale_y = transform.localScale.y;
    }
    private void FixedUpdate()
    {
       // Debug.DrawRay(firePoint.position, player.position - firePoint.position + boundsPlayer_v3);
    }
    private void Update()
    {
        bool isAlive = enemyHealth.enemyIsAlive;

        //чек расстояния до игрока (агро рейндж)
        float distancetoPlayer = Vector2.Distance(transform.position, player.position);
        try
        {
            raycastHit2D = Physics2D.Raycast(firePoint.position, player.position - firePoint.position + boundsPlayer_v3, distancetoPlayer, groundLayer);
        }
        catch 
        {

        }
        if (isAlive)
        {
            if (distancetoPlayer < agroRange)
            {
                RotateCharector();
                if (distancetoPlayer > distRangeAtack)
                {
                    // follow
                }
                if (distancetoPlayer <= distRangeAtack && !isCooldownRangeAtack && raycastHit2D.collider == null)
                {
                    animator.SetTrigger("Attack");
                }
            }
            else if (distancetoPlayer > agroRange)
            {
                //StopFollowingPlayer();
            }
        }
    }
    void RotateCharector()
    {
        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector2(_localScale_x, _localScale_y);
        }
        else if (transform.position.x > player.position.x)
        {
            transform.localScale = new Vector2(-_localScale_x, _localScale_y);
        }
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(coolDownRangeAttack);
        isCooldownRangeAtack = !isCooldownRangeAtack;
    }
    public void Shooting()
    {
        isCooldownRangeAtack = !isCooldownRangeAtack;
        StartCoroutine(cooldown());

        //выстрел вправо
        if (isRotate)
        {
            GameObject RangeAtack = Instantiate(rangeAtack, firePoint.position, Quaternion.Euler(0,0,90));
            Rigidbody2D currentRangeAtackVelocity = RangeAtack.GetComponent<Rigidbody2D>();
            Vector2 dir = new Vector2(player.position.x - firePoint.position.x + boundsPlayer.x , player.position.y - firePoint.position.y + boundsPlayer.y).normalized;
            float angle = Mathf.Atan2(player.position.y - firePoint.position.y + boundsPlayer.y, player.position.x - firePoint.position.x) * Mathf.Rad2Deg;
            RangeAtack.transform.rotation = Quaternion.Euler(0, 0, angle + 90);

            currentRangeAtackVelocity.velocity = dir * firespeed;
        }
        //выстрел влево
        //else if (!isRotate)
        //{
        //    GameObject currentRangeAtack = Instantiate(rangeAtack, firePoint.position, Quaternion.Inverse(rangeAtack.transform.rotation));
        //    Rigidbody2D currentRangeAtackVelocity = currentRangeAtack.GetComponent<Rigidbody2D>();
        //    currentRangeAtackVelocity.velocity = new Vector2(firespeed * -1, currentRangeAtackVelocity.velocity.y);
        //}
    }
}
