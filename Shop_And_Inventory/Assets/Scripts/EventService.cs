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
    public EventController OnShopRefresh { get; private set; }

    public EventService()
    {
        OnItemBought = new EventController<ItemData>();
        OnShopRefresh = new EventController();
    }

}
