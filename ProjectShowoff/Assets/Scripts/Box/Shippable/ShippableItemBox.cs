
public class ShippableItemBox : ShippableBox<ItemBoxData, Item>
{
	protected override void OnInit()
	{
		base.OnInit();
		ItemBoxScript data = gameObject.AddComponent<ItemBoxScript>();
		data.AddedToBoxCallback += Deliver;
		data.contained = Box;
	}
}