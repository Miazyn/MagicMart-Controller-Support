using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AlignNotes : MonoBehaviour
{

    [MenuItem("Utilities/AlignRhythmNotes")]
    public static void AlignmentOfNotes()
    {

        GameObject[] myNotes = GameObject.FindGameObjectsWithTag("Note");

        for (int i = 0; i < myNotes.Length; i++)
        {
            myNotes[i].transform.position = new Vector3(myNotes[i].transform.position.x, 4f, 0f);
        }
    } 
}
