using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextOverflowCheck : MonoBehaviour
{
    string a = "This will be overflown";
    int maxChar = 8;

    int curCheck;
    List<int> spaces = new List<int>();

    public TextMeshProUGUI tester;

    void CheckSpace()
    {
        int counter = 0;
        for (int i = 1; i <= a.Length / maxChar; i++)
        {
            curCheck = i * maxChar;
            Debug.Log(curCheck);
            bool addedSpace = false;
            for (int j = 0; j < curCheck; j++)
            {
                if(a[j] == ' ')
                {
                    if (addedSpace)
                    {
                        spaces[counter] = j;
                    }
                    else
                    {
                        spaces.Add(j);
                        addedSpace = true;
                    }
                }
            }
            counter++;
        }
        string newString = "";
        for(int y = 0; y < a.Length; y++)
        {
            bool addSpace = false;
            for(int x = 0; x < spaces.Count; x++)
            {
                if(y == spaces[x])
                {
                    addSpace = true;
                }
            }
            if (addSpace)
            {
                if (a[y] != ' ')
                {
                    newString += "\n" + a[y].ToString();

                }
                else
                {
                    newString += "\n";
                }
            }
            else
            {
                newString += a[y].ToString();
            }
        }
    }
}
