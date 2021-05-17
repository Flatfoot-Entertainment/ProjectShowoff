using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionTrigger : MonoBehaviour
{
	[SerializeField] private LayerMask destructionLayer;

	private void OnTriggerEnter(Collider other)
	{
		if (InMask(other.gameObject.layer))
		{
			Destroy(other.gameObject);
		}
	}

	private bool InMask(int layer)
	{
		return (destructionLayer == (destructionLayer | (1 << layer)));
	}
}
