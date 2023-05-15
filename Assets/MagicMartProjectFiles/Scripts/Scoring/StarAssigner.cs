using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAssigner : MonoBehaviour
{
    [SerializeField] GameObject star1;
    [SerializeField] GameObject star2;
    [SerializeField] GameObject star3;

    GameManager manager;
    float score;

    bool IsJudging = false;
    [SerializeField] SceneMana sceneMana;
    void Start()
    {
        manager = GameManager.Instance;

        manager.ChangeGameState(GameManager.GameState.EvaluationState);
        score = manager.OverallScore;

        StartCoroutine(Judge());
    }

    void Update()
    {
        if (IsJudging)
        {
            if (Input.GetMouseButtonDown(0))
            {
                sceneMana.LoadNextScene("MainStore");
            }
        }
        
    }

    IEnumerator Judge()
    {
        yield return new WaitForSeconds(1f);
        if(score > 80)
        {
            star1.GetComponent<Animator>().enabled = true;
            star2.GetComponent<Animator>().enabled = true;
            star3.GetComponent<Animator>().enabled = true;
        }
        else if(score > 50)
        {
            star1.GetComponent<Animator>().enabled = true;
            star2.GetComponent<Animator>().enabled = true;
        }
        else if(score > 20)
        {
            star1.GetComponent<Animator>().enabled = true;
        }
        IsJudging = true;
    }

}
