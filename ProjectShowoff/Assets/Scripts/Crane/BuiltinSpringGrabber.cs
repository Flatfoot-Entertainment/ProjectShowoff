using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A CraneHook using the builting SpringJoint component.
 */
[RequireComponent(typeof(SpringJoint))]
public class BuiltinSpringGrabber : CraneHook
{
	private SpringJoint joint;

	public override void Hook(Rigidbody hooked)
	{
		joint.connectedBody = hooked;
	}

	public override bool Unhook()
	{
		if (joint.connectedBody)
		{
			joint.connectedBody = null;
			return true;
		}
		return false;
	}

	protected override void OnAwake()
	{
		joint = GetComponent<SpringJoint>();
	}
}
