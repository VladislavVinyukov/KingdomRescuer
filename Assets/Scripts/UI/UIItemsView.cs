using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemsView : MonoBehaviour
{
    public Text AppleCountText;
    
    void Start()
    {
        AppleCountText.text = ItemsCountHolder.appleSaved.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(ItemsCountHolder.needUIUpdate)
        {
            UpdateUIAllpeText();
        }
    }

    public void UpdateUIAllpeText()
    {
        AppleCountText.text = ItemsCountHolder.appleSaved.ToString();
        ItemsCountHolder.needUIUpdate = false;
    }
}
