
public class ShopController
{
    private ShopView shopView;
    private ShopModel shopModel;

    public ShopController(ShopView _shopView, ShopModel _shopModel)
    {
        shopModel = _shopModel;
        shopView = _shopView;

        shopView.InitializeShopController(this);
        shopModel.InitializeShopController(this);
    }
}
