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
	private Sequence sequence;

	// TODO variable speed, etc.

	private bool delivered = false;

	public void DeliverTo(Planet planet)
	{
		target = planet;
		startPos = transform.position;
		sequence = DOTween.Sequence();
		sequence.Append(transform.DOLookAt(target.transform.position, 1f));
		sequence.Append(transform.DOMove(target.transform.position, 5f));
	}

	// I somehow really don't want the planets to be triggers
	private void OnTriggerEnter(Collider other)
	{
		if (delivered || other.gameObject != target.gameObject) return;
		delivered = true;
		// Ship has been delivered
		target.Deliver(box);
		box = null;
		if (sequence.IsActive()) sequence.Kill();
		StartCoroutine(Return());
	}

	private IEnumerator Return()
	{
		sequence = DOTween.Sequence();
		sequence.Append(transform.DOLookAt(startPos, 1f));
		sequence.Append(transform.DOMove(startPos, 5f));
		yield return new WaitWhile(() => { return sequence.IsActive() && sequence.IsPlaying(); });
		OnArrival?.Invoke(this);
	}
}
