using UnityEngine;

public class ItemPropertyHandler : MonoBehaviour
{
	public GameObject replacedBy;
	public float maxForce;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.relativeVelocity.magnitude < maxForce) return;
		Lean.Pool.LeanPool.Despawn(gameObject);
		Lean.Pool.LeanPool.Spawn(replacedBy, transform.position, transform.rotation);
		OnDestruction();
	}
	
	protected virtual void OnDestruction() { }
}
