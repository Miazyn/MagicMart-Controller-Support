using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuUI : MonoBehaviour
{
    SO_Inventory inventory;
    Player player;

    public Transform itemsParent;
    ItemInventorySlotUI[] slots;

    public GameObject inventoryUI;

    private void Start()
    {
        player = Player.instance;
        player.onItemChangedCallback += UpdateUI;
        //player.onInventoryToggleCallback += InventoryToggle;
        inventory = player.inventory;

        inventoryUI.SetActive(false);
        slots = itemsParent.GetComponentsInChildren<ItemInventorySlotUI>();


    }
    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.inventoryItems.Count)
            {
                slots[i].AddItem(inventory.inventoryItems[i].item, inventory.inventoryItems[i].amount);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }

    }
    private void InventoryToggle()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    private void OnDisable()
    {
        player.onItemChangedCallback -= UpdateUI;
        player.onInventoryToggleCallback -= InventoryToggle;
    }
}
