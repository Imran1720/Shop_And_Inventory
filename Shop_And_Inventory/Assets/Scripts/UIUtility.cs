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

    private void OnEnable()
    {
        EventService.Instance.OnItemBought.AddListener(ShowBoughtNotification);
    }

    private void OnDisable()
    {
        EventService.Instance.OnItemBought.RemoveListener(ShowBoughtNotification);
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
}
