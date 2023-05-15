using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    GameManager manager;
    SO_Recipe recipe;

    [Header("UI")]
    [SerializeField] Image recipeImage;
    [SerializeField] TextMeshProUGUI recipeName;

    void Start()
    {
        manager = GameManager.Instance;
        recipe = manager.CurrentRecipe;

        recipeImage.sprite = recipe.recipeSprite;
        recipeName.SetText(recipe.recipeName);
    }
}
