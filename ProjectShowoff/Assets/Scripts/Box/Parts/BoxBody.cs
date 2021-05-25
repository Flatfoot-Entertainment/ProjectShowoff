using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BoxBody : MonoBehaviour
{
	// A callback to get notified when the contents in the box change
	public Action OnContentsUpdated;

	// The list of objects intersecting with the body
	public List<GameObject> intersecting { get; private set; } = new List<GameObject>();

	private List<GameObject> nextList = new List<GameObject>();

	private void OnTriggerStay(Collider other)
	{
		nextList.Add(other.gameObject);
	}

	private void FixedUpdate()
	{
		// Check if lists differ
		// Either the count is different or their intersecting set is non-empty
		bool differing = intersecting.Count != nextList.Count;
		if (!differing) differing = intersecting.Except(nextList).Any();

		// Reset the lists
		intersecting.Clear();
		intersecting = nextList;
		nextList = new List<GameObject>();

		if (differing) OnContentsUpdated?.Invoke();
	}

	public bool Has(GameObject item)
	{
		return intersecting.Contains(item);
	}
}
