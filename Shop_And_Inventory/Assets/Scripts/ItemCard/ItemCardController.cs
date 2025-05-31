using UnityEngine;

public class ItemCardController
{
    private ItemCardView itemCardView;
    private ItemCardModel itemCardModel;


    public ItemCardController(ItemCardView _view, ItemCardModel _model)
    {
        itemCardView = _view;
        itemCardModel = _model;

        itemCardView.SetController(this);
        itemCardModel.SetController(this);

        EventService.Instance.OnShopRefresh.AddListener(OnShopRefresh);
        EventService.Instance.OnShopUpdate.AddListener(SetItem);
        EventService.Instance.OnItemSelected.AddListener(SetItem);
        EventService.Instance.OnItemGathered.AddListener(OnItemGathered);
    }

    ~ItemCardController()
    {
        EventService.Instance.OnShopRefresh.RemoveListener(OnShopRefresh);
        EventService.Instance.OnShopUpdate.RemoveListener(SetItem);
        EventService.Instance.OnItemSelected.RemoveListener(SetItem);
        EventService.Instance.OnItemGathered.RemoveListener(OnItemGathered);
    }

    public void SetItem(ItemData _data)
    {
        itemCardModel.SetItem(_data);
        Reset();
        RefreshUI();
    }

    private void OnShopRefresh(ItemData _data)
    {
        if (UIUtility.Instance.IsShopCardActive())
            SetItem(_data);
    }
    private void OnItemGathered(ItemData _data)
    {
        if (!UIUtility.Instance.IsShopCardActive())
            SetItem(_data);
    }
    public void RefreshUI()
    {
        ItemData data = itemCardModel.GetItemData();
        itemCardView.RefreshUI(data, itemCardModel.GetBGSprite(data.itemRarity), itemCardModel.GetTitleColor(data.itemRarity), itemCardModel.GetNumberOfItemsToBuy());
    }

    public void DecreaseItemCount()
    {
        int count = itemCardModel.GetNumberOfItemsToBuy();
        if (count >= 1)
        {
            itemCardModel.SetNumberOfItemsToBuy(--count);
        }
        itemCardView.UpdateBuyingItemCount(count);
    }

    public void IncreaseItemCount()
    {
        int count = itemCardModel.GetNumberOfItemsToBuy();
        if (count < itemCardModel.GetMaxItemAvailableCount())
        {
            itemCardModel.SetNumberOfItemsToBuy(++count);
        }

        itemCardView.UpdateBuyingItemCount(count);
    }
    public void BuyItem()
    {
        if (itemCardModel.GetNumberOfItemsToBuy() <= 0)
        {
            return;
        }
        PopUpManager.Instance.SetData(itemCardModel.GetCurrentItem(), itemCardModel.GetNumberOfItemsToBuy());
    }
    private void OnBuyPopUp(ItemData _data)
    {
        itemCardModel.SetItem(_data);
        Reset();
    }
    public void Reset()
    {
        itemCardModel.SetNumberOfItemsToBuy(1);
    }
}
