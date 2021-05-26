using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(ShipLoadingHandler))]
public class Ship : MonoBehaviour
{
	[HideInInspector]
	public event System.Action OnArrival;
	public ContainerData box { get; set; }
	private Planet target;
	private Vector3 startPos;
	private Vector3 startRotation;
	private Vector3 startScale;
	private Sequence sequence;

	[SerializeField] private float travelTime;
	[SerializeField] private Ease travelEase;
	[SerializeField] private float turnTime;
	[SerializeField] private Ease turnEase;
	[SerializeField] private float scaleTime;
	[SerializeField] private Ease scaleOutEase;
	[SerializeField] private Ease scaleInEase;
	[SerializeField] private float scaleFactor;

	private bool delivered = false;

	private void Start()
	{
		startPos = transform.position;
		startRotation = transform.rotation.eulerAngles;
		startScale = transform.localScale;
	}

	public void DeliverTo(Planet planet)
	{
		target = planet;
		sequence = DOTween.Sequence();
		sequence.Append(transform.DOLookAt(target.transform.position, turnTime).SetEase(turnEase));
		sequence.Insert(0f, (transform.DOMove(target.transform.position, travelTime).SetEase(travelEase)));
		sequence.Insert(travelTime - scaleTime, transform.DOScale(startScale * scaleFactor, scaleTime).SetEase(scaleOutEase));
	}

	// I somehow really don't want the planets to be triggers
	// TODO check for distance from planet(more expensive)/switch to colliders?
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
		sequence.Append(transform.DOLookAt(startPos, turnTime).SetEase(turnEase));
		sequence.Append(transform.DOMove(startPos, travelTime).SetEase(travelEase));
		sequence.Insert(turnTime, transform.DOScale(startScale, scaleTime).SetEase(scaleInEase));
		sequence.Insert(travelTime, transform.DORotate(startRotation, turnTime).SetEase(turnEase));
		yield return new WaitWhile(() => { return sequence.IsActive() && sequence.IsPlaying(); });
		OnArrival?.Invoke();
	}

	public void ResetShip()
	{
		delivered = false;
		sequence = null;
		target = null;
	}
}
