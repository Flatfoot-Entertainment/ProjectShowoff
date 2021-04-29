using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * An alternative to the SpringJoint provided by Unity.
 * It uses lerping under the hood, so it is not at all physically accurate.
 */
public class BetterSpring : MonoBehaviour
{
	private Rigidbody target;
	/**
	 * What should the spring be connected to?
	 */
	public Rigidbody Target
	{
		get => target;
		set
		{
			// Before overriding, set the velocity to the last velocity
			if (target)
			{
				target.useGravity = true;
				target.velocity = lastVelocity;
			}
			target = value;
			if (target)
				target.useGravity = false;
			lastVelocity = Vector3.zero;
		}
	}

	/**
	 * The speed the target is moved towards the spring origin.
	 * There is no specific unit, so just estimate. The higher, the faster...
	 */
	[SerializeField] private float speed;
	/**
	 * At what distance should the target be stopped moving
	 */
	[SerializeField] private float cutoff;

	private Vector3 lastVelocity = Vector3.zero;

	void FixedUpdate()
	{
		// For safety => target will often be null
		if (Target == null) return;
		Vector3 oldPos = Target.position;
		// Respect the cutoff...
		if (Vector3.Distance(Target.position, transform.position) >= cutoff)
		{
			// Move the target using Rigidbody.MovePosition() and Vector3.Lerp()
			Target.MovePosition(
				Vector3.Lerp(Target.position, transform.position, speed * Time.fixedDeltaTime)
			);
		}
		// Calculate the old velocity by using the old position and extracting out
		// the fixed delta time again
		lastVelocity = (Target.position - oldPos) / Time.fixedDeltaTime;
	}
}
