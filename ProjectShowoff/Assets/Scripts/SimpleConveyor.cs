using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleConveyor : MonoBehaviour
{
	[SerializeField] private float speed;
	private Rigidbody rb;


	// Start is called before the first frame update
	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		Vector3 oldPos = rb.position;
		rb.position += (-transform.forward) * speed * Time.fixedDeltaTime;
		rb.MovePosition(oldPos);
	}
}
