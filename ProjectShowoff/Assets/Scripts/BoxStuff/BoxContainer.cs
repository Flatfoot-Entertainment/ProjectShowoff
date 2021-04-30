using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContainer : MonoBehaviour
{
	private List<GameObject> containing = new List<GameObject>();
	private BoxLid lid;
	private BoxBody body;

	private void Awake()
	{
		lid = GetComponentInChildren<BoxLid>();
		body = GetComponentInChildren<BoxBody>();
	}

	private void Start()
	{
		lid.OnExitCallback += LidExit;
		lid.OnEnterCallback += LidEnter;
	}

	private void OnDestroy()
	{
		lid.OnExitCallback -= LidExit;
		lid.OnEnterCallback -= LidEnter;
	}

	private void LidExit(GameObject subject)
	{
		// If the thing exiting the lid is in the body, it is fully in the box
		if (body.Has(subject))
		{
			Debug.Log($"{subject.name} is now fully in the box");
			containing.Add(subject);
		}
	}

	// If something intersects with the Lid, it is not completely in the box anymore
	private void LidEnter(GameObject subject)
	{
		if (containing.Remove(subject))
		{
			Debug.Log($"{subject.name} has left the box");
		}
	}
}
