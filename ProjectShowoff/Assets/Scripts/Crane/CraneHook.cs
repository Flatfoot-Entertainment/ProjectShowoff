using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class CraneHook : MonoBehaviour
{
	private Rigidbody rb;

	protected abstract void OnAwake();

	private void Awake()
	{
		OnAwake();
		rb = GetComponent<Rigidbody>();
	}

	public abstract void Hook(Rigidbody hooked);

	public abstract bool Unhook();

	public void MovePosition(Vector3 pos)
	{
		rb.MovePosition(pos);
	}
}
