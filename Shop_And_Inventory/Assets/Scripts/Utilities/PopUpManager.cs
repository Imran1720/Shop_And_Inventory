using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Instance;

    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button cancelButton;

    ItemData itemData;
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        EventService.Instance.OnShopRefresh.AddListener(OnShopRefresh);
    }

    private void OnDisable()
    {
        EventService.Instance.OnShopRefresh.RemoveListener(OnShopRefresh);
    }
    private void Start()
    {
        InitializeButtonListeners();
    }

    private void InitializeButtonListeners()
    {
        acceptButton.onClick.AddListener(BuyItem);
        cancelButton.onClick.AddListener(ClosePopUp);
    }

    public void SetData(ItemData _itemData, int _itemCount)
    {
        itemData = _itemData;
        itemData.quantity = _itemCount;
        int temQuantity = _itemCount;
        if (itemData.isShopItem)
        {
            promptText.text = $"Do you want to buy {itemData.itemName} x{_itemCount} for {(itemData.buyingPrice * _itemCount)}?";
        }
        else
        {
            promptText.text = $"Do you want to Sell {itemData.itemName} x{_itemCount} for {(itemData.sellingPrice * _itemCount)}?";
        }
    }


    private void BuyItem()
    {
        //event to update count in the shop
        gameObject.SetActive(false);
        if (itemData.isShopItem)
        {
            int totalItemCost = itemData.quantity * itemData.buyingPrice;
            if (totalItemCost <= GameService.instance.UIManager.GetTotalMoney())
                EventService.Instance.OnItemBought.InvokeEvent(itemData);
            else
            {
                GameService.instance.UIManager.ShowOutOfFundNotification();
            }
        }
        else
        {
            EventService.Instance.OnItemSold.InvokeEvent(itemData);
        }
    }

    private void ClosePopUp() => gameObject.SetActive(false);
    private void OnShopRefresh(ItemData _data)
    {
        itemData = _data;
        gameObject.SetActive(false);
    }

}
