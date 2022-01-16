using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapControlMovementType : MonoBehaviour
{
    [SerializeField] private GameObject GameObject;
    [SerializeField] private GameObject GameObject1;
    [SerializeField] private GameObject GameObject2;
    [SerializeField] private GameObject GameObject3;
    private bool currentstate;
    public void SwapOnClick()
    {
        GameObject.SetActive(!currentstate);
        GameObject1.SetActive(!currentstate);
        GameObject2.SetActive(!currentstate);
        GameObject3.SetActive(!currentstate);
        PlayerInputs.controltype = currentstate;

        currentstate = !currentstate;
    }

}
