using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIUtility : MonoBehaviour
{
    public static UIUtility Instance;

    public List<Sprite> itemButtonRarityCart;

    public GameObject notificationPannel;
    public GameObject buyPopUPPanel;
    public GameObject shopItemCardPanel;
    public GameObject inventoryItemCardPanel;
    public TextMeshProUGUI coinsCountText;

    public int playerCoins = 0;
    private void OnEnable()
    {
        EventService.Instance.OnItemBought.AddListener(ShowBoughtNotification);
        EventService.Instance.OnItemSold.AddListener(ShowItemSoldNotification);
        EventService.Instance.OnItemSelected.AddListener(OnItemSelected);
    }

    private void OnDisable()
    {
        EventService.Instance.OnItemBought.RemoveListener(ShowBoughtNotification);
        EventService.Instance.OnItemSold.RemoveListener(ShowItemSoldNotification);
        EventService.Instance.OnItemSelected.RemoveListener(OnItemSelected);
    }

    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetButtonRarity(Rarity _rarity)
    {
        return itemButtonRarityCart[(int)_rarity];
    }

    private void ShowBoughtNotification(ItemData data)
    {
        notificationPannel.SetActive(true);
        NotificationManager.Instance.SetNotificationData("You Bought A " + data.itemName);
    }

    public void ShowInventoryFullNotification()
    {
        notificationPannel.SetActive(true);
        NotificationManager.Instance.SetNotificationData("Inventory is Full!!");
    }

    public void ShowItemSoldNotification(ItemData data)
    {
        notificationPannel.SetActive(true);
        NotificationManager.Instance.SetNotificationData("You Sold A " + data.itemName);
    }

    public void ShowNoMoneyNotification()
    {
        notificationPannel.SetActive(true);
        NotificationManager.Instance.SetNotificationData("Insufficient Funds!!");
    }

    public GameObject GetNotificationPanel() => notificationPannel;
    public void EnableBuyPopPanel() => buyPopUPPanel.SetActive(true);

    public void SetCoins(int count) => coinsCountText.text = count.ToString();

    private void OnItemSelected(ItemData _data)
    {
        inventoryItemCardPanel.SetActive(!_data.isShopItem);
        shopItemCardPanel.SetActive(_data.isShopItem);
    }
    public bool IsShopCardActive() => shopItemCardPanel.activeSelf;

    public void IncrementCoins(int value)
    {
        playerCoins += value;
        SetCoins(playerCoins);
    }
    public void DecrementCoins(int value)
    {
        playerCoins -= value;
        if (playerCoins <= 0)
        {
            playerCoins = 0;
        }
        SetCoins(playerCoins);
    }
    public int GetTotalMoney() => playerCoins;
}
