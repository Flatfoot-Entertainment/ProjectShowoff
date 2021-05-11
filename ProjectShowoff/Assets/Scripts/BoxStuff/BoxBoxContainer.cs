using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBoxContainer : BoxContainer<BoxBox, ItemBox, float>
{
	public override BoxBox Box => box;
	private BoxBox box;

	protected override void OnAwake()
	{
		base.OnAwake();
		box = new BoxBox();
	}
}
