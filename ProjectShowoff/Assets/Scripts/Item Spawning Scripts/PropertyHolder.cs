using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//item script to show debug info in the inspector
[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public abstract class PropertyHolder<Contained> : MonoBehaviour
{
	public abstract Contained contained { get; set; }

	private void Start()
	{
		OnStart();
	}

	protected virtual void OnStart() { }
}
