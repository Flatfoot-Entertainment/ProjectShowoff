using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleConveyor : MonoBehaviour
{
	public float Speed
	{
		get => speed;
		set => speed = value;
	}

	public float InitialSpeed
	{
		get => initialSpeed;
		set => initialSpeed = value;
	}

	[SerializeField] private float speed;
	[SerializeField] private float initialSpeed = 2.0f;
	[SerializeField] private Vector3 moveDir;
	private Rigidbody rb;


	// Start is called before the first frame update
	private void Start()
	{
		speed = initialSpeed;
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		Vector3 oldPos = rb.position;
		rb.position += (-moveDir) * speed * Time.fixedDeltaTime;
		rb.MovePosition(oldPos);
	}

	public IEnumerator Stop(float delay)
	{
		speed = 0f;
		yield return new WaitForSeconds(delay);
		speed = initialSpeed;
	}
}
