using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CookIngredient : MonoBehaviour, IDropHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] float delayedCheck = 2f;
    public RectTransform rect;
    //public Vector2 originalPosition;

    public Transform AfterOnTheke;

    public SO_Ingredient ingredient;

    public Canvas canvas;
    public CanvasGroup canvasGroup;
    public float onDragAlpha;

    public bool IsOnTheke = false;
    public bool HasBeenOnTheke = false;
    public bool IsCurrentlyDragged;

    public float HoverSizeUp = 1.5f;
    Vector3 ogScale;

    GameObject prefab;

    bool IsDragged = false;


    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        //originalPosition = rect.anchoredPosition;
       
    }
    private void Start()
    {
        Debug.Log($"Spawned in: {transform.position}");
        GetComponent<Image>().sprite = ingredient.ingredientSprite;
        if (!canvas)
        {
            Debug.LogWarning("NO CANVAS FOR SCALE DEFINED");
        }
        ogScale = rect.localScale;
        StartCoroutine(CheckIfDragged());

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin dragged");

        IsCurrentlyDragged = true;
        canvasGroup.alpha = onDragAlpha;
        canvasGroup.blocksRaycasts = false;
        
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");

        IsOnTheke = false;
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
        IsDragged = true;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End dragged");


        IsCurrentlyDragged = false;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (!IsOnTheke && HasBeenOnTheke) 
        {
            Debug.Log("I am not on a theke: " + ingredient.ingredientName);
            GameObject.FindObjectOfType<CookingPot>().RemoveItem(ingredient);

            Destroy(gameObject);
        }
        else if(!IsOnTheke)
        {
            Destroy(gameObject);
        }
    }


    IEnumerator CheckIfDragged()
    {
        yield return new WaitForSeconds(delayedCheck);
        if (!IsDragged)
        {

            Destroy(gameObject);
        }
    }
    public void SizeUp()
    {
        rect.localScale *= HoverSizeUp;
    }
    public void SizeDown()
    {
        rect.localScale = ogScale;
    }

    public void OnDrop(PointerEventData eventData)
    {
        
    }
}
