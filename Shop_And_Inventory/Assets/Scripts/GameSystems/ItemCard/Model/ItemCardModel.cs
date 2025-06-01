using UnityEngine;

public class ItemCardModel
{
    private ItemCardController itemCardController;

    private ItemData currentItem;
    private int currentitemQuantity;
    private int itemsToBeBought = 1;
    private ItemSpriteSO backgroundSpritesSO;

    public ItemCardModel(ItemSpriteSO _bgSpritesSO)
    {
        backgroundSpritesSO = _bgSpritesSO;
    }
    public void SetItem(ItemData _item)
    {
        currentItem = _item;
        currentitemQuantity = _item.quantity;
    }

    public ItemData GetCurrentItem() => currentItem;
    public int GetItemBuyingPrice() => currentItem.buyingPrice;
    public int GetItemSellingPrice() => currentItem.sellingPrice;
    public int GetMaxItemAvailableQuantity() => currentitemQuantity;
    public int GetNumberOfItemsToBuy() => itemsToBeBought;

    public Color GetTitleColor(Rarity itemRarity)
    {
        switch (itemRarity)
        {
            case Rarity.LEGENDARY: return Color.yellow;
            case Rarity.EPIC: return Color.magenta;
            case Rarity.RARE: return Color.cyan;
            case Rarity.COMMON: return Color.green;
            default: return Color.white;

        }
    }
    public Sprite GetBGSprite(Rarity itemRarity)
    {

        switch (itemRarity)
        {
            case Rarity.LEGENDARY: return backgroundSpritesSO.legendaryBG;
            case Rarity.EPIC: return backgroundSpritesSO.epicBG;
            case Rarity.RARE: return backgroundSpritesSO.rareBG;
            case Rarity.COMMON: return backgroundSpritesSO.commonBG;
            default: return backgroundSpritesSO.veryCommonBG;

        }
    }

    public void SetNumberOfItemsToBuy(int value) => itemsToBeBought = value;
    public void SetController(ItemCardController _controller) => itemCardController = _controller;
}
