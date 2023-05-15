using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainStoreUpdating : MonoBehaviour
{
    GameManager manager;
    Player player;

    [SerializeField] TextMeshProUGUI playerMoney;
    [SerializeField] TextMeshProUGUI customersleft;

    [SerializeField] TextMeshProUGUI dayNum;

    void Start()
    {
        player = Player.instance;
        manager = GameManager.Instance;

        player.onMoneyChangedCallback += UpdateMoney;
        manager.onNextCustomerCallback += UpdateUI;
        manager.onDayChangedCallback += UpdateDayUI;
        UpdateMoney();
        UpdateUI();
        UpdateDayUI();
    }

    void UpdateDayUI()
    {
        dayNum.text = manager.day.ToString();
    }

    void UpdateUI()
    {
        customersleft.SetText("" + (manager.ExpectedCustomerAmount - manager.CustomerCounter));
    }

    void UpdateMoney()
    {
        playerMoney.SetText("" + player.moneyAmount);
    }

    void OnDisable()
    {
        player.onMoneyChangedCallback -= UpdateMoney;
        manager.onNextCustomerCallback -= UpdateUI;
        manager.onDayChangedCallback -= UpdateDayUI;
    }
}
