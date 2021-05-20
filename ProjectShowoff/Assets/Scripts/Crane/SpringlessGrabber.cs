using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpringlessGrabber : CraneHook
{
	[Header("Physics interaction")]
	[Tooltip("A multiplier of the force after letting go of an object"), SerializeField]
	private float throwSpeed = 50f;
	[Tooltip("How fast the object goes towards the hook location.\n" +
		"The higher this value, the faster"),
	SerializeField]
	private float smoothing = 10f;
	// The thing being hooked
	private Rigidbody target;
	// The constraints of the target rigidbody the moment it was hooked
	private RigidbodyConstraints oldTargetConstraints;
	// Flag to set, if the hook should unhook at the end of the next FixedUpdate call
	private bool shouldUnhook = false;

	[Header("Tweening")]
	[Tooltip("The time it takes for the objects rotation to reset after picking it up"), SerializeField]
	private float rotationResetTime = 0.5f;
	[Tooltip("The easing mode to use for the initial pickup"), SerializeField]
	private Ease rotationResetEasingMode = Ease.InOutBounce;
	[Tooltip("The rotation time for rotating a picked up object"), SerializeField]
	private float rotationTime = 0.3f;
	[Tooltip("The easing mode for rotation a picked up object"), SerializeField]
	private Ease rotationEasingMode = Ease.InOutBounce;
	// The last rotation used for tweening
	// Used for calculating the next rotation
	private Quaternion lastRot;

	public override void Hook(Rigidbody hooked)
	{
		target = hooked;
		target.isKinematic = true;
		target.detectCollisions = false;
		lastRot = Quaternion.identity;
		shouldUnhook = false;
		oldTargetConstraints = target.constraints;
		target.constraints = oldTargetConstraints | RigidbodyConstraints.FreezeRotation;
		target.DORotate(Vector3.zero, rotationResetTime).SetEase(rotationResetEasingMode);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			lastRot = Quaternion.Euler(0f, 90f, 0f) * lastRot;
			target.DORotate(
				lastRot.eulerAngles, rotationTime
			).SetEase(rotationEasingMode);
		}
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

		Vector3 targetOldPos = target.position;
		target.position = Vector3.Lerp(target.position, transform.position, smoothing * Time.fixedDeltaTime);
		Vector3 targetMovement = target.position - targetOldPos;

		if (shouldUnhook)
		{
			target.velocity = targetMovement * throwSpeed;
			LateUnhook();
		}
	}

	private void LateUnhook()
	{
		target.isKinematic = false;
		target.detectCollisions = true;
		// Reset constraints to the old ones (previously rotation was disallowed)
		target.constraints = oldTargetConstraints;

		// For safety, abort all tweens
		target.DOKill();

		// Reset all the values
		target = null;
		shouldUnhook = false;
	}
}
