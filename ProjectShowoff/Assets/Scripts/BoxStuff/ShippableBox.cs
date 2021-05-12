using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippableBox<BoxT, Contained> : MonoBehaviour where BoxT : IBox<Contained>
{
	[SerializeField] private LayerMask deliveringCollisionMask;
	private BoxT box;
	private bool delivered;

	public void Init(Vector3 dimentions, BoxT box)
	{
		// TODO add BoxScript to this gameobject with the correct box in it
		delivered = false;
		this.box = box;
		transform.localScale = dimentions;
	}

	private void OnCollisionEnter(Collision other)
	{
		if (!delivered && InMask(other.gameObject.layer))
		{
			BoxContainer<BoxT, Contained>.Deliver(box.MoneyValue);
			delivered = true;
		}
	}

	private void OnDestroy()
	{
		if (!delivered)
		{
			BoxContainer<BoxT, Contained>.Deliver(box.MoneyValue);
			delivered = true;
		}
	}

	private bool InMask(int layer)
	{
		return (deliveringCollisionMask == (deliveringCollisionMask | (1 << layer)));
	}
}
