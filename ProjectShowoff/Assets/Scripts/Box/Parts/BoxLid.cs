using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class BoxLid<Contained> : MonoBehaviour
{
	// Callback that is called, when an object exits the lid
	public Action<BoxScript<Contained>> OnExitCallback;
	// Callback that is called, when an object enters the lid
	public Action<BoxScript<Contained>> OnEnterCallback;
	// A list of objects in the lid
	public List<BoxScript<Contained>> inLid { get; private set; } = new List<BoxScript<Contained>>();
	private List<BoxScript<Contained>> nextList = new List<BoxScript<Contained>>();

	private void OnTriggerStay(Collider other)
	{
		var comp = other.gameObject.GetComponent<BoxScript<Contained>>();
		if (comp) nextList.Add(comp);
	}

	private void FixedUpdate()
	{
		bool differing = inLid.Count != nextList.Count;
		if (!differing) differing = inLid.Except(nextList).Any();
		if (differing)
		{
			var old = inLid.Except(nextList).ToList();
			var n = nextList.Except(inLid).ToList();

			UpdateListsForNewFrame();

			// Call the On*Callbacks for each item that exited/entered
			foreach (BoxScript<Contained> b in n) OnEnterCallback(b);
			foreach (BoxScript<Contained> b in old) OnExitCallback(b);
		}
		else
			UpdateListsForNewFrame();
	}

	private void UpdateListsForNewFrame()
	{
		inLid.Clear();
		inLid = nextList;
		nextList = new List<BoxScript<Contained>>();
	}
}
