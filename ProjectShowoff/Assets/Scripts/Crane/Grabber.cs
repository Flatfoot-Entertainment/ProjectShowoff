using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BetterSpring))]
public class Grabber : CraneHook
{
	private BetterSpring spring;

	public override void Hook(Rigidbody hooked)
	{
		spring.Target = hooked;
	}

	public override bool Unhook()
	{
		if (spring.Target)
		{
			spring.Target = null;
			return true;
		}
		return false;
	}

	protected override void OnAwake()
	{
		spring = GetComponent<BetterSpring>();
	}
}
