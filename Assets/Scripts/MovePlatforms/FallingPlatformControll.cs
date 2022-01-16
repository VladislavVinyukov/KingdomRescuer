using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformControll : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb2d;
    private Vector3 homepos;
    [SerializeField, Range(0.1f,3)] private float fallTimer;
    [SerializeField, Range(1,5)] private float appearTimer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        homepos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(CountToFallingPlatform());
        }
    }

    IEnumerator CountToFallingPlatform()
    {
        yield return new WaitForSeconds(fallTimer);
        anim.SetBool("Appear", false);
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        StartCoroutine(CounttoAppearPlatform());
    }

    IEnumerator CounttoAppearPlatform()
    {
        yield return new WaitForSeconds(appearTimer);
        rb2d.bodyType = RigidbodyType2D.Static;
        transform.position = homepos;
        anim.SetBool("Appear", true);
    }
}
