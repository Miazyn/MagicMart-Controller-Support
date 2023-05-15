using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Cooking/Recipe")]
public class SO_Recipe : ScriptableObject
{
    public string recipeName;
    [TextArea(5,10)]
    public string description;
    public SO_Ingredient[] ingredients;

    public Sprite recipeSprite;

    public int health;
    public int mana;
    public int power;

    public int terribleSellPrice;
    public int normalSellPrice;
    public int goodSellPrice;
    public int perfectSellPrice;
    public (int healthReq, int manaReq, int powerReq) CheckRecipeRequirements() 
    {
        return (this.health, this.mana, this.power);
    }

    public bool IsValidIngredient(SO_Ingredient i)
    {
        foreach(var item in ingredients)
        {
            if (item.CompareIngredient(i))
            {
                return true;
            }
        }
        return false;
    }

    public bool ContainsRecipe(SO_Ingredient[] iArray)
    {
        foreach(var item in ingredients)
        {
            bool found = false;
            foreach(var i in iArray)
            {
                if (item.CompareIngredient(i))
                {
                    found = true;
                }
            }
            if (!found)
            {
                return false;
            }
        }
        return true;
    }
    public bool ContainsRecipe(List<SO_Ingredient> iList)
    {
        foreach (var item in ingredients)
        {
            bool found = false;
            foreach (var i in iList)
            {
                if (item.CompareIngredient(i))
                {
                    found = true;
                }
            }
            if (!found)
            {
                return false;
            }
        }
        return true;
    }
}
