using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpringlessGrabber : CraneHook
{
	[SerializeField] private float maxDistance;
	[SerializeField] private AnimationCurve smoothing;
	[SerializeField] private float maxVelocity;
	[SerializeField] private float leniancy;

	[Header("Tweening")]
	[SerializeField] private float rotationTime = 0.5f;
	[SerializeField] private Ease easingMode = Ease.InOutElastic;
	private Rigidbody target;
	private RigidbodyConstraints oldTargetConstraints;
	private Tweener tweener;

	private Vector3 slowMoveVelocity = Vector3.zero;
	private bool shouldUnhook = false;

	public override void Hook(Rigidbody hooked)
	{
		// Another idea would be to disable the collider while carrying it
		// -> You wouldn't throw stuff around
		target = hooked;
		slowMoveVelocity = Vector3.zero;
		shouldUnhook = false;
		oldTargetConstraints = target.constraints;
		target.constraints = oldTargetConstraints | RigidbodyConstraints.FreezeRotation;
		tweener = target.DORotate(Vector3.zero, rotationTime).SetEase(easingMode);
	}

	public override bool Unhook()
	{
		// Only set a flag -> We still need the target in the next FixedUpdate
		shouldUnhook = true;
		// Return if we have a target
		return target;
	}

	protected override void OnAwake() { }

	private void FixedUpdate()
	{
		if (!target) return;
		Vector3 delta = transform.position - target.position;
		float dist = delta.magnitude;
		if (dist < leniancy)
		{
			Vector3 oldPos = target.position;
			target.MovePosition(transform.position);
			// Check how much the target has been moved to reach the grabber
			slowMoveVelocity = (target.position - oldPos);
			target.velocity = shouldUnhook
				// If we should unhook, we set the velocity to the last one we recorded
				? target.velocity = slowMoveVelocity * maxVelocity
				// ...otherwise we set it to zero
				: target.velocity = Vector3.zero;
		}
		else
		{
			slowMoveVelocity = Vector3.zero;
			float velocityMultiplier = smoothing.Evaluate(Mathf.Clamp(dist, 0, maxDistance));
			target.velocity = (delta).normalized * maxVelocity * velocityMultiplier;
		}
		// If the flag was set, we unhook the target finally.
		// After this iteration we don't need it anymore
		if (shouldUnhook)
			LateUnhook();
	}

	private void LateUnhook()
	{
		// Set target velocity.y to zero
		// Prevent stuff from flying
		Vector3 v = target.velocity;
		v.y = 0;
		target.velocity = v;
		// For now unhook in this method
		target.constraints = oldTargetConstraints;
		// For safety, abort the tween
		if (tweener.IsActive()) tweener.Kill();
		tweener = null;
		// Reset all the values
		target = null;
		shouldUnhook = false;
		slowMoveVelocity = Vector3.zero;
	}
}
