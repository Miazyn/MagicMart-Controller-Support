using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class IngredientSpawnerVerTwo : MonoBehaviour, IInitializePotentialDragHandler, IDragHandler, IPointerDownHandler
{
    [SerializeField] GameObject ingredientPrefab;
    [SerializeField] SO_Ingredient ingredientToSpawn;
    [SerializeField] Transform prefabParent;
    [SerializeField] Canvas canvas;

    CookIngredient ingredientScript;
    GameObject instantiatedObject;

    Vector2 currentpos;

    void Awake()
    {
        GetComponent<Image>().sprite = ingredientToSpawn.ingredientSprite;
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        instantiatedObject = Instantiate(ingredientPrefab, prefabParent);
        instantiatedObject.transform.position = transform.position;
        ingredientScript = instantiatedObject.GetComponent<CookIngredient>();
        ingredientScript.ingredient = ingredientToSpawn;
        ingredientScript.canvas = canvas;

        currentpos = GetComponent<RectTransform>().anchoredPosition;
        eventData.pointerDrag = instantiatedObject;

    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        currentpos = ingredientPrefab.GetComponent<RectTransform>().anchoredPosition;
    }
}
