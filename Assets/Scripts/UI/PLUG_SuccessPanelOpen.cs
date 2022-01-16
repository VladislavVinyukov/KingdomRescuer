using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLUG_SuccessPanelOpen : MonoBehaviour
{
    [SerializeField] private GameObject winPannel;
    private GameObject Player;

    private void Update()
    {
        if (Player != null)
        {
                winPannel.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            Player = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Player = null;
        }
    }
}
