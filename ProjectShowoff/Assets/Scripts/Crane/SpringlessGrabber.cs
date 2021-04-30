using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringlessGrabber : CraneHook
{
	[SerializeField] private float maxDistance;
	[SerializeField] private AnimationCurve smoothing;
	[SerializeField] private float maxVelocity;
	[SerializeField] private float leniancy;
	private Rigidbody target;

	public override void Hook(Rigidbody hooked)
	{
		target = hooked;
	}

	public override bool Unhook()
	{
		if (target)
		{
			target = null;
			return true;
		}
		return false;
	}

	protected override void OnAwake() { }

	private void FixedUpdate()
	{
		if (!target) return;
		Vector3 delta = transform.position - target.position;
		float dist = delta.magnitude;
		if (dist < leniancy)
		{
			target.velocity = Vector3.zero;
			target.MovePosition(transform.position);
		}
		else
		{
			float smoothingFactor = Mathf.Clamp(dist, 0, maxDistance);
			float lookup = smoothing.Evaluate(smoothingFactor);
			// Somehow make it not constant speed -> some smoothing
			target.velocity = (transform.position - target.position).normalized * maxVelocity * lookup;
		}
	}
}
