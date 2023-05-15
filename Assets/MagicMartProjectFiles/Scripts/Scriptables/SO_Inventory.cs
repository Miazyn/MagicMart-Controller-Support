using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "SO/Inventory")]
public class SO_Inventory : ScriptableObject
{
    public string inventoryName;

    public List<InventorySlot> inventoryItems = new List<InventorySlot>();
    public bool AddItem(SO_Ingredient _item, int _amount)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].item == _item)
            {
                inventoryItems[i].AddAmount(_amount);
                return true;
            }
        }
        inventoryItems.Add(new InventorySlot(_item, _amount));
        return true;
    }

    public (bool, int) FindItemInList(SO_Ingredient _ingredient)
    {
        for(int i = 0; i < inventoryItems.Count; i++)
        {
            if (_ingredient.CompareIngredient(inventoryItems[i].item))
            {
                return (true, i);
            }
        }
        
        return (false, -1);
    }
}

[System.Serializable]
public class InventorySlot
{
    public SO_Ingredient item;
    public int amount;
    public InventorySlot (SO_Ingredient _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

    public int GetAmount()
    {
        return amount;
    }
}
