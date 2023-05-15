using UnityEngine;
using UnityEditor;
using System.IO;

public class CSVToSO
{
    private static string CSVItems = "/Editor/CSV/ItemsTabelle.csv";
    private static string CSVRezepte = "/Editor/CSV/Rezepte.csv";
    private static string resourcePath = "Ingredients/";
    [MenuItem("Utilities/GenerateItems")]
    public static void GenerateSO()
    {
        Debug.Log("Generate Items");
        string[] allLines = File.ReadAllLines(Application.dataPath + CSVItems);

        foreach (string s in allLines)
        {
            string[] splitData = s.Split(',');

            SO_Ingredient tester = ScriptableObject.CreateInstance<SO_Ingredient>();
            tester.ingredientName = splitData[0];
            tester.health = int.Parse(splitData[1]);
            tester.mana = int.Parse(splitData[2]);
            tester.power = int.Parse(splitData[3]);

            tester.BuyPrice = int.Parse(splitData[4]);
            tester.ingredientDescription = splitData[6];


            var testerSprite = Resources.Load<Sprite>(resourcePath + tester.ingredientName);
            if (testerSprite != null) 
            {
                tester.ingredientSprite = testerSprite;
            }
            else
            {
                Debug.Log("no Sprite found at: " + resourcePath + tester.ingredientName);
            }

            //Knowledge of unity of all data //Path has alrdy to be exist
            AssetDatabase.CreateAsset(tester, $"Assets/MyProject/Scriptables/Resources/CookingIngredients/{tester.ingredientName}.asset");
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("Utilities/GenerateRecipes")]
    public static void GenerateRecipes()
    {
        Debug.Log("Generate Recipes");
        string[] allLines = File.ReadAllLines(Application.dataPath + CSVRezepte);

        foreach (string s in allLines)
        {
            string[] splitData = s.Split(',');

            int ingredientCounter = 0;

            SO_Recipe tester = ScriptableObject.CreateInstance<SO_Recipe>();
            tester.recipeName = splitData[0];

            tester.perfectSellPrice = int.Parse(splitData[5]);
            tester.goodSellPrice = int.Parse(splitData[6]);
            tester.normalSellPrice = int.Parse(splitData[7]);
            tester.terribleSellPrice = int.Parse(splitData[8]);

            if(splitData[1] != "")
            {
                ingredientCounter++;
                if(splitData[2] != "")
                {
                    ingredientCounter++;
                    if(splitData[3] != "")
                    {
                        ingredientCounter++;
                        if(splitData[4] != "")
                        {
                            ingredientCounter++;
                        }
                    }
                }
            }

            tester.ingredients = new SO_Ingredient[ingredientCounter];
            
            for(int i = 0; i < tester.ingredients.Length; i++)
            {
                tester.ingredients[i] = Resources.Load<SO_Ingredient>("CookingIngredients/" + splitData[i + 1]);
            }


            tester.health = int.Parse(splitData[9]);
            tester.mana = int.Parse(splitData[10]);
            tester.power = int.Parse(splitData[11]);
            
            var testerSprite = Resources.Load<Sprite>("Recipes/" + tester.recipeName);
            if (testerSprite != null)
            {
                tester.recipeSprite = testerSprite;
            }
            else
            {
                Debug.Log("no Sprite found at: " + "Recipes/" + tester.recipeName);
            }
            //Knowledge of unity of all data //Path has alrdy to be exist
            AssetDatabase.CreateAsset(tester, $"Assets/MyProject/Scriptables/Resources/Recipes/{tester.recipeName}.asset");

        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
