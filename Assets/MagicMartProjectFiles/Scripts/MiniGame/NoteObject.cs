using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    [SerializeField] float toleranceNormalHit = 0.5f;
    [SerializeField] float toleranceGoodHit = 0.2f;
    [SerializeField] float tolerancePerfectHit = 0.1f;
    bool CanBePressed;
    [SerializeField] KeyCode keyToPress;
    bool HasHitNote;
    [SerializeField] float keyXValue;

    [Header("Game SFX")]
    [SerializeField] GameObject hitEffect, goodHitEffect, perfectHitEffect, missEffect;

    [SerializeField] float beatTempo = 120f;
    private void Start()
    {
        beatTempo /= 60f;

    }
    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.position -= new Vector3(beatTempo * Time.deltaTime, 0f, 0f);
        }
        if (Input.GetKeyDown(keyToPress))
        {
            if (CanBePressed)
            {
                HasHitNote = true;
                HitCheck();

            }
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
