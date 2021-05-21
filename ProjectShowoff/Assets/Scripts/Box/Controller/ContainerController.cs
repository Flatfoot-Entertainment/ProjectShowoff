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

	protected override void OnObjectAdded()
	{
		base.OnObjectAdded();
	}

	protected override void OnObjectRemoved()
	{
		base.OnObjectRemoved();
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public override bool Equals(object other)
	{
		return base.Equals(other);
	}

	public override string ToString()
	{
		return base.ToString();
	}

	protected override void PurgeContents()
	{
		foreach (GameObject gO in contained) Destroy(gO);
	}
}
