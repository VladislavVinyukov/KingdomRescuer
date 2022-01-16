using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInputs : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;
    private PlayerCombat playerCombat;
    private PlayerHealth playerHealth;

    [Header("Joystick")]
    [SerializeField] private Joystick joystick;
    private bool jumpButtonCanvas;
    private float horizontalDir;
    public static bool controltype = true;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerCombat = GetComponent<PlayerCombat>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if(controltype)
        PCMovment();
        else
        MobileMovment();
    }
    private void PCMovment()
    {
        // œ–≈ƒ¬»∆≈Õ»≈ Õ¿  À¿¬»¿“”–≈
        float horizontalDir = Input.GetAxis(GlobalStringVars.HORIZONTAL_AXIX);
        bool isJumpButtonPressed = Input.GetButtonDown(GlobalStringVars.JUMP);

        if (Input.GetButtonDown(GlobalStringVars.FIRE_RMB))
        {
            playerAnimation.RangeAtackStart();
        }
        if (Input.GetButtonDown(GlobalStringVars.FIRE_LMB))
        {
            playerCombat.MeleeAtack(horizontalDir);
        }

        if (playerHealth.isAlive)
        {
            playerMovement.Move(horizontalDir);
            playerMovement.Jump(isJumpButtonPressed, isJumpButtonPressed);
        }
    }

    private void MobileMovment()
    {
        //œ≈–≈ƒ¬»∆≈Õ»≈ Õ¿ “≈À≈‘ŒÕ≈
        horizontalDir = joystick.Horizontal;

        if (playerHealth.isAlive)
        {
            playerMovement.Jump(jumpButtonCanvas, jumpButtonCanvas);
            playerMovement.Move(horizontalDir);
            jumpButtonCanvas = false;
        }
    }
    public void JumpButtonClick()
    {
        jumpButtonCanvas = true;
    }
    public void MeleeAttackButtonCLick()
    {
        playerCombat.MeleeAtack(horizontalDir);
    }
    public void RangeAttackButtonCLick()
    {
        playerAnimation.RangeAtackStart();
    }

}
