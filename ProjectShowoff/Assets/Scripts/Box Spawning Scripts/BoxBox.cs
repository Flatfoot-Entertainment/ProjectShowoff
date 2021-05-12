using System.Collections.Generic;

public class BoxBox : IBox<ItemBox>
{
	public float MoneyValue
	{
		get
		{
			float val = 0f;
			foreach (ItemBox box in Contents)
			{
				val += box.MoneyValue;
			}
			return val;
		}
	}

	public BoxBox()
	{

	}

	public List<ItemBox> Contents => contents;
	private List<ItemBox> contents = new List<ItemBox>();

	public void AddToBox(ItemBox contained)
	{
		if (!contents.Contains(contained))
		{
			contents.Add(contained);
		}
	}

	public void RemoveFromBox(ItemBox contained)
	{
		contents.Remove(contained);
	}
}