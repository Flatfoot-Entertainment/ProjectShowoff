using UnityEngine;

public class ItemScript : BoxScript<Item>
{
	public override Item contained
	{
		get => item;
		set => item = value;
	}

	private Item item;
	[SerializeField] private ItemType itemType;
	[SerializeField] private float itemValue;
	[SerializeField] private int price;

	private void Start()
	{
		itemType = item.Type;
		itemValue = item.Value;
		price = item.Price;
	}
}