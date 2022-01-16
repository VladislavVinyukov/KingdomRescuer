using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireMageBossAIControll : MonoBehaviour
{
    [Header ("Need to connect")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject rangeAtack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private List<Transform> points;

    [Header ("Settings")]
    [SerializeField] private float agroRange;
    [SerializeField] private float teleportRange;
    [SerializeField] private float firespeed;
    [SerializeField] private float distRangeAtack;
    [SerializeField] private float coolDownRangeAttack;
    
    
    private BossEnemyHealth enemyHealth;
    private Animator animator;
    private Vector2 boundsPlayer;
    private bool canTeleport = true;
    private int currentPoint = 0;
    private int pointIndex;
    private bool isCooldownRangeAtack = false;
    private float _localScale_x, _localScale_y;

    private bool inRage;

    private void Awake()
    {
        boundsPlayer = player.GetComponentInChildren<CapsuleCollider2D>().bounds.extents;
        enemyHealth = GetComponent<BossEnemyHealth>();
        animator = GetComponent<Animator>();
        _localScale_x = transform.localScale.x;
        _localScale_y = transform.localScale.y;
    }
    private void Update()
    {
        //DebugDraw();
        bool isAlive = enemyHealth.enemyIsAlive;
        inRage = enemyHealth.inRage;
        //чек расстояния до игрока (агро рейндж)
        float distancetoPlayer = Vector2.Distance(transform.position, player.position);


        if (isAlive)
            // добавить триггер входа в босс зону
        {
            RotateCharector();
            if(distancetoPlayer < teleportRange && canTeleport)
                StartCoroutine(TimerToTeleport());
            if (distancetoPlayer <= distRangeAtack && !isCooldownRangeAtack)
                    animator.SetTrigger("Attack");
        }
    }

    IEnumerator TimerToTeleport()
    {
        canTeleport = false;
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("StartTeleport");
    }
    //запуск в конце анимации исчезновения
    void Teleport()
    {
        do
        {
            pointIndex = Random.Range(0, points.Count);
        }
        while (pointIndex == currentPoint);
        transform.position = points[pointIndex].position;
        currentPoint = pointIndex;
        canTeleport = true;
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
        if (!inRage)
        {
            isCooldownRangeAtack = !isCooldownRangeAtack;
            StartCoroutine(cooldown());
            GameObject RangeAtack = Instantiate(rangeAtack, firePoint.position, Quaternion.identity);
            Rigidbody2D currentRangeAtackVelocity = RangeAtack.GetComponent<Rigidbody2D>();
            Vector2 dir = new Vector2(player.position.x - firePoint.position.x + boundsPlayer.x, player.position.y - firePoint.position.y + boundsPlayer.y).normalized;
            float angle = Mathf.Atan2(player.position.y - firePoint.position.y + boundsPlayer.y, player.position.x - firePoint.position.x) * Mathf.Rad2Deg;
            RangeAtack.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            currentRangeAtackVelocity.velocity = dir * firespeed;
        }
        else
        {
            isCooldownRangeAtack = !isCooldownRangeAtack;
            StartCoroutine(cooldown());
            GameObject RangeAtackUp = Instantiate(rangeAtack, firePoint.position, Quaternion.identity);
            GameObject RangeAtack = Instantiate(rangeAtack, firePoint.position, Quaternion.identity);
            GameObject RangeAtackDown = Instantiate(rangeAtack, firePoint.position, Quaternion.identity);

            Rigidbody2D currentRangeAtackVelocityUp = RangeAtackUp.GetComponent<Rigidbody2D>();
            Rigidbody2D currentRangeAtackVelocity = RangeAtack.GetComponent<Rigidbody2D>();
            Rigidbody2D currentRangeAtackVelocityDown = RangeAtackDown.GetComponent<Rigidbody2D>();

            Vector2 dirUp = new Vector2(player.position.x - firePoint.position.x + boundsPlayer.x, player.position.y - firePoint.position.y + boundsPlayer.y *4).normalized;
            Vector2 dir = new Vector2(player.position.x - firePoint.position.x + boundsPlayer.x, player.position.y - firePoint.position.y + boundsPlayer.y).normalized;
            Vector2 dirDown = new Vector2(player.position.x - firePoint.position.x + boundsPlayer.x , player.position.y - firePoint.position.y - boundsPlayer.y *3).normalized;

            float angleUp = Mathf.Atan2(player.position.y - firePoint.position.y + boundsPlayer.y * 4, player.position.x - firePoint.position.x + boundsPlayer.x) * Mathf.Rad2Deg;
            float angle = Mathf.Atan2(player.position.y - firePoint.position.y + boundsPlayer.y, player.position.x - firePoint.position.x + boundsPlayer.x) * Mathf.Rad2Deg;
            float angleDown = Mathf.Atan2(player.position.y - firePoint.position.y - boundsPlayer.y * 3, player.position.x - firePoint.position.x + boundsPlayer.x) * Mathf.Rad2Deg;

            RangeAtackUp.transform.rotation = Quaternion.Euler(0, 0, angleUp +90);
            RangeAtack.transform.rotation = Quaternion.Euler(0, 0, angle +90);
            RangeAtackDown.transform.rotation = Quaternion.Euler(0, 0, angleDown + 90);

            currentRangeAtackVelocityUp.velocity = dirUp * firespeed;
            currentRangeAtackVelocity.velocity = dir * firespeed;
            currentRangeAtackVelocityDown.velocity = dirDown * firespeed;
        }    
    }
    private void DebugDraw()
    {
        Debug.DrawRay(firePoint.position, new Vector3(player.position.x - firePoint.position.x + boundsPlayer.x, player.position.y - firePoint.position.y + boundsPlayer.y * 4), Color.blue);
        Debug.DrawRay(firePoint.position, new Vector3(player.position.x - firePoint.position.x + boundsPlayer.x, player.position.y - firePoint.position.y + boundsPlayer.y), Color.green);
        Debug.DrawRay(firePoint.position, new Vector3 (player.position.x - firePoint.position.x + boundsPlayer.x, player.position.y - firePoint.position.y - boundsPlayer.y * 3), Color.red);

    }
}
