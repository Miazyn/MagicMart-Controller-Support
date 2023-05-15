using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    InputControl controls;

    private void Awake()
    {
        controls = new InputControl();

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

    private void DialogContinue_performed(InputAction.CallbackContext ctx)
    {
        Debug.Log("Dialog clicked (Controller or Mousee)" + " " + ctx.action.actionMap.name);
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
