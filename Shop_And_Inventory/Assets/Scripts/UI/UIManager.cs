using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Coin Data")]
    [SerializeField] private CoinUIManager coinUIManager;

    [Header("Item-Card Data")]
    [SerializeField] private ItemCardUIManager itemCardUIManager;

    [Header("Notification Data")]
    [SerializeField] private NotificationUIManager notificationUIManager;
    [SerializeField] private float notificationDuration;

    [Header("Notification Messages")]
    [SerializeField] private string inventoryFullMessage;
    [SerializeField] private string outOfFundMessage;

    [Header("Pop-Up panel Data")]
    [SerializeField] private PopUpUIManager buyPopUpUIManager;

    [Header("Panel Data")]
    public GameObject buyPopUPPanel;

    private void OnEnable()
    {
        EventService.Instance.OnItemBought.AddListener(OnItemBought);
        EventService.Instance.OnItemSold.AddListener(OnItemSold);
        EventService.Instance.OnItemSelected.AddListener(OnItemSelected);
        EventService.Instance.OnShopRefresh.AddListener(OnShopRefresh);
    }

    private void OnDisable()
    {
        EventService.Instance.OnItemBought.RemoveListener(OnItemBought);
        EventService.Instance.OnItemSold.RemoveListener(OnItemSold);
        EventService.Instance.OnItemSelected.RemoveListener(OnItemSelected);
        EventService.Instance.OnShopRefresh.RemoveListener(OnShopRefresh);
    }

    private void Start()
    {
        notificationUIManager.SetData(notificationDuration, outOfFundMessage, inventoryFullMessage);
    }

    private void OnItemBought(ItemData _data) => notificationUIManager.ShowNotification($"You bought a {_data.itemName}");
    private void OnItemSold(ItemData _data) => notificationUIManager.ShowNotification($"You sold a {_data.itemName}");
    private void OnItemSelected(ItemData _data) => itemCardUIManager.OnItemSelected(_data.isShopItem);
    private void OnShopRefresh(ItemData _data) => buyPopUpUIManager.OnShopRefresh(_data);


    //Notification UI
    public void ShowInventoryFullNotification() => notificationUIManager.ShowInventoryFull();
    public void ShowOutOfFundNotification() => notificationUIManager.ShowOutOfFund();

    //Coin UI
    public void IncrementCoins(int value) => coinUIManager.IncrementCoins(value);
    public void DecrementCoins(int value) => coinUIManager.DecrementCoins(value);
    public int GetTotalMoney() => coinUIManager.GetTotalCoins();

    //ItemCard UI
    public bool IsShopCardActive() => itemCardUIManager.IsShopCardActive();

    //Buy Pop-UP UI
    public void SetBuyPopUpData(ItemData _data, int _itemCount) => buyPopUpUIManager.SetData(_data, _itemCount);
    public void ShowBuyPopUp() => buyPopUpUIManager.ShowPopUp();

}
