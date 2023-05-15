using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesLoader : MonoBehaviour
{
    void Start()
    {
        var textFile = Resources.Load<TextAsset>("Shop/Test");
        Debug.Log(textFile.text);
    }

}
