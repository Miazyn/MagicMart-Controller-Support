using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteObject : MonoBehaviour
{
    [SerializeField] float toleranceNormalHit = 0.5f;
    [SerializeField] float toleranceGoodHit = 0.2f;
    [SerializeField] float tolerancePerfectHit = 0.1f;
    bool CanBePressed;

    public enum KeyToPress
    {
        left,
        right,
        up,
        down
    }

    [SerializeField] KeyToPress keyToPress;
    bool HasHitNote;
    [SerializeField] float keyXValue;

    [Header("Game SFX")]
    [SerializeField] GameObject hitEffect, goodHitEffect, perfectHitEffect, missEffect;

    [SerializeField] float beatTempo = 120f;

    Player player;
    InputControl inputActions;

    private void Start()
    {
        player = Player.instance;
        inputActions = player.controls;

        beatTempo /= 60f;

        inputActions.Player.ArrowDown.performed += ArrowDown_performed;
        inputActions.Player.ArrowUp.performed += ArrowUp_performed;
        inputActions.Player.ArrowLeft.performed += ArrowLeft_performed;
        inputActions.Player.ArrowRight.performed += ArrowRight_performed;

    }

    private void OnDisable()
    {
        inputActions.Player.ArrowDown.performed -= ArrowDown_performed;
        inputActions.Player.ArrowUp.performed -= ArrowUp_performed;
        inputActions.Player.ArrowLeft.performed -= ArrowLeft_performed;
        inputActions.Player.ArrowRight.performed -= ArrowRight_performed;
    }

    private void ArrowRight_performed(InputAction.CallbackContext obj)
    {
        if(KeyToPress.right == keyToPress)
        {
            IsInputValid();
        }
    }

    private void ArrowLeft_performed(InputAction.CallbackContext obj)
    {
        if (KeyToPress.left == keyToPress)
        {
            IsInputValid();
        }
    }

    private void ArrowUp_performed(InputAction.CallbackContext obj)
    {
        if (KeyToPress.up == keyToPress)
        {
            IsInputValid();
        }
    }

    private void ArrowDown_performed(InputAction.CallbackContext obj)
    {
        if (KeyToPress.down == keyToPress)
        {
            IsInputValid();
        }
    }

    private void IsInputValid()
    {
        Debug.Log("Key triggered");
        if (CanBePressed)
        {
            Debug.Log("Press went through");
            HasHitNote = true;
            HitCheck();
        }
    }

    void Update()
    {
        Debug.Log($"Can be pressed: {CanBePressed}");
        if (gameObject.activeSelf)
        {
            transform.position -= new Vector3(beatTempo * Time.deltaTime, 0f, 0f);
        }
        
    }

    private void HitCheck()
    {
        if (transform.position.x < (keyXValue + tolerancePerfectHit) && transform.position.x > (keyXValue - tolerancePerfectHit))
        {
            //Perfect
            Instantiate(perfectHitEffect, transform.position, perfectHitEffect.transform.rotation);
            RhythmGameManager.instance.PerfectHit();
            gameObject.SetActive(false);
        }
        else if (transform.position.x < (keyXValue + toleranceGoodHit) && transform.position.x > (keyXValue - toleranceGoodHit))
        {
            //Good
            Instantiate(goodHitEffect, transform.position, goodHitEffect.transform.rotation);
            RhythmGameManager.instance.GoodHit();
            gameObject.SetActive(false);

        }
        else if (transform.position.x < (keyXValue + toleranceNormalHit) && transform.position.x > (keyXValue - toleranceNormalHit))
        {
            //Normal
            Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
            RhythmGameManager.instance.NormalHit();
            gameObject.SetActive(false);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Activator")
        {
            CanBePressed = true;
        }
        if(collision.tag == "Despawner")
        {
            RhythmGameManager.instance.NoteMiss();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Activator")
        {
            CanBePressed = false;
            if (!HasHitNote)
            {
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
                
            }
        }
    }
}
