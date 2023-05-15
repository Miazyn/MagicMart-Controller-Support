using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCreation : MonoBehaviour
{
    GameManager gameManager;
    Player player;
    SO_Inventory playerInventory;


    [SerializeField] int minCustomers = 5;
    [SerializeField] int maxCustomers = 7;

    int customerAmount;

    [SerializeField] SO_NPC blueprint;
    [SerializeField] SO_NPC[] mainChars;

    Sprite[] sprites;
    SO_Voice[] voices;
    SO_Recipe[] allRecipes;

    [SerializeField] SO_NPC[] allNPCs;

    [SerializeField] GameObject cookButton;
    [SerializeField] GameObject shopButton;
    [SerializeField] GameObject playerMoney;

    int npcOfTheDay = 0;

    SO_NPC[] possibleNPC;
    void Awake()
    {
        allNPCs = Resources.LoadAll<SO_NPC>("GenericNPC");
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        player = Player.instance;
        playerInventory = player.inventory;


        DayStart();
        if(gameManager.curState == GameManager.GameState.IdleState)
        {
            EnableItems();
        }

        gameManager.onDayChangedCallback += DayStart;
    }

    public void DayStart()
    {
        if (gameManager.curState == GameManager.GameState.DayStart)
        {
            customerAmount = Random.Range(minCustomers, maxCustomers);

            npcOfTheDay = Random.Range(0, customerAmount);
            StartCoroutine(CreatingCustomers());
        }
    }


    IEnumerator CreatingCustomers()
    {
        gameManager.Customers = new SO_NPC[customerAmount];
        for(int i = 0; i < customerAmount; i++)
        {
            if(i == npcOfTheDay)
            {
                gameManager.Customers[i] = mainChars[Random.Range(0, mainChars.Length)];
            }
            else
            {
                gameManager.Customers[i] = allNPCs[Random.Range(0, allNPCs.Length)];
            }
        }


        yield return null;
        gameManager.ExpectedCustomerAmount = customerAmount;
        gameManager.ChangeGameState(GameManager.GameState.StartState);
    }

    void EnableItems()
    {
        shopButton.SetActive(true);
        cookButton.SetActive(true);
        playerMoney.SetActive(true);
    }

    private void OnDisable()
    {
        gameManager.onDayChangedCallback -= DayStart;
    }
}
