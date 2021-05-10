using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBody : MonoBehaviour
{
	private List<GameObject> intersecting = new List<GameObject>();

	private void OnTriggerEnter(Collider other)
	{
		// TODO specify layers and shit
		intersecting.Add(other.gameObject);
	}

	private void OnTriggerExit(Collider other)
	{
		intersecting.Remove(other.gameObject);
	}

	public bool Has(GameObject item)
	{
		return intersecting.Contains(item);
	}
}
