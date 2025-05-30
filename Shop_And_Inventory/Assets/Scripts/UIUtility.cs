using System.Collections;
using System.Collections.Generic;
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

    private void OnEnable()
    {
        EventService.Instance.OnItemBought.AddListener(ShowBoughtNotification);
        EventService.Instance.OnItemSelected.AddListener(OnItemSelected);
    }

    private void OnDisable()
    {
        EventService.Instance.OnItemBought.RemoveListener(ShowBoughtNotification);
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
    public GameObject GetNotificationPanel() => notificationPannel;
    public void EnableBuyPopPanel() => buyPopUPPanel.SetActive(true);


    private void OnItemSelected(ItemData _data)
    {
        inventoryItemCardPanel.SetActive(!_data.isShopItem);
        shopItemCardPanel.SetActive(_data.isShopItem);
    }
    public bool IsShopCardActive() => shopItemCardPanel.activeSelf;
}
