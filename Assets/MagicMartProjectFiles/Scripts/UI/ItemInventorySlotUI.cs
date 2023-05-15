using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInventorySlotUI : MonoBehaviour
{
    SO_Ingredient item;
    public Image icon;
    public TextMeshProUGUI itemAmount;
    public void AddItem(SO_Ingredient newItem, int amount)
    {
        item = newItem;
        icon.sprite = item.ingredientSprite;
        icon.enabled = true;

        itemAmount.SetText(amount.ToString());
        itemAmount.enabled = true;
    }

    public void ClearSlot()
    {
        itemAmount.SetText("0");
    }

    public SO_Ingredient GetItem()
    {
        return item;
    }
}
