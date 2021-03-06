using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BoxType
{
	Type1,
	Type2,
	Type3
}

// TODO maybe call it BoxData or something?
public class ItemBoxData : IBoxData<Item>
{
	public float MoneyValue => GetBoxContentsValue();
	public BoxType Type => type;
	private readonly BoxType type;

	//sorting items by their item type
	private Dictionary<ItemType, float> boxContents;
	public Dictionary<ItemType, float> BoxContents => boxContents;

	public List<Item> Contents => lookUp;

	private List<Item> lookUp;

	public ItemBoxData(BoxType pBoxType)
	{
		type = pBoxType;
		boxContents = new Dictionary<ItemType, float>();
		lookUp = new List<Item>();
	}

	public void AddItemToBox(Item item)
	{
		// TODO move to AddToBox
		if (!boxContents.ContainsKey(item.Type)) //does the box already contain the item type?
		{
			boxContents.Add(item.Type, item.Value);
		}
		else
		{
			boxContents[item.Type] = boxContents[item.Type] + item.Value;
		}
		lookUp.Add(item);
	}

	public void RemoveItemFromBox(Item item)
	{
		// TODO move to RemoveFromBox
		if (lookUp.Contains(item))
		{
			boxContents[item.Type] -= item.Value;
			if (boxContents[item.Type] <= 0f)
			{
				boxContents.Remove(item.Type);
			}
			lookUp.Remove(item);
		}
	}

	public float GetBoxContentsValue()
	{
		float contentsSum = 0f;
		foreach (ItemType itemType in boxContents.Keys.ToList())
		{
			contentsSum += boxContents[itemType];
		}
		return contentsSum;
	}

	public void ShowBoxContents()
	{
		Debug.Log("Contents: ");
		foreach (ItemType itemType in boxContents.Keys.ToList())
		{
			Debug.Log(itemType.ToString() + ": " + boxContents[itemType]);
		}
	}

	public void AddToBox(Item contained)
	{
		AddItemToBox(contained);
	}

	public void RemoveFromBox(Item contained)
	{
		RemoveItemFromBox(contained);
	}
}
