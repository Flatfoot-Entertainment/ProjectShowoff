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

	private void OnTransformParentChanged()
	{
		if (transform.parent.GetComponent<ContainerController>())
		{
			// TODO filthy hack (for now)
			// Function gets called, if it gets added to the box
			Deliver();
		}
	}
}