using System.Collections.Generic;

public class ContainerData : IBoxData<ItemBoxData>
{
	public int MoneyValue
	{
		get
		{
			int val = 0;
			foreach (ItemBoxData box in Contents)
			{
				val += box.MoneyValue;
			}
			return val;
		}
	}

	public ContainerData()
	{

	}

	public List<ItemBoxData> Contents => contents;
	private List<ItemBoxData> contents = new List<ItemBoxData>();
	private Dictionary<ItemType, int> cachedItemValues = new Dictionary<ItemType, int>();

	public void AddToBox(ItemBoxData contained)
	{
		if (!contents.Contains(contained))
		{
			contents.Add(contained);

			AddBoxToCached(contained);
		}
	}

	public void RemoveFromBox(ItemBoxData contained)
	{
		contents.Remove(contained);
		RemoveBoxFromCached(contained);
	}

	private void AddBoxToCached(ItemBoxData box)
	{
		foreach (Item item in box.Contents)
		{
			if (cachedItemValues.ContainsKey(item.Type))
				cachedItemValues[item.Type] += item.Value;
			else cachedItemValues[item.Type] = item.Value;
		}
	}

	private void RemoveBoxFromCached(ItemBoxData box)
	{
		foreach (Item item in box.Contents)
		{
			if (cachedItemValues.ContainsKey(item.Type))
			{
				cachedItemValues[item.Type] -= item.Value;
				if (cachedItemValues[item.Type] <= 0)
					cachedItemValues.Remove(item.Type);
			}
		}
	}

	public Dictionary<ItemType, int> GetItems()
	{
		return cachedItemValues;
	}
}