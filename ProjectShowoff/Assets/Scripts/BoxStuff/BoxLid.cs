using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BoxLid : MonoBehaviour
{
	public Action<GameObject> OnExitCallback;
	public Action<GameObject> OnEnterCallback;

	private void OnTriggerExit(Collider other)
	{
		OnExitCallback?.Invoke(other.gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		OnEnterCallback?.Invoke(other.gameObject);
	}
}
