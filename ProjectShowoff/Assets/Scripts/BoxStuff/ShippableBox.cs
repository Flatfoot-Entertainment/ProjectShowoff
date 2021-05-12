using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippableBox : MonoBehaviour
{
	public Box Box => box;
	[SerializeField] private LayerMask deliveringCollisionMask;
	private Box box;
	private bool delivered;

	public void Init(Vector3 dimentions, Box box)
	{
		delivered = false;
		this.box = box;
		transform.localScale = dimentions;
		EventScript.Instance.EventQueue.AddEvent(new ManageMoneyEvent(box.GetBoxContentsValue()));
	}

    private void OnCollisionEnter(Collision other)
	{
		if (!delivered && InMask(other.gameObject.layer))
		{
			//BoxContainer.Deliver(box.GetBoxContentsValue());
			EventScript.Instance.EventQueue.Update();
			delivered = true;
		}
	}

	private void OnDestroy()
	{
		if (!delivered)
		{
			//BoxContainer.Deliver(box.GetBoxContentsValue());
			EventScript.Instance.EventQueue.Update();
			delivered = true;
		}
	}

	private bool InMask(int layer)
	{
		return (deliveringCollisionMask == (deliveringCollisionMask | (1 << layer)));
	}
}
