using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ship : MonoBehaviour
{
    [HideInInspector]
    public event System.Action<Ship> OnArrival; //TODO EventManager..?
    public ContainerData box { get; set; }
    private Planet target;
    private Vector3 startPos;
    private Vector3 startForward;
    private Sequence sequence;

    [SerializeField] private float travelTime;
    [SerializeField] private float turnTime;

    // TODO variable speed, etc.

    private bool delivered = false;

    public void DeliverTo(Planet planet)
    {
        target = planet;
        startPos = transform.position;
        startForward = transform.forward;
        sequence = TravelSequenceTowards(target.transform.position);
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
        sequence = TravelSequenceTowards(startPos);
        sequence.Insert(travelTime, transform.DOLookAt(startPos + startForward, turnTime));
        yield return new WaitWhile(() => { return sequence.IsActive() && sequence.IsPlaying(); });
        OnArrival?.Invoke(this);
    }

    private Sequence TravelSequenceTowards(Vector3 pos)
    {
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOLookAt(pos, turnTime));
        sequence.Append(transform.DOMove(pos, travelTime));
        return sequence;
    }
}
