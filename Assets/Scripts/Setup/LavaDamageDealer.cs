using UnityEngine;

public class LavaDamageDealer : MonoBehaviour
{
    [SerializeField, Range(10,40)] private int lavaDamage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag.Equals("Player"))
        {
            collision.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(lavaDamage);
        }
    }
}
