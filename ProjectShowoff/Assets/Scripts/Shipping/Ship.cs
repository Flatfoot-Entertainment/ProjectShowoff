using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ship : MonoBehaviour
{
	public event System.Action<Ship> OnArrival;
	public ContainerData box { get; set; }
	private Planet target;
	private Vector3 startPos;
	private Tweener tweener;

	// TODO variable speed, etc.

	public void DeliverTo(Planet planet)
	{
		target = planet;
		startPos = transform.position;
		tweener = transform.DOMove(target.transform.position, 5f);
	}

	// I somehow really don't want the planets to be triggers
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log($"Collision with {other.gameObject.name}");
		if (other.gameObject != target.gameObject) return;
		// Ship has been delivered
		target.Deliver(box);
		box = null;
		if (tweener.IsActive()) tweener.Kill();
		StartCoroutine(Return());
	}

	private IEnumerator Return()
	{
		// TODO rotate as well
		yield return new WaitForSeconds(1f);
		tweener = transform.DOMove(startPos, 5f);
		yield return new WaitWhile(() => { return tweener.IsActive() && tweener.IsPlaying(); });
		OnArrival?.Invoke(this);
	}
}
