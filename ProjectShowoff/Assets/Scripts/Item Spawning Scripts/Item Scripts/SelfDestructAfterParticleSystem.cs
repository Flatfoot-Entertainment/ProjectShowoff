using UnityEngine;

public class SelfDestructAfterParticleSystem : MonoBehaviour
{
	private void OnParticleSystemStopped()
	{
		Destroy(gameObject);
	}
}
