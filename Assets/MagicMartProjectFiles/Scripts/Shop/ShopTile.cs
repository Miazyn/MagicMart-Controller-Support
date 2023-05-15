using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopTile : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float timeb4WindowOpens = 0.5f;

    public SO_Ingredient Ingredient;
    [SerializeField] Image ItemImage;
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI Price;
    [SerializeField] TextMeshProUGUI PlayerStock;
    public AudioSource buttonClick;
    public AudioSource buySound;

    Player CurrentPlayer;
    [SerializeField]    SO_Inventory PlayerInventory;


    public void OnPointerClick(PointerEventData eventData)
    {
        buttonClick.Play();
        if (CurrentPlayer.CanBuy(Ingredient.BuyPrice))
        {
            CurrentPlayer.SetMoneyAmount(-Ingredient.BuyPrice);
            buySound.Play();
            CurrentPlayer.inventory.AddItem(Ingredient, 1);
            SetUpTile();
        }
    }

    public void SetUpTile()
    {
        CurrentPlayer = Player.instance;
        PlayerInventory = CurrentPlayer.inventory;

        ItemImage.sprite = Ingredient.ingredientSprite;
        Name.SetText(Ingredient.ingredientName);
        Price.SetText(Ingredient.BuyPrice.ToString());
        int playerStockOfItem = 0;

        if (CurrentPlayer.inventory.inventoryItems.Count > 0)
        {
            playerStockOfItem =
                PlayerInventory.inventoryItems
                [PlayerInventory.FindItemInList(Ingredient).Item2].GetAmount();

            PlayerStock.SetText("Owned: " + playerStockOfItem.ToString());
        }
        else
        {
            Debug.LogWarning("NO Items in player");
            PlayerStock.SetText("Owned: " + 0);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(TimerB4ShowDesc());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        DescriptionBoxOnHover.OnMouseLoseFocus();
    }

    void ShowMessage()
    {
        DescriptionBoxOnHover.OnMouseOver(Ingredient, Input.mousePosition);
    }

    IEnumerator TimerB4ShowDesc()
    {
        yield return new WaitForSeconds(timeb4WindowOpens);
        ShowMessage();
    }
}
