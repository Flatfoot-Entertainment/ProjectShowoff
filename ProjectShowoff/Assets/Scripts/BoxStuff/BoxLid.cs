using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BoxLid : MonoBehaviour
{
	public Action<ItemScript> OnExitCallback;
	public Action<ItemScript> OnEnterCallback;

	private void OnTriggerExit(Collider other)
	{
		OnExitCallback?.Invoke(other.gameObject.GetComponent<ItemScript>());
	}

	private void OnTriggerEnter(Collider other)
	{
		OnEnterCallback?.Invoke(other.gameObject.GetComponent<ItemScript>());
	}
}
