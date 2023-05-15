using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Updater : MonoBehaviour
{
    [SerializeField] CookingPot cookingPot;
    SO_Recipe recipe;

    [SerializeField] TextMeshProUGUI mana; 
    [SerializeField] TextMeshProUGUI health; 
    [SerializeField] TextMeshProUGUI power; 
    private void Awake()
    {
        cookingPot.onIngredientsChangedCallback += UpdateStatsUI;
    }

    void UpdateStatsUI()
    {
        recipe = cookingPot.currentRecipe;
        mana.SetText("Mana: " + cookingPot.curMana + "/" + recipe.mana);
        health.SetText("Health: " + cookingPot.curHealth + "/" + recipe.health);
        power.SetText("Power: " + cookingPot.curPower + "/" + recipe.power);
    }

    void OnDisable()
    {
        cookingPot.onIngredientsChangedCallback -= UpdateStatsUI;
    }
}
