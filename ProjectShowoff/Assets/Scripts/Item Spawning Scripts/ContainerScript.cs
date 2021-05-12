
public class ContainerScript : BoxScript<ItemBox>
{
	public override ItemBox contained
	{
		get => box;
		set => box = value;
	}

	private ItemBox box;
}