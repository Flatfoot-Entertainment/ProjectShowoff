
public class BoxScript : PropertyHolder<ItemBox>
{
	public override ItemBox contained
	{
		get => box;
		set => box = value;
	}

	private ItemBox box;
}