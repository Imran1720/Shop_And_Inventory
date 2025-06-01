using UnityEngine;

public class ItemCardModel
{
    private ItemCardController itemCardController;

    private ItemData currentItem;
    private int itemCount;
    private int itemsToBeBought;
    private ItemSpriteSO bgSpriteSO;

    public ItemCardModel(ItemSpriteSO _bgSprites)
    {
        bgSpriteSO = _bgSprites;
        itemsToBeBought = 1;
    }


    public void SetItem(ItemData _data)
    {
        currentItem = _data;
        itemCount = _data.quantity;
    }


    public ItemData GetItemData() => currentItem;
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
            case Rarity.LEGENDARY: return bgSpriteSO.legendaryBG;
            case Rarity.EPIC: return bgSpriteSO.epicBG;
            case Rarity.RARE: return bgSpriteSO.rareBG;
            case Rarity.COMMON: return bgSpriteSO.commonBG;
            default: return bgSpriteSO.veryCommonBG;

        }
    }

    public void SetNumberOfItemsToBuy(int value) => itemsToBeBought = value;
    public int GetNumberOfItemsToBuy() => itemsToBeBought;

    public int GetMaxItemAvailableCount() => itemCount;
    public int GetItemBuyingPrice() => currentItem.buyingPrice;

    public void SetController(ItemCardController _controller) => itemCardController = _controller;

    public ItemData GetCurrentItem() => currentItem;
}
