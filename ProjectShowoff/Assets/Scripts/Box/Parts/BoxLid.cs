using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class BoxLid<Contained> : MonoBehaviour
{
	public Action<BoxScript<Contained>> OnExitCallback;
	public Action<BoxScript<Contained>> OnEnterCallback;
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
		if (!differing)
		{
			differing = inLid.Except(nextList).Any();
		}
		if (differing)
		{
			var old = inLid.Except(nextList).ToList();
			var n = nextList.Except(inLid).ToList();
			inLid.Clear();
			inLid = nextList;
			nextList = new List<BoxScript<Contained>>();
			foreach (BoxScript<Contained> b in n) OnEnterCallback(b);
			foreach (BoxScript<Contained> b in old) OnExitCallback(b);
		}
		else
		{
			inLid.Clear();
			inLid = nextList;
			nextList = new List<BoxScript<Contained>>();
		}
	}

	// private void OnTriggerExit(Collider other)
	// {
	// 	var comp = other.gameObject.GetComponent<BoxScript<Contained>>();
	// 	if (comp)
	// 	{
	// 		OnExitCallback?.Invoke(comp);
	// 		inLid.Remove(other.gameObject);
	// 	}
	// }

	// private void OnTriggerEnter(Collider other)
	// {
	// 	var comp = other.gameObject.GetComponent<BoxScript<Contained>>();
	// 	if (comp)
	// 	{
	// 		OnEnterCallback?.Invoke(comp);
	// 		inLid.Add(other.gameObject);
	// 	}
	// }
}
