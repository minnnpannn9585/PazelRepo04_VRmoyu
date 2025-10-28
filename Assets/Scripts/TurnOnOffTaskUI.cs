using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnOnOffTaskUI : MonoBehaviour
{
    public InputActionProperty buttonAction;
    public GameObject handUI;

    void Start()
    {
        buttonAction.action.performed += ctx =>
        {
            handUI.SetActive(!handUI.activeSelf);
        };
    }
}
