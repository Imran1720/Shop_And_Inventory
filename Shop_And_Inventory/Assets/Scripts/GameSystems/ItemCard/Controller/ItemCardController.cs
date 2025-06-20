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
        AddObservers();
    }

    public void AddObservers()
    {
        EventService.Instance.OnShopRefresh.AddListener(OnShopRefresh);
        EventService.Instance.OnShopUpdate.AddListener(SetItem);
        EventService.Instance.OnItemSelected.AddListener(SetItem);
        EventService.Instance.OnItemGathered.AddListener(OnItemGathered);
    }

    public void RemoveObservers()
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
        itemCardView.RefreshUI(item, GetBGSprite(item.itemRarity), GetTitleColor(item.itemRarity), itemCardModel.GetNumberOfItemsToBuy());
    }

    public void DecreaseItemCount()
    {
        SoundManager.Instance.PlaySoundFX(Sounds.BUTTON_CLICK);
        int count = itemCardModel.GetNumberOfItemsToBuy();
        if (count > 1)
        {
            itemCardModel.SetNumberOfItemsToBuy(count - 1);
        }
        itemCardView.UpdateBuyingItemCount(itemCardModel.GetNumberOfItemsToBuy());
    }

    public void IncreaseItemCount()
    {
        SoundManager.Instance.PlaySoundFX(Sounds.BUTTON_CLICK);
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
        SoundManager.Instance.PlaySoundFX(Sounds.POPUP);
        int itemQuantity = itemCardModel.GetNumberOfItemsToBuy();
        ItemData item = itemCardModel.GetCurrentItem();

        if (itemQuantity <= 0) return;

        GameService.Instance.UIManager.ShowBuyPopUp(item, itemQuantity);
    }

    public void Reset() => itemCardModel.SetNumberOfItemsToBuy(1);

    private void OnShopRefresh(ItemData _item)
    {
        if (GameService.Instance.UIManager.IsShopCardActive())
            SetItem(_item);
    }
    private void OnItemGathered(ItemData _item)
    {
        if (!GameService.Instance.UIManager.IsShopCardActive())
            SetItem(_item);
    }

    public Color GetTitleColor(Rarity itemRarity)
    {
        switch (itemRarity)
        {
            case Rarity.LEGENDARY:
                return Color.yellow;
            case Rarity.EPIC:
                return Color.magenta;
            case Rarity.RARE:
                return Color.cyan;
            case Rarity.COMMON:
                return Color.green;
            default:
                return Color.white;
        }
    }

    public Sprite GetBGSprite(Rarity itemRarity)
    {
        ItemSpriteSO backgroundSpritesSO = itemCardModel.GetBackgroundSO();
        switch (itemRarity)
        {
            case Rarity.LEGENDARY:
                return backgroundSpritesSO.legendaryBG;
            case Rarity.EPIC:
                return backgroundSpritesSO.epicBG;
            case Rarity.RARE:
                return backgroundSpritesSO.rareBG;
            case Rarity.COMMON:
                return backgroundSpritesSO.commonBG;
            default:
                return backgroundSpritesSO.veryCommonBG;
        }
    }
}
