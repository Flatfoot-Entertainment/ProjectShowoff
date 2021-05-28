using UnityEngine;

public class ExplosiveItemPropertyHandler : ItemPropertyHandler
{
	public float radius;
	public float force;
	/// <inheritdoc />
	protected override void OnDestruction()
	{
		// TODO is OverlapSphereNonAlloc faster?
		var colliders = Physics.OverlapSphere(transform.position, radius);
		foreach (Collider coll in colliders)
		{
			var rb = coll.GetComponent<Rigidbody>();
			if (rb)
			{
				rb.AddExplosionForce(force, transform.position, radius);
			}
		}
		base.OnDestruction();
	}
}
