using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BoxLid<Contained> : MonoBehaviour
{
	public Action<PropertyHolder<Contained>> OnExitCallback;
	public Action<PropertyHolder<Contained>> OnEnterCallback;

	private void OnTriggerExit(Collider other)
	{
		var comp = other.gameObject.GetComponent<PropertyHolder<Contained>>();
		if (comp) OnExitCallback?.Invoke(comp);
	}

	private void OnTriggerEnter(Collider other)
	{
		var comp = other.gameObject.GetComponent<PropertyHolder<Contained>>();
		if (comp) OnEnterCallback?.Invoke(comp);
	}
}
