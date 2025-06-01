public class EventService
{
    private static EventService instance;
    public static EventService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventService();
            }
            return instance;
        }
    }

    public EventController<ItemData> OnItemBought { get; private set; }
    public EventController<ItemData> OnShopRefresh { get; private set; }
    public EventController<ItemData> OnBuyPopUp { get; private set; }
    public EventController<ItemData> OnShopUpdate { get; private set; }
    public EventController<ItemData> OnItemSelected { get; private set; }

    public EventService()
    {
        OnItemBought = new EventController<ItemData>();
        OnShopRefresh = new EventController<ItemData>();
        OnBuyPopUp = new EventController<ItemData>();
        OnShopUpdate = new EventController<ItemData>();
        OnItemSelected = new EventController<ItemData>();
    }

}
