using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperController : MonoBehaviour
{
    public float forceJumper;
    private Animator anim;
    private GameObject player;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            player = collision.gameObject;
            anim.SetBool("jumperGo", true);
            SoundManager.PlaySound(SoundManager.Sound.JumperSound, transform.position);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            player.GetComponentInParent<PlayerMovement>().ResetDoubleJump();
            anim.SetBool("jumperGo", false);
        }
    }

    //start from anim of Jumper
    public void PlayerJumperForce()
    {
        if(player != null)
        {
            player.GetComponentInParent<Rigidbody2D>().AddForce(Vector2.up * forceJumper, ForceMode2D.Force );
        }
    }

}
