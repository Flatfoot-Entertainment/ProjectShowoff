
public class ManageConveyorsEvent : Event
{
    public ItemSpawner ItemSpawner => itemSpawner;
    public float DelayTime => delayTime;

    private ItemSpawner itemSpawner;
    private float delayTime;
    public ManageConveyorsEvent(ItemSpawner pItemSpawner, float pDelayTime) : base(EventType.ManageConveyors)
    {
        itemSpawner = pItemSpawner;
        delayTime = pDelayTime;
    }
}
