using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugCookingButtons : MonoBehaviour
{
    [SerializeField] CookingPot cookingPot;
    [SerializeField] RecipeBoard recipe;
    SO_Recipe[] recipes;

    [SerializeField] TextMeshProUGUI score;
    private void Awake()
    {
        recipes = Resources.LoadAll<SO_Recipe>("Recipes");
    }

    public void NextRecipe()
    {
        recipe.recipe = recipes[Random.Range(0, recipes.Length - 1)];
        cookingPot.currentRecipe = recipe.recipe;
        cookingPot.DebugUpdateRecipe();
        recipe.CreateRecipeBoard();
    }
}
