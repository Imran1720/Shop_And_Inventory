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
        EventService.Instance.OnItemBought.AddListener(ShowNotification);
    }

    private void OnDisable()
    {
        EventService.Instance.OnItemBought.RemoveListener(ShowNotification);
    }

    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetButtonRarity(Rarity _rarity)
    {
        return itemButtonRarityCart[(int)_rarity];
    }

    private void ShowNotification(ItemData data)
    {
        notificationPannel.SetActive(true);
        NotificationManager.Instance.SetNotificationData(data.itemName);
    }

    public GameObject GetNotificationPanel() => notificationPannel;
    public void EnableBuyPopPanel() => buyPopUPPanel.SetActive(true);
}
