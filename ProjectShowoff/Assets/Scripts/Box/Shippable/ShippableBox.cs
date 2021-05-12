using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippableBox<BoxT, Contained> : MonoBehaviour where BoxT : IBoxData<Contained>
{
	[SerializeField] private LayerMask deliveringCollisionMask;
	public BoxT Box => box;
	private BoxT box;
	private bool delivered;

	protected virtual void OnInit() { }

	public void Init(Vector3 dimentions, BoxT box)
	{
		delivered = false;
		this.box = box;
		transform.localScale = dimentions;
		OnInit();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (!delivered && InMask(other.gameObject.layer))
		{
			BoxController<BoxT, Contained>.Deliver(box.MoneyValue);
			delivered = true;
		}
	}

	private void OnDestroy()
	{
		if (!delivered)
		{
			BoxController<BoxT, Contained>.Deliver(box.MoneyValue);
			delivered = true;
		}
	}

	private bool InMask(int layer)
	{
		return (deliveringCollisionMask == (deliveringCollisionMask | (1 << layer)));
	}
}
