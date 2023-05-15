using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Create new NPC quest")]

public class SO_QuestNPC : SO_Quest
{
    public SO_Dialog[] QuestDialogBeforeCompletion;
    public SO_Dialog[] QuestDialogAfterCompletion;
}
