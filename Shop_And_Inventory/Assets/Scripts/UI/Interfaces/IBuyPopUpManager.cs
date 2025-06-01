public interface IBuyPopUpManager
{
    void SetData(ItemData _itemData, int _itemCount);
    void BuyItem();
    void OnShopRefresh(ItemData _data);
}
