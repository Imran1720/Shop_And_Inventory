using UnityEngine;

public class ShopSpawner : MonoBehaviour
{
    [Header("Script")]
    private ShopView shopView;

    [Header("Objects")]
    [SerializeField] private ShopView shopPrefab;
    [SerializeField] private Item itemCardPrefab;
    [SerializeField] private ItemDataBase itemDatabase;

    [Header("Data")]
    [SerializeField] public int initialShopItemCount;
    [SerializeField] private int shopRefreshTime;

    private void Start()
    {
        CreateShop();

        ShopModel shopModel = new ShopModel(itemCardPrefab, GameService.Instance.GetGameItemList(), initialShopItemCount, shopRefreshTime);
        ShopController controller = new ShopController(shopView, shopModel);
    }

    private void CreateShop()
    {
        shopView = Instantiate(shopPrefab);
        shopView.transform.SetParent(transform, false);
    }
}
