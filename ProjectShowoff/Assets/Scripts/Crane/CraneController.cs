using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CraneController : MonoBehaviour
{
	[SerializeField, Tooltip("The hook of this crane.")]
	private CraneHook hook;
	[SerializeField, Tooltip("Which objects (by LayerMask) should be able to be hooked")]
	private LayerMask hookableMask;
	[SerializeField, Tooltip("The height of this train")]
	private float craneHeight;
	private Vector3 desiredPosition = Vector3.zero;

	private Plane cranePlane;

	private void Awake()
	{
#if UNITY_EDITOR
		DOTween.Init(false, true, LogBehaviour.Verbose);
#else
		DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
#endif
	}

	void Start()
	{
		cranePlane = new Plane(Vector3.up, new Vector3(0, craneHeight, 0));
		Vector3 p = hook.transform.position;
		p.y = craneHeight;
		hook.transform.position = p;
	}

	private void UpdateDesiredPosition()
	{
		Vector3 mousePos = Input.mousePosition;
		Ray ray = Camera.main.ScreenPointToRay(mousePos);
		if (cranePlane.Raycast(ray, out float dist))
		{
			desiredPosition = ray.GetPoint(dist);
		}
	}

	void Update()
	{
		if (Input.GetMouseButton(0))
			UpdateDesiredPosition();

		if (Input.GetMouseButtonDown(0))
			hook.Hook(GetTarget(Input.mousePosition));

		if (Input.GetMouseButtonUp(0))
			hook.Unhook();
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
