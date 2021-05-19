using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxController : BoxController<ItemBoxData, Item>
{
	public AnimationClip ClosingAnimation => closingAnimation;

	[SerializeField] private ShippableItemBox shippableBoxPrefab;
	[SerializeField] private AnimationClip closingAnimation;

	private ItemBoxData box;
	protected override void OnAwake()
	{
		base.OnAwake();
		box = new ItemBoxData(BoxType.Type1); // TODO multiple types
	}

	protected override ShippableBox<ItemBoxData, Item> InstantiateShipped()
	{
		return Instantiate<ShippableItemBox>(shippableBoxPrefab, transform.position, transform.rotation, transform.parent);
	}

	public override ItemBoxData Box
	{
		get => box;
		protected set => box = value;
		
	}

	protected override void PurgeContents()
	{
		foreach (GameObject gO in contained) Lean.Pool.LeanPool.Despawn(gO);
	}
}
