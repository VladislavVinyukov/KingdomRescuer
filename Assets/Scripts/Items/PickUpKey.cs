using System.Collections;
using UnityEngine;

public class PickUpKey : MonoBehaviour
{
    [SerializeField] private ParticleSystem iteamCatchParticleSystem;
    [SerializeField] private GameObject keyGFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SoundManager.PlaySound(SoundManager.Sound.keyPickUp, transform.position);
            ItemsCountHolder.KeyIncrease();
            iteamCatchParticleSystem.Play();
            keyGFX.SetActive(false);
            StartCoroutine(DestroyItem());
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    IEnumerator DestroyItem()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}

