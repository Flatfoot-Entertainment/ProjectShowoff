using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBoxContainer : BoxContainer<BoxBox, ItemBox>
{
	public override BoxBox Box
	{
		get => box;
		protected set => box = value;
	}
	private BoxBox box;

	protected override void OnAwake()
	{
		base.OnAwake();
		box = new BoxBox();
	}
}
