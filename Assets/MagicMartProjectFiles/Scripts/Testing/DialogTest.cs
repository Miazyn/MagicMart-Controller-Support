using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTest : MonoBehaviour
{
    public SO_Dialog myDialog;


    private void Start()
    {
        //Functional
        Debug.Log(myDialog.lines[0].Replace("$playerName", "John"));
    }
}
