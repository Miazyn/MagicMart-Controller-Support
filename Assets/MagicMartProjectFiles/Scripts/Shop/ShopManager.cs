using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject ShopTilePrefab;

    SO_Ingredient[] allIngredients;

    public Transform parentTransform;

    [SerializeField] SceneMana sceneMana;

    [Header("Money")]
    [SerializeField] TextMeshProUGUI money;
    [SerializeField] AudioSource clickButton;
    [SerializeField] AudioSource boughtSound;
    Player player;

    void Awake()
    {
        allIngredients = Resources.LoadAll<SO_Ingredient>("CookingIngredients");
    }

    void Start()
    {
        player = Player.instance;

        player.onMoneyChangedCallback += UpdateMoneyUI;
        for(int i =0; i < allIngredients.Length; i++)
        {
            GameObject _shopTile = Instantiate(ShopTilePrefab, parentTransform);

            _shopTile.GetComponent<RectTransform>().anchoredPosition = parentTransform.GetComponent<RectTransform>().anchoredPosition;
            _shopTile.GetComponent<ShopTile>().Ingredient = allIngredients[i];
            _shopTile.GetComponent<ShopTile>().buttonClick = clickButton;
            _shopTile.GetComponent<ShopTile>().buySound = boughtSound;

            _shopTile.GetComponent<ShopTile>().SetUpTile();
        }
        UpdateMoneyUI();
    }

    void UpdateMoneyUI()
    {
        money.SetText("" + player.moneyAmount);
    }

    public void LeaveShop()
    {
        sceneMana.LoadNextScene("MainStore");
    }

    void OnDisable()
    {
        player.onMoneyChangedCallback -= UpdateMoneyUI;
    }
}
