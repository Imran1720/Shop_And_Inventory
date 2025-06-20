public interface IBuyPopUpManager
{
    void SetData(ItemData _itemData, int _itemCount);
    void OnShopRefresh(ItemData _data);
    void BuyItem();
}
