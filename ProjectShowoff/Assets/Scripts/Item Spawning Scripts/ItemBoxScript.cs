
public class ItemBoxScript : BoxScript<ItemBoxData>
{
	public event System.Action AddedToBoxCallback;

	public override ItemBoxData contained
	{
		get => box;
		set => box = value;
	}

	private ItemBoxData box;

	public override void OnAddedToBox()
	{
		base.OnAddedToBox();
		AddedToBoxCallback?.Invoke();
	}
}