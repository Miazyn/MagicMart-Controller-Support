using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeTransitions : MonoBehaviour
{
    [SerializeField] GameObject clickToContinue;
    [SerializeField] SceneMana sceneMana;
    GameManager manager;

    public Animator Pot;
    public Animator Arrow;
    public Animator Star;

    [SerializeField] Player player;
    InputControl inputActions;
    void Start()
    {
        player = Player.instance;
        manager = GameManager.Instance;

        inputActions = player.controls;

        StartCoroutine(LateStart());
        manager.ResetMusic();
        //DISPLAY CORRECT ANIM
        if(manager.curState == GameManager.GameState.IdleState)
        {
            //Into Cooking
            Pot.enabled = true;
        }
        if(manager.curState == GameManager.GameState.CookingState)
        {
            //Into Rhythm
            Arrow.enabled = true;
        }
        if(manager.curState == GameManager.GameState.MiniRhythmGameState)
        {
            //Into Evaluation
            Star.enabled = true;
        }


        inputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Performed");
        if (manager.curState == GameManager.GameState.IdleState)
        {
            sceneMana.LoadNextScene("CookingArea");
        }
        if (manager.curState == GameManager.GameState.CookingState)
        {
            //Into Rhythm
            
                sceneMana.LoadNextScene("RhythmMiniGame");
            
        }
        if (manager.curState == GameManager.GameState.MiniRhythmGameState)
        {
            //Into Evaluation
            
            sceneMana.LoadNextScene("Score");
            
        }
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(2f);
        clickToContinue.SetActive(true);
    }

    private void OnDisable()
    {
        inputActions.Player.Interact.performed -= Interact_performed;
    }
}
