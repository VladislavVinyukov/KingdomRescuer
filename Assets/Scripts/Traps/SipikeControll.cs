using System.Collections;
using UnityEngine;

public class SipikeControll : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void StopAnim()
    {
        animator.enabled = false;
        StartCoroutine(RestartAnim());
    }
    IEnumerator RestartAnim()
    {
        yield return new WaitForSeconds(2f);
        animator.enabled = true;
    }


}
