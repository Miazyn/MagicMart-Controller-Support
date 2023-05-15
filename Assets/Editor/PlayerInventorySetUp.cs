using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerInventorySetUp : MonoBehaviour
{
    private static string resourcePath = "CookingIngredients";
    [MenuItem("Utilities/SetUpPlayerInventory")]
    public static void SetUpPlayerInventory()
    {
        var invItems = Resources.LoadAll(resourcePath, typeof(SO_Ingredient));

        SO_Inventory playerInventory = ScriptableObject.CreateInstance<SO_Inventory>();
        playerInventory.inventoryName = "Player Inventory";
        foreach(var item in invItems)
        {
            playerInventory.AddItem((SO_Ingredient)item, 5);
        }
        AssetDatabase.CreateAsset(playerInventory, $"Assets/MyProject/Scriptables/Resources/Inventory/{playerInventory.inventoryName}.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

}
