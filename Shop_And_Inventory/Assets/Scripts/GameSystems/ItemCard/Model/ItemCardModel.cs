using UnityEngine;

public class ItemCardModel
{
    private ItemCardController itemCardController;

    private ItemData currentItem;
    private int currentitemQuantity;
    private int itemsToBeBought = 1;
    private ItemSpriteSO backgroundSpritesSO;

    public ItemCardModel(ItemSpriteSO _bgSpritesSO) => backgroundSpritesSO = _bgSpritesSO;
    public ItemSpriteSO GetBackgroundSO() => backgroundSpritesSO;
    public ItemData GetCurrentItem() => currentItem;

    public int GetNumberOfItemsToBuy() => itemsToBeBought;
    public int GetMaxItemAvailableQuantity() => currentitemQuantity;


    public void SetNumberOfItemsToBuy(int value) => itemsToBeBought = value;
    public void SetController(ItemCardController _controller) => itemCardController = _controller;

    public void SetItem(ItemData _item)
    {
        currentItem = _item;
        currentitemQuantity = _item.quantity;
    }

}
