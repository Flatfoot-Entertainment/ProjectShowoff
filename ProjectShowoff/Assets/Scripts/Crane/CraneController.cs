using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneController : MonoBehaviour
{
	[SerializeField] private CraneHook hook;
	[SerializeField] private LayerMask hookableMask;
	[SerializeField] private float craneHeight;
	private Vector3 desiredPosition = Vector3.zero;

	private Plane cranePlane;

	// Start is called before the first frame update
	void Start()
	{
		cranePlane = new Plane(Vector3.up, new Vector3(0, craneHeight, 0));
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			// Update hook position
			Vector3 mousePos = Input.mousePosition;
			Ray ray = Camera.main.ScreenPointToRay(mousePos);
			if (cranePlane.Raycast(ray, out float dist))
			{
				desiredPosition = ray.GetPoint(dist);
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			// TODO set desired pos here at least once
			// Get target
			hook.Hook(GetTarget(Input.mousePosition));
		}
		if (Input.GetMouseButtonUp(0))
		{
			hook.Unhook();
		}

	}

	private Rigidbody GetTarget(Vector3 mousePos)
	{
		// TODO maybe in the future cache Camera.main -> not the fastest
		Ray ray = Camera.main.ScreenPointToRay(mousePos);
		if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, hookableMask))
		{
			return hit.rigidbody;
		}
		return null;
	}

	private void FixedUpdate()
	{
		hook.MovePosition(desiredPosition);
	}
}
