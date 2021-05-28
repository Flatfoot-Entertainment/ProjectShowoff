using UnityEngine;

public class SelfDestructAfter : MonoBehaviour
{
	[SerializeField] private float time;

	private void Start()
	{
		Destroy(gameObject, time);
	}
}
