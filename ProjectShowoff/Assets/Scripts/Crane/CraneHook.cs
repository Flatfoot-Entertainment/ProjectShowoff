using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class CraneHook : MonoBehaviour
{
	// TODO why is this a Rigidbody? => Nothing uses it as a Rigidbody
	private Rigidbody rb;

	protected abstract void OnAwake();

	private void Awake()
	{
		OnAwake();
		rb = GetComponent<Rigidbody>();
	}

	/**
	 * Hook something to this Grabber
	 */
	public abstract void Hook(Rigidbody hooked);

	/**
	 * Unhook the currently hooked object.
	 * returns Returns, if something was unhooked, so if this method did something.
	 */
	public abstract bool Unhook();

	/**
	 * Move the hook using the underlying Rigidbody
	 */
	public void MovePosition(Vector3 pos)
	{
		rb.MovePosition(pos);
	}
}
