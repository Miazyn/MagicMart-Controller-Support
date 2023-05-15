using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Create new quest")]
public class SO_Quest : ScriptableObject
{
    public string QuestName;
    public int QuestID;
    public SO_Recipe ReqRecipe;
    public SO_CookedFood CookedFood;

}
