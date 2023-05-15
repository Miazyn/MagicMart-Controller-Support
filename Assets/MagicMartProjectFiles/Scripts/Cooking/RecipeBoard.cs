using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecipeBoard : MonoBehaviour
{
    GameManager manager;
    public SO_Recipe recipe;

    [SerializeField] GameObject recipeBoard;
    [SerializeField] public GameObject recipeItemPrefab;
    GameObject[] allSlots;

    [SerializeField] TextMeshProUGUI recipeName;
    void Start()
    {
        manager = GameManager.Instance;
        if (manager.Customers.Length > 0)
        {
            recipe = manager.Customers[manager.CustomerCounter].quests[0].ReqRecipe;
        }

        CreateRecipeBoard();
    }

    public void CreateRecipeBoard()
    {
        if(allSlots != null)
        {
            foreach(var item in allSlots)
            {
                Destroy(item.gameObject);
            }
        }
        allSlots = new GameObject[recipe.ingredients.Length];
        int count = recipe.ingredients.Length;
        for(int i = 0; i < count; i++)
        {
            GameObject curItem = Instantiate(recipeItemPrefab, recipeBoard.transform);
            curItem.GetComponent<RecipeIngredientSlot>().heldIngredient = recipe.ingredients[i];
            allSlots[i] = curItem;
        }
        recipeName.SetText(recipe.recipeName.ToUpper());
    }
}
