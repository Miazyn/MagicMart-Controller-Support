using UnityEngine;
using UnityEditor;

public class CustomMenu : Editor
{
    [MenuItem("Custom/Button", false, 10)]
    static void CreateCustomButton()
    {
        GameObject obj = new GameObject("CustomButton");
        Canvas myCanvas = FindObjectOfType<Canvas>();

        obj.transform.SetParent(myCanvas.transform, false);

        obj.AddComponent<CustomButton>();
    }
}
