using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmEffects : MonoBehaviour
{
    public float lifetime = 1f;

    private void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
