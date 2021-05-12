using UnityEngine;

public class ShippableContainer : ShippableBox<ContainerData, ItemBoxData>
{
	[SerializeField] private LayerMask deliveringCollisionMask;

	private void OnCollisionEnter(Collision other)
	{
		if (InMask(other.gameObject.layer))
		{
			Deliver();
		}
	}

	private bool InMask(int layer)
	{
		return (deliveringCollisionMask == (deliveringCollisionMask | (1 << layer)));
	}
}