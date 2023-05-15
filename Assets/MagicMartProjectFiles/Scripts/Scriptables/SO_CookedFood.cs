using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Cooking/Cooked Food")]
public class SO_CookedFood : ScriptableObject
{
    public SO_Recipe Recipe;
    public string RecipeName;
    public Quality quality;
    public enum Quality
    {
        normal,
        good,
        perfect
    }

    void Awake()
    {
        RecipeName = Recipe.recipeName;    
    }
}
