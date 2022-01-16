using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private bool cooldownRangeAtack = true;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float velocity = rb.velocity.magnitude;
        float velocityY = Mathf.Abs(rb.velocity.y);
        anim.SetFloat("velocity", velocity);
        anim.SetFloat("velocityY", velocityY);
    }
    public void IsGrounded(bool isGrounded)
    {
        anim.SetBool("isGrounded", isGrounded);
    }
    public void AnimJumped(bool value)
    {
        anim.SetBool("isJump", value);
    }
    public void AnimDoubleJump(bool value)
    {
        anim.SetBool("isDoubleJump", value);
    }
    public void RangeAtackStart()
    {
        if (cooldownRangeAtack)
        {
            anim.SetTrigger("rangeAtack");
            cooldownRangeAtack = !cooldownRangeAtack;
            StartCoroutine(cooldown());
        }
    }
    IEnumerator cooldown()
    {
        yield return  new WaitForSeconds(2);
        cooldownRangeAtack = !cooldownRangeAtack;
    }

    public void ResetTrigger()
    {
        anim.ResetTrigger("rangeAtack");
        anim.ResetTrigger("IdleAtack");
        anim.ResetTrigger("RunAtack");
    }

}
