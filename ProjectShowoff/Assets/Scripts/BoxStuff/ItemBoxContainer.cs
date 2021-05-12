using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxContainer : BoxContainer<ItemBox, Item>
{
	private ItemBox box;
	protected override void OnAwake()
	{
		base.OnAwake();
		box = new ItemBox(BoxType.Type1); // TODO multiple types
	}

	public override ItemBox Box
	{
		get => box;
		protected set => box = value;
	}
}
