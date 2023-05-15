using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateCustomers : Editor
{
    [MenuItem("Utilities/GenerateCustomers")]
    public static void GeneratingCustomers()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("GenericSprites");
        SO_Voice[] voices = Resources.LoadAll<SO_Voice>("Generic/Voices");
        SO_Recipe[] allRecipes = Resources.LoadAll<SO_Recipe>("Recipes");

        SO_Dialog beforeDialog = Resources.Load<SO_Dialog>("Generic/GenericQuests/0101");
        SO_Dialog afterDialog = Resources.Load<SO_Dialog>("Generic/GenericQuests/0102");

        foreach(SO_Recipe recipe1 in allRecipes)
        {
            SO_QuestNPC tester = ScriptableObject.CreateInstance<SO_QuestNPC>();

            tester.QuestDialogAfterCompletion = new SO_Dialog[1];
            tester.QuestDialogBeforeCompletion = new SO_Dialog[1];
            tester.ReqRecipe = recipe1;

            AssetDatabase.CreateAsset(tester, $"Assets/MyProject/Scriptables/Resources/GenericNPC/{tester.ReqRecipe.recipeName + "Quest"}.asset");

        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        SO_QuestNPC[] quests = Resources.LoadAll<SO_QuestNPC>("GenericNPC");
        string name = "Longneck Cat";
        int counter = 0;
        foreach(SO_QuestNPC npc in quests) { 

            SO_NPC tester = ScriptableObject.CreateInstance<SO_NPC>();

            tester.voice = voices[Random.Range(0, voices.Length)];

            tester.quests = new SO_QuestNPC[1];

            tester.quests[0] = npc;

            //AFTER GET
            tester.quests[0].QuestDialogAfterCompletion[0].spriteForCharacterDisplay = new List<Sprite>();
            tester.quests[0].QuestDialogAfterCompletion[0].keyForCharacterDisplay = new List<int>();

            tester.quests[0].QuestDialogAfterCompletion[0].nameOfSpeaker = new List<string>();
            tester.quests[0].QuestDialogAfterCompletion[0].keyForName = new List<int>();

            //SET
            tester.quests[0].QuestDialogAfterCompletion[0].spriteForCharacterDisplay.Add(sprites[Random.Range(0, sprites.Length)]);
            tester.quests[0].QuestDialogAfterCompletion[0].keyForCharacterDisplay.Add(0);

            tester.quests[0].QuestDialogAfterCompletion[0].nameOfSpeaker.Add(name);
            tester.quests[0].QuestDialogAfterCompletion[0].keyForName.Add(0);


            //BEFORE GET
            tester.quests[0].QuestDialogBeforeCompletion[0].spriteForCharacterDisplay = new List<Sprite>();
            tester.quests[0].QuestDialogBeforeCompletion[0].keyForCharacterDisplay = new List<int>();

            tester.quests[0].QuestDialogBeforeCompletion[0].nameOfSpeaker = new List<string>();
            tester.quests[0].QuestDialogBeforeCompletion[0].keyForName = new List<int>();

            //SET
            tester.quests[0].QuestDialogBeforeCompletion[0].spriteForCharacterDisplay.Add(sprites[Random.Range(0, sprites.Length)]);
            tester.quests[0].QuestDialogBeforeCompletion[0].keyForCharacterDisplay.Add(0);

            tester.quests[0].QuestDialogBeforeCompletion[0].nameOfSpeaker.Add(name);
            tester.quests[0].QuestDialogBeforeCompletion[0].keyForName.Add(0);

            AssetDatabase.CreateAsset(tester, $"Assets/MyProject/Scriptables/Resources/GenericNPC/{tester.name + counter}.asset");
            counter++;
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
