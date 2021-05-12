using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerController : BoxController<ContainerData, ItemBoxData>
{
	public override ContainerData Box
	{
		get => box;
		protected set => box = value;
	}
	private ContainerData box;

	protected override void OnAwake()
	{
		base.OnAwake();
		box = new ContainerData();
	}
}
