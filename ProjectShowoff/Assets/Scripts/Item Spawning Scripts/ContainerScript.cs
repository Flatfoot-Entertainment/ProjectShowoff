
public class ContainerScript : BoxScript<ItemBoxData>
{
	public override ItemBoxData contained
	{
		get => box;
		set => box = value;
	}

	private ItemBoxData box;
}