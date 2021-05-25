using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CraneController : MonoBehaviour
{
	[SerializeField, Tooltip("The hook of this crane.")]
	private SpringlessGrabber hook;

	[SerializeField, Tooltip("Which objects (by LayerMask) should be able to be hooked")]
	private LayerMask hookableMask;

	[SerializeField, Tooltip("The height of this train")]
	private float craneHeight;

	private Vector3 desiredPosition = Vector3.zero;

	private Plane cranePlane;

	private Camera mainCam;

	private void Awake()
	{
		mainCam = Camera.main;
	}

	void Start()
	{
		cranePlane = new Plane(Vector3.up, new Vector3(0, craneHeight, 0));
		hook.transform.position.SetY(craneHeight);
	}

	private void UpdateDesiredPosition()
	{
		// We don't have a camera (for some reason) or the camera is disabled
		if (!mainCam || !mainCam.isActiveAndEnabled) return;
		Vector3 mousePos = Input.mousePosition;
		Ray ray = mainCam.ScreenPointToRay(mousePos);
		if (cranePlane.Raycast(ray, out float dist)) desiredPosition = ray.GetPoint(dist);
	}

	void Update()
	{
		if (Input.GetMouseButton(0))
			UpdateDesiredPosition();

		if (Input.GetMouseButtonDown(0))
		{
			Rigidbody target = GetTarget(Input.mousePosition);
			if (target) hook.Hook(target);
		}

		if (Input.GetMouseButtonUp(0))
			hook.Unhook();
	}

	private Rigidbody GetTarget(Vector3 mousePos)
	{
		// We don't have a camera (for some reason) or the camera is disabled
		if (!mainCam || !mainCam.isActiveAndEnabled) return null;
		Ray ray = mainCam.ScreenPointToRay(mousePos);
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
