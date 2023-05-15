using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] int maxCharPerLine = 43;
    [SerializeField] float typingSpeedInSeconds = 0.03f;

    [Header("Elements for Dialog")]
    [SerializeField] GameObject textBoxObject;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject npc;
    [SerializeField] Image npcSprite;
    [SerializeField] GameObject InDialogEffect;

    [Header("Disabling Buttons etc")]
    [SerializeField] GameObject cookButton;
    [SerializeField] GameObject shopButton;
    [SerializeField] GameObject playerMoney;


    Sprite savedSprite;
    string savedName;

    Coroutine displayCoroutine;
    public bool typerRunning = false;
    AudioSource audioSource;
    AudioClip[] allTypeClips;

    bool IsNpcLine = false;

    Player player;
    [SerializeField] GameManager manager;

    int counter = 0;

    SO_Recipe recipe;

    SO_Dialog lastDialog;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = Player.instance;
        manager = GameManager.Instance;
        WarnCheck();

        if(manager.curState == GameManager.GameState.MiniRhythmGameState)
        {
            manager.ChangeGameState(GameManager.GameState.EvaluationState);
        }
        if (manager.curState == GameManager.GameState.EvaluationState)
        {
            manager.ChangeGameState(GameManager.GameState.AfterDialog);
        }
    }
    private void WarnCheck()
    {
        if (!textBoxObject)
        {
            Debug.LogWarning("No Default TextBox is defined.");
        }
        if (!nameText)
        {
            Debug.LogWarning("No Default NameBox is defined.");

        }
        if (!dialogueText)
        {
            Debug.LogWarning("No Default DialogueBox is defined.");
        }
        if (!npcSprite)
        {
            Debug.LogWarning("No Default SpriteBox is defined.");
        }
    }
    public void SetUpDialog(SO_Dialog _currentDialog, SO_NPC _dialogNpc, SO_Recipe _questRecipe)
    {
        allTypeClips = _dialogNpc.voice.voiceClips;
        recipe = _questRecipe;

        if(lastDialog == null)
        {
            lastDialog = _currentDialog;
        }
        else if(_currentDialog != lastDialog)
        {
            counter = 0;
            lastDialog = _currentDialog;
        }

        if (counter <= _currentDialog.lines.Count && !typerRunning)
        {
            TextReceived(_currentDialog);
            counter++;
        }
        else if (typerRunning)
        {
            DisplayCharacterSprite(_currentDialog);
            DisplayCharacterName(_currentDialog);

            StopTypeEffect(_currentDialog);
        }
    }
    void TextReceived(SO_Dialog dialogue)
    {
        npcSprite.GetComponent<Image>().sprite = DisplayCharacterSprite(dialogue);
        nameText.SetText(DisplayCharacterName(dialogue));

        if (!typerRunning)
        {
            if (dialogue != null || dialogue.lines[counter] != "")
            {
                //SET ACTIVE

                textBoxObject.SetActive(true);
                npc.SetActive(true);
                InDialogEffect.SetActive(true);

                cookButton.SetActive(false);
                shopButton.SetActive(false);
                playerMoney.SetActive(false);
            }
            if (counter > dialogue.lines.Count - 1 && dialogue.dialogueChoices.Count == 0)
            {
                EndDialog();
            }
            else
            {
                string line = CleanString(dialogue.lines[counter]);

                if (displayCoroutine != null)
                {
                    StopCoroutine(TypeEffect(line));
                }

                displayCoroutine = StartCoroutine(TypeEffect(line));
            }
        }

    }
    void EndDialog()
    {

        //SET INACTIVE
        textBoxObject.SetActive(false);
        InDialogEffect.SetActive(false);
        npc.SetActive(false);

        counter = 0;

        if (manager.curState == GameManager.GameState.DialogState)
        {
            manager.ChangeGameState(GameManager.GameState.IdleState);
        }
        if (manager.curState == GameManager.GameState.AfterDialog)
        {
            manager.CustomerCounter++;
            manager.ChangeGameState(GameManager.GameState.StartState);
        }
        cookButton.SetActive(true);
        shopButton.SetActive(true);
        playerMoney.SetActive(true);
    }
    IEnumerator TypeEffect(string line)
    {
        typerRunning = true;
        dialogueText.text = "";
        for (int i = 0; i < line.ToCharArray().Length; i++)
        {
            
            dialogueText.text += line.ToCharArray()[i];
            
            if (IsNpcLine)
            {
                if (line.ToCharArray()[i] != '.' || line.ToCharArray()[i] != ' ' || line.ToCharArray()[i] != '\n')
                {
                    audioSource.clip = allTypeClips[UnityEngine.Random.Range(0, allTypeClips.Length - 1)];
                    audioSource.Play();
                }
            }

            yield return new WaitForSeconds(typingSpeedInSeconds);

        }
        typerRunning = false;
    }
    void StopTypeEffect(SO_Dialog dialogue)
    {
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
        }

        string line = CleanString(dialogue.lines[counter - 1]);

        dialogueText.text = line;

        typerRunning = false;

        if (counter > dialogue.lines.Count)
        {
            Debug.LogError("END AT STOP TYPE");
            EndDialog();
        }
    }
    private string CleanString(string lineToClean)
    {
        string line = lineToClean;

        line = CheckStringForLineBreak(line);

        line = line.Replace("\r", "")
            .Replace("$playerName", player.PlayerName)
            .Replace("$recipeName", recipe.recipeName)
            .Replace("$storeName", player.StoreName);

        return line;
    }
    Sprite DisplayCharacterSprite(SO_Dialog dialogue)
    {
        if (dialogue.keyForCharacterDisplay == null && dialogue.spriteForCharacterDisplay != null)
        {
            Debug.LogError($"Mising a sprite key for sprite in dialog {dialogue}");
            return null;
        }
        if (dialogue.keyForCharacterDisplay == null && dialogue.spriteForCharacterDisplay == null)
        {
            Debug.LogError($"No Sprite has been found for this dialog {dialogue}");
            return null;
        }


        for (int i = 0; i < dialogue.keyForCharacterDisplay.Count; i++)
        {
            if (dialogue.keyForCharacterDisplay[i] == counter)
            {
                savedSprite = dialogue.spriteForCharacterDisplay[i];
                //npcSprite.GetComponent<Image>().sprite = savedSprite;
                npc.SetActive(true);

                return savedSprite;
            }
        }
        if (savedSprite)
        {
            //npcSprite.GetComponent<Image>().sprite = savedSprite;
            return savedSprite;
        }


        Debug.LogError($"No Sprite has been found for this dialog {dialogue}");
        return null;
    }
    string DisplayCharacterName(SO_Dialog dialogue)
    {
        if (dialogue.keyForName == null && dialogue.nameOfSpeaker == null)
        {
            Debug.LogError($"No Name has been found on Dialog {dialogue}.");
            return null;
        }

        if (dialogue.keyForName == null && dialogue.nameOfSpeaker != null)
        {
            savedName = dialogue.nameOfSpeaker[0];

            return CheckDisplayName();
        }

        for (int i = 0; i < dialogue.keyForName.Count; i++)
        {
            if (dialogue.keyForName[i] == counter)
            {
                savedName = dialogue.nameOfSpeaker[i];
                return CheckDisplayName();

            }
        }

        if(savedName == "")
        {
            Debug.LogError($"No Name has been found on Dialog {dialogue}.");
            return null;
        }

        return CheckDisplayName();
    }
    string CheckDisplayName()
    {
        IsNpcLine = savedName != "$playerName" ? true : false;

        if (savedName == "$playerName")
        {
            return player.PlayerName;
        }
        return savedName;
    }
    string CheckStringForLineBreak(string line)
    {
        char[] a = line.ToCharArray();
        int charCounter = 0;
        int curCheck = 0;

        List<int> spaces = new List<int>();

        for (int i = 1; i <= a.Length / maxCharPerLine; i++)
        {
            curCheck = i * maxCharPerLine;
            
            bool addedSpace = false;
            for (int j = 0; j < curCheck; j++)
            {
                if (a[j] == ' ')
                {
                    if (addedSpace)
                    {
                        spaces[charCounter] = j;
                    }
                    else
                    {
                        spaces.Add(j);
                        addedSpace = true;
                    }
                }
            }
            charCounter++;
        }
        string newString = "";
        for (int y = 0; y < a.Length; y++)
        {
            bool addSpace = false;
            for (int x = 0; x < spaces.Count; x++)
            {
                if (y == spaces[x])
                {
                    addSpace = true;
                }
            }

            if (addSpace)
            {
                newString += "\n";
            }
            else
            {
                 newString += a[y].ToString();
            }
        }

        return newString;
    }
}