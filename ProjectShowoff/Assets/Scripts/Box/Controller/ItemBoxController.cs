using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxController : BoxController<ItemBoxData, Item>
{
	private ItemBoxData box;
	protected override void OnAwake()
	{
		base.OnAwake();
		box = new ItemBoxData(BoxType.Type1); // TODO multiple types
	}

	public override ItemBoxData Box
	{
		get => box;
		protected set => box = value;
	}
}
