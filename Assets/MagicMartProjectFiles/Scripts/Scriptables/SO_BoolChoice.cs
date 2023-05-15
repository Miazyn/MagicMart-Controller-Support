using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoolChoice", menuName = "Scriptables/BoolChoice")]
public class SO_BoolChoice : SO_Choice
{
    public enum Choices
    {
        Yes,
        No
    }
    public Choices choice;

    public bool HasAgreed()
    {
        if(choice == Choices.Yes)
        {
            return true;
        }
        return false;
    }
}
