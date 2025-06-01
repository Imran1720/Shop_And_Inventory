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

    public void SetItem(ItemData _item)
    {
        itemCardModel.SetItem(_item);
        Reset();
        RefreshUI();
    }

    public void RefreshUI()
    {
        ItemData item = itemCardModel.GetCurrentItem();

        itemCardView.RefreshUI(item,
            itemCardModel.GetBGSprite(item.itemRarity),
            itemCardModel.GetTitleColor(item.itemRarity),
            itemCardModel.GetNumberOfItemsToBuy());
    }

    public void DecreaseItemCount()
    {
        int count = itemCardModel.GetNumberOfItemsToBuy();
        if (count > 1)
        {
            itemCardModel.SetNumberOfItemsToBuy(count - 1);
        }
        itemCardView.UpdateBuyingItemCount(itemCardModel.GetNumberOfItemsToBuy());
    }

    public void IncreaseItemCount()
    {
        int count = itemCardModel.GetNumberOfItemsToBuy();
        int maxItemAvailablity = itemCardModel.GetMaxItemAvailableQuantity();

        if (count < maxItemAvailablity)
        {
            itemCardModel.SetNumberOfItemsToBuy(count + 1);
        }
        itemCardView.UpdateBuyingItemCount(itemCardModel.GetNumberOfItemsToBuy());
    }

    public void BuyItem()
    {
        int itemQuantity = itemCardModel.GetNumberOfItemsToBuy();
        ItemData item = itemCardModel.GetCurrentItem();

        if (itemQuantity <= 0) return;

        GameService.instance.UIManager.SetBuyPopUpData(item, itemQuantity);
        GameService.instance.UIManager.ShowBuyPopUp();
    }

    public void Reset()
    {
        itemCardModel.SetNumberOfItemsToBuy(1);
    }

    private void OnShopRefresh(ItemData _item)
    {
        if (GameService.instance.UIManager.IsShopCardActive())
            SetItem(_item);
    }
    private void OnItemGathered(ItemData _item)
    {
        if (!GameService.instance.UIManager.IsShopCardActive())
            SetItem(_item);
    }
}
