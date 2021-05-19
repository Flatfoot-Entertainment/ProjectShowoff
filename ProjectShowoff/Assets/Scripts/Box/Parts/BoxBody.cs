using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BoxBody : MonoBehaviour
{
	public Action OnContentsUpdated;
	public List<GameObject> intersecting { get; private set; } = new List<GameObject>();
	public List<GameObject> nextList = new List<GameObject>();

	// private void OnTriggerEnter(Collider other)
	// {
	// 	// TODO specify layers and shit
	// 	OnContentsUpdated?.Invoke();
	// 	intersecting.Add(other.gameObject);
	// }

	private void OnTriggerStay(Collider other)
	{
		nextList.Add(other.gameObject);
	}

	private void FixedUpdate()
	{
		// Check if lists differ
		bool differing = intersecting.Count != nextList.Count;
		if (!differing)
		{
			differing = intersecting.Except(nextList).Any();
		}
		intersecting.Clear();
		intersecting = nextList;
		nextList = new List<GameObject>();
		if (differing)
		{
			OnContentsUpdated?.Invoke();
		}
	}

	// private void OnTriggerExit(Collider other)
	// {
	// 	Debug.Log("Exit");
	// 	OnContentsUpdated?.Invoke();
	// 	intersecting.Remove(other.gameObject);
	// }

	public bool Has(GameObject item)
	{
		return intersecting.Contains(item);
	}
}
