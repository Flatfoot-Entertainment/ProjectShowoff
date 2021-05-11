using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippableBox : MonoBehaviour
{
	[SerializeField] private LayerMask deliveringCollisionMask;
	private Box box;
	private bool delivered;

	public void Init(Vector3 dimentions, Box box)
	{
		delivered = false;
		this.box = box;
		transform.localScale = dimentions;
	}

	private void OnCollisionEnter(Collision other)
	{
		if (!delivered && InMask(other.gameObject.layer))
		{
			BoxContainer.Deliver(box.GetBoxContentsValue());
			delivered = true;
		}
	}

	private void OnDestroy()
	{
		if (!delivered)
		{
			BoxContainer.Deliver(box.GetBoxContentsValue());
			delivered = true;
		}
	}

	private bool InMask(int layer)
	{
		return (deliveringCollisionMask == (deliveringCollisionMask | (1 << layer)));
	}
}
