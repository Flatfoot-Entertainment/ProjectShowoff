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
	[SerializeField] private float rotationResetTime = 0.5f;
	[SerializeField] private float rotationTime = 0.3f;
	[SerializeField] private Ease rotationResetEasingMode = Ease.InOutBounce;
	[SerializeField] private Ease rotationEasingMode = Ease.InOutBounce;
	private Rigidbody target;
	private RigidbodyConstraints oldTargetConstraints;
	private List<Tweener> tweeners = new List<Tweener>();

	private Vector3 slowMoveVelocity = Vector3.zero;
	private bool shouldUnhook = false;

	private Quaternion lastRot;

	private Vector3 targetOldPos = Vector3.zero;
	private Vector3 targetMovement = Vector3.zero;

	public override void Hook(Rigidbody hooked)
	{
		targetOldPos = hooked.position;
		lastRot = Quaternion.identity;
		// Another idea would be to disable the collider while carrying it
		// -> You wouldn't throw stuff around
		target = hooked;
		target.detectCollisions = false;
		target.isKinematic = true;
		slowMoveVelocity = Vector3.zero;
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

	private void CleanTweeners()
	{

	}

	protected override void OnAwake() { }

	private void OnDrawGizmos()
	{
		if (!target) return;
		Gizmos.color = new Color(1.0f, 0, 0, 0.5f);
		Gizmos.DrawSphere(transform.position, 1.0f);
		Gizmos.color = new Color(0, 1.0f, 0, 0.5f);
		Gizmos.DrawSphere(targetOldPos, 1.0f);
	}

	private void FixedUpdate()
	{
		if (!target) return;
		Debug.Log($"Position: {target.position}");
		Debug.Log($"Old Position: {targetOldPos}");
		targetOldPos = target.position;
		target.position = Vector3.Lerp(target.position, transform.position, 0.9f);
		targetMovement = target.position - targetOldPos;
		// targetMovement = Vector3.Lerp(targetMovement, newtargetMovement, maxVelocity * Time.fixedDeltaTime);

		// Vector3 delta = transform.position - target.position;
		// float dist = delta.magnitude;
		// if (dist < leniancy)
		// {
		// 	Vector3 oldPos = target.position;
		// 	target.MovePosition(transform.position);
		// 	// Check how much the target has been moved to reach the grabber
		// 	slowMoveVelocity = (target.position - oldPos);
		// 	target.velocity = shouldUnhook
		// 		// If we should unhook, we set the velocity to the last one we recorded
		// 		? target.velocity = slowMoveVelocity * maxVelocity
		// 		// ...otherwise we set it to zero
		// 		: target.velocity = Vector3.zero;
		// }
		// else
		// {
		// 	slowMoveVelocity = Vector3.zero;
		// 	float velocityMultiplier = smoothing.Evaluate(Mathf.Clamp(dist, 0, maxDistance));
		// 	target.velocity = (delta).normalized * maxVelocity * velocityMultiplier;
		// }
		// If the flag was set, we unhook the target finally.
		// After this iteration we don't need it anymore
		// Debug.Log("1111111111111111111111111111111");
		// Debug.Log($"Target old pos: {targetOldPos}");
		// Debug.Log($"Target: {target.name}");
		// Debug.Log($"Target position: {target.position}");

		if (shouldUnhook)
		{
			target.detectCollisions = true;
			target.isKinematic = false;
			Debug.Log($"Target movement: {targetMovement * maxVelocity}");
			target.velocity = targetMovement * maxVelocity;
			LateUnhook();
		}
	}

	private void LateUnhook()
	{
		target.constraints = oldTargetConstraints;
		// Set target velocity.y to zero
		// Prevent stuff from flying
		// TODO weird
		Vector3 v = target.velocity;
		v.y = 0;

		// target.velocity = Vector3.zero;
		// For now unhook in this method
		// For safety, abort all tweens
		target.DOKill();
		// Reset all the values
		target = null;
		shouldUnhook = false;
		slowMoveVelocity = Vector3.zero;
	}
}
