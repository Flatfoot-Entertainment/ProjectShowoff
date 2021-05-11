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

public class Box
{
	public int MoneyValue => 1; // TODO calculate
	public BoxType Type => type;
	private readonly BoxType type;

	//sorting items by their item type
	private Dictionary<ItemType, float> boxContents;
	public Dictionary<ItemType, float> BoxContents => boxContents;
	private List<Item> lookUp;

	public Box(BoxType pBoxType)
	{
		type = pBoxType;
		boxContents = new Dictionary<ItemType, float>();
		lookUp = new List<Item>();
	}

	public void AddItemToBox(Item item)
	{
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
        foreach(ItemType itemType in boxContents.Keys.ToList())
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
}
