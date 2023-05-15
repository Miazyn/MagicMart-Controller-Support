using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    GameManager manager;

    [SerializeField] GameManager.GameState reqState;
    Animator myAnim;
    void Start()
    {
        manager = GameManager.Instance;
        myAnim = GetComponent<Animator>();
        myAnim.enabled = false;
        manager.onStateChangedCallback += StartBlinking;
    }

    void StartBlinking()
    {
        if(manager.curState == reqState)
        {
            myAnim.enabled = true;
        }
    }

    void OnDisable()
    {
        manager.onStateChangedCallback -= StartBlinking;
    }
}
