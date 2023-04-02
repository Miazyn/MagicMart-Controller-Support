using System.Collections;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    InputControl controls;

    private void Awake()
    {
        controls = new InputControl();

        controls.Player.DialogContinue.performed += tmp => DialogContinue_performed();
        controls.Player.Cooking.performed += cook => Cooking_performed();
        controls.Player.Shop.performed += shop => Shop_performed();

        controls.Enable();
    }

    private void Shop_performed()
    {
        Debug.Log("Shoppping");
    }

    private void Cooking_performed()
    {
        Debug.Log("Cooking");
    }

    private void DialogContinue_performed()
    {
        Debug.Log("Dialog clicked (Controller or Mousee)");
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
