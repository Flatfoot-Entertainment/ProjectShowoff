using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BoxLid<Contained> : MonoBehaviour
{
	public Action<BoxScript<Contained>> OnExitCallback;
	public Action<BoxScript<Contained>> OnEnterCallback;

	private void OnTriggerExit(Collider other)
	{
		var comp = other.gameObject.GetComponent<BoxScript<Contained>>();
		if (comp) OnExitCallback?.Invoke(comp);
	}

	private void OnTriggerEnter(Collider other)
	{
		var comp = other.gameObject.GetComponent<BoxScript<Contained>>();
		if (comp) OnEnterCallback?.Invoke(comp);
	}
}
