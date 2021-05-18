using System.Collections.Generic;

public class ContainerData : IBoxData<ItemBoxData>
{
	public float MoneyValue
	{
		get
		{
			float val = 0f;
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

	public void AddToBox(ItemBoxData contained)
	{
		if (!contents.Contains(contained))
		{
			contents.Add(contained);
		}
	}

	public void RemoveFromBox(ItemBoxData contained)
	{
		contents.Remove(contained);
	}

	public Dictionary<ItemType, float> GetItems()
	{
		Dictionary<ItemType, float> ret = new Dictionary<ItemType, float>();
		foreach (ItemBoxData box in Contents)
		{
			foreach (Item item in box.Contents)
			{
				if (ret.ContainsKey(item.Type)) ret[item.Type] += item.Value;
				else ret[item.Type] = item.Value;
			}
		}
		return ret;
	}
}