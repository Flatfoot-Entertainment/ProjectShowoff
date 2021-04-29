using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterSpring : MonoBehaviour
{
	private Rigidbody target;
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

	[SerializeField] private float speed;
	[SerializeField] private float cutoff;

	private Vector3 lastVelocity = Vector3.zero;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (Target == null) return;
		Vector3 oldPos = Target.position;
		if (Vector3.Distance(Target.position, transform.position) >= cutoff)
		{
			Target.MovePosition(
				Vector3.Lerp(Target.position, transform.position, speed * Time.fixedDeltaTime)
			);
		}
		lastVelocity = (Target.position - oldPos) / Time.fixedDeltaTime;
	}
}
