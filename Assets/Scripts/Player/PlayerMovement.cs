using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private AnimationCurve curveSpeed;

    [Header("Other")]
    private Rigidbody2D rb;
    private PlayerAnimation playerAnimation;
    private RangeAtack rangeAtack;
    private bool isrevertright = false;

    [Header("IsGrounded")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCollTransform;
    [SerializeField] private float collOffset;
    private bool isGrounded = false;
    private Color rayColor;
    private CapsuleCollider2D groundColl;

    [Header ("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField, Range(1,3)] private int maxDoubleJump;
    private int _maxDoubleJump;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        rb = GetComponent<Rigidbody2D>();
        groundColl = groundCollTransform.gameObject.GetComponent<CapsuleCollider2D>();
        rangeAtack = GetComponent<RangeAtack>();
        _maxDoubleJump = maxDoubleJump;
    }
    private void FixedUpdate()
    {
        //Vector2 overlapspherePos = groundCollTransform.position;
        //проверка isground линией под персонажем
        isGrounded = Physics2D.Raycast(groundColl.bounds.center - new Vector3(groundColl.bounds.extents.x, groundColl.bounds.extents.y + collOffset), Vector2.right, new Vector3(groundColl.bounds.size.x, collOffset).magnitude, groundLayer);
        //isGrounded = Physics2D.BoxCast(groundColl.bounds.center, groundColl.bounds.size, 0f, Vector2.down, collOffset, groundLayer);
        //нижн€€ граница бокса
        #region debugDraw
        if (isGrounded)
            rayColor = Color.green;
        else rayColor = Color.red;
        Debug.DrawRay(groundColl.bounds.center - new Vector3(groundColl.bounds.extents.x, groundColl.bounds.extents.y + collOffset), Vector2.right * new Vector3(groundColl.bounds.size.x, collOffset), rayColor);
        #endregion
    }
    private void Update()
    {
        playerAnimation.IsGrounded(isGrounded);
    }

    public void Move(float dir)
    {
        if (Mathf.Abs(dir) > 0.01f)
        {
            //это проверка на переворот персонажа, чтоб она не происходила всегда, добавил флажок "уже перевернут"
            if (dir > 0f && isrevertright)
            {
                Flip();
            }
            else if (dir < 0f && !isrevertright)
            {
                Flip();
            }
            //ходьба
            HorizontalMovment(dir);
        }
    }
    private void Flip()
    {
        rangeAtack.DirPlayerPos(isrevertright);
        transform.Rotate(0f, 180f, 0f);
        isrevertright = !isrevertright;

    }
    public void Jump(bool jumpButton, bool doubleJumpButton)
    {
        // прыжок и включение-выключение анимации прыжка
        if (jumpButton && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            playerAnimation.AnimJumped(true);

            SoundManager.PlaySound(SoundManager.Sound.PlayerJump, transform.position);
        }
        else if (doubleJumpButton)
        {
            if (maxDoubleJump > 0)
            {
                maxDoubleJump--;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                SoundManager.PlaySound(SoundManager.Sound.PlayerJump, transform.position);
                playerAnimation.AnimDoubleJump(true);
            }
        }
        else if (isGrounded)
        {
            playerAnimation.AnimJumped(false);
            playerAnimation.AnimDoubleJump(false);
            ResetDoubleJump();
        }
    }
    public void ResetDoubleJump()
    {
        maxDoubleJump = _maxDoubleJump;
    }
    private void HorizontalMovment(float dir)
    {
        rb.velocity = new Vector2(curveSpeed.Evaluate(dir), rb.velocity.y);
    }
    public bool PlayerOnPlatform()
    {
        //проверка коллизии дл€ платформы
        if (isGrounded)
            return true;
        else
            return false;
    }
}

