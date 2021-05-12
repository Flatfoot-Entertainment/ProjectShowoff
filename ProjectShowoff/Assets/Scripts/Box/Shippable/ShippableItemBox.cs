using UnityEngine;

public class ShippableItemBox : ShippableBox<ItemBoxData, Item>
{
	protected override void OnInit()
	{
		base.OnInit();
		Debug.Log("Add Box to Box");
		ItemBoxScript data = gameObject.AddComponent<ItemBoxScript>();
		data.contained = Box;
	}
}