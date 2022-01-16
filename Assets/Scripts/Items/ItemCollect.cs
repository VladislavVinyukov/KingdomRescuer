using System.Collections;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    [SerializeField] private ParticleSystem iteamCatchParticleSystem;
    [SerializeField] private GameObject appleGFX;


    //возможно нужна проверка для избежания повторного захода в триггер
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ItemsCountHolder.AppleIncrese();
            iteamCatchParticleSystem.Play();
            appleGFX.SetActive(false);
            StartCoroutine(DestroyItem());
            SoundManager.PlaySound(SoundManager.Sound.ApplePickUp, transform.position);
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    IEnumerator DestroyItem()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
