using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BoxType
{
	Type1,
	Type2,
	Type3
}

public class ItemBoxData : IBoxData<Item>
{
	public int MoneyValue => GetBoxContentsValue();
	public BoxType Type => type;
	private readonly BoxType type;

	//sorting items by their item type
	private Dictionary<ItemType, int> boxContents = new Dictionary<ItemType, int>();
	public Dictionary<ItemType, int> BoxContents => boxContents;

	public List<Item> Contents => lookUp;

	private List<Item> lookUp = new List<Item>();

	public ItemBoxData(BoxType pBoxType)
	{
		type = pBoxType;
	}

	public int GetBoxContentsValue()
	{
		int contentsSum = 0;
		foreach (ItemType itemType in boxContents.Keys.ToList())
		{
			contentsSum += boxContents[itemType];
		}
		return contentsSum;
	}

	public void AddToBox(Item contained)
	{
		Debug.Log($"FUCK {boxContents != null}");
		Debug.Log($"ME {contained != null}");
		if (!boxContents.ContainsKey(contained.Type))
			boxContents.Add(contained.Type, contained.Value);
		else
			boxContents[contained.Type] = boxContents[contained.Type] + contained.Value;
		lookUp.Add(contained);
	}

	public void RemoveFromBox(Item contained)
	{
		if (lookUp.Contains(contained))
		{
			boxContents[contained.Type] -= contained.Value;
			if (boxContents[contained.Type] <= 0f)
			{
				boxContents.Remove(contained.Type);
			}
			lookUp.Remove(contained);
		}
	}
}
