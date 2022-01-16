using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevelDoor : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> blackKey;
    [SerializeField] private SpriteRenderer door;
    [SerializeField] private Sprite openDoor;
    [SerializeField] private Sprite Yellowkey;
    [SerializeField] private GameObject interactTrigger;

    private int maxKey;
    void Start()
    {
        maxKey = blackKey.Count;
    }

    void Update()
    {
        if(ItemsCountHolder.needChageSpriteBlackKey)
        {
            int countKey = ItemsCountHolder.countKeysUp;
            blackKey[countKey-1].sprite = Yellowkey;
            ItemsCountHolder.needChageSpriteBlackKey = false;

            if(countKey == maxKey)
            {
                door.sprite = openDoor;
                interactTrigger.SetActive(true);
            }
        }
    }
}
