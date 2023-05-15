using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class DescriptionBoxOnHover : MonoBehaviour
{
    [Header("All Text")]
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI manaTxt;
    public TextMeshProUGUI powerTxt;
    public TextMeshProUGUI healthTxt;
    public Image itemImage;

    public RectTransform descriptionWindows;

    public static Action<SO_Ingredient, Vector2> OnMouseOver;
    public static Action OnMouseLoseFocus;

    void OnEnable()
    {
        OnMouseOver += ShowTip;
        OnMouseLoseFocus += HideTip;
    }
    void OnDisable()
    {
        OnMouseOver -= ShowTip;
        OnMouseLoseFocus -= HideTip;
    }

    void Start()
    {
        HideTip();   
    }

    void ShowTip(SO_Ingredient _ingredientToShow, Vector2 mousePos)
    {
        descriptionText.text = _ingredientToShow.ingredientDescription;
        manaTxt.text = _ingredientToShow.mana.ToString();
        healthTxt.text = _ingredientToShow.health.ToString();
        powerTxt.text = _ingredientToShow.power.ToString();

        itemImage.sprite = _ingredientToShow.ingredientSprite;

        descriptionWindows.gameObject.SetActive(true);
        descriptionWindows.transform.position = new Vector2(mousePos.x - descriptionWindows.sizeDelta.x * 0.35f, mousePos.y);
    }

    void HideTip()
    {
        descriptionText.text = default;
        descriptionWindows.gameObject.SetActive(false);
    }

}
