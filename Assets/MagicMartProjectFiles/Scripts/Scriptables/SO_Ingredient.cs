using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Cooking/Ingredient")]
public class SO_Ingredient : ScriptableObject
{
    public string ingredientName;
    public Sprite ingredientSprite;
    public int health;
    public int mana;
    public int power;

    public int BuyPrice;

    [TextArea(5, 10)]
    public string ingredientDescription;

    public bool CompareIngredient(SO_Ingredient compareTo)
    {
        if (this.ingredientName == compareTo.ingredientName)
        {
            if(this.ingredientSprite == compareTo.ingredientSprite)
            {
                return true;
            }
        }
        return false;
    }

    public (int _health, int _mana, int _power) GetIngredientStats()
    {
        return (health, mana, power);
    }
}
