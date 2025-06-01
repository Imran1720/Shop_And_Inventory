using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpUIManager : MonoBehaviour, IBuyPopUpManager
{
    [Header("Message Prompt")]
    [SerializeField] private TextMeshProUGUI promptText;

    [Header("Buttons")]
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button cancelButton;

    [Header("Pop-Up Panel")]
    [SerializeField] private GameObject popUpPanel;
    [SerializeField] private Image popUpPanelBG;

    ItemData itemData;

    private void Start()
    {
        InitializeButtonListeners();
        ClosePopUp();
    }

    private void InitializeButtonListeners()
    {
        acceptButton.onClick.AddListener(BuyItem);
        cancelButton.onClick.AddListener(ClosePopUp);
    }

    public void BuyItem()
    {
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
        ClosePopUp();
    }

    public void OnShopRefresh(ItemData _data)
    {

        if (itemData.isShopItem)
        {
            ClosePopUp();
        }
    }

    public void SetData(ItemData _itemData, int _itemCount)
    {
        itemData = _itemData;
        itemData.quantity = _itemCount;

        if (itemData.isShopItem)
        {
            promptText.text = $"Do you want to buy {itemData.itemName} x{_itemCount} for {(itemData.buyingPrice * _itemCount)}?";
        }
        else
        {
            promptText.text = $"Do you want to Sell {itemData.itemName} x{_itemCount} for {(itemData.sellingPrice * _itemCount)}?";
        }
    }

    private void ClosePopUp()
    {
        SoundManager.Instance.PlaySoundFX(Sounds.CANCEL);
        popUpPanelBG.enabled = false;
        popUpPanel.SetActive(false);
    }
    public void ShowPopUp()
    {
        popUpPanelBG.enabled = true;
        popUpPanel.SetActive(true);
    }
}
