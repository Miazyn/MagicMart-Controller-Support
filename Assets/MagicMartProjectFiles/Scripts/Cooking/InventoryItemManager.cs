using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemManager : MonoBehaviour
{
    public GameObject ItemSlotPrefab;
    public Transform ParentTransform;
    public Transform PrefabParent;
    public Transform OnThekeParent;
    public Canvas Canvas;

    private void Awake()
    {
        var loadItems = Resources.LoadAll("CookingIngredients", typeof(SO_Ingredient));
        foreach(var item in loadItems)
        {
            IngredientSpawner _newIngredient = Instantiate(ItemSlotPrefab, ParentTransform).GetComponent<IngredientSpawner>();

            _newIngredient.ingredientToSpawn = (SO_Ingredient)item;

            _newIngredient.prefabParent = PrefabParent;
            _newIngredient.canvas = this.Canvas;
            _newIngredient.GetComponent<RectTransform>().anchoredPosition = ParentTransform.GetComponent<RectTransform>().anchoredPosition;
            _newIngredient.AfterThekeParent = OnThekeParent;
        }
    }
}
