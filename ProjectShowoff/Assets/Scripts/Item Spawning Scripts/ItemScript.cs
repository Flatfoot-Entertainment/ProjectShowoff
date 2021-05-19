using UnityEngine;

public class ItemScript : BoxScript<Item>
{
	public override Item contained
	{
		get => item;
		set
		{
			item = value;
			itemType = item.Type;
			itemValue = item.Value;
			price = item.Price;
		}
	}

	private Item item;
	[SerializeField] private ItemType itemType;
	[SerializeField] private float itemValue;
	[SerializeField] private int price;
}