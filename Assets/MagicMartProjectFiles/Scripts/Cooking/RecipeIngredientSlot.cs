using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeIngredientSlot : MonoBehaviour
{
    public SO_Ingredient heldIngredient;
    [SerializeField] GameObject itemImageSlot;
    Image image;

    private void Start()
    {
        image = itemImageSlot.GetComponent<Image>();
        image.sprite = heldIngredient.ingredientSprite;
    }
}
