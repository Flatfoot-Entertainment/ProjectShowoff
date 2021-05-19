using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanPoolDestructionTrigger : MonoBehaviour
{
	[SerializeField] private LayerMask destructionLayer;

	private void OnTriggerEnter(Collider other)
	{
		if (destructionLayer.Contains(other.gameObject.layer))
		{
			Lean.Pool.LeanPool.Despawn(other.gameObject);
		}
	}
}
