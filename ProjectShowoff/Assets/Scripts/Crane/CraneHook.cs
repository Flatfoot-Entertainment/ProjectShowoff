using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CraneHook : MonoBehaviour
{
	protected abstract void OnAwake();

	private void Awake()
	{
		OnAwake();
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
		transform.position = pos;
		// rb.MovePosition(pos);
	}
}
