using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class BoxController<BoxT, Contained> : MonoBehaviour where BoxT : IBoxData<Contained>
{
	// TODO maybe in the future have a class that handles shipments.
	// That way, we don't have a static event, which might mess with lifetimes, etc.
	private BoxLid<Contained> lid;
	private BoxBody body;
	public abstract BoxT Box { get; protected set; }
	protected List<GameObject> contained = new List<GameObject>();

	[SerializeField] private float finalPositionThreshold = 0.1f;
	[SerializeField] private float sampleBoxCost = 50.0f;

	public bool Shippable { get; private set; }

	private void Awake()
	{
		lid = GetComponentInChildren<BoxLid<Contained>>();
		body = GetComponentInChildren<BoxBody>();
		// box = new ItemBox(BoxType.Type1);
		OnAwake();
	}

	protected virtual void OnAwake() { }

	private void Start()
	{
		lid.OnExitCallback += LidExit;
		lid.OnEnterCallback += LidEnter;
		body.OnContentsUpdated += UpdateShippable;
		UpdateShippable();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			UpdateShippable();
		}
	}

	private void UpdateShippable()
	{
		foreach (BoxScript<Contained> s in lid.inLid)
		{
			if (body.Has(s.gameObject))
			{
				Shippable = false;
				return;
			}
		}
		Shippable = contained.Count > 0;
	}

	private void OnDestroy()
	{
		lid.OnExitCallback -= LidExit;
		lid.OnEnterCallback -= LidEnter;
		body.OnContentsUpdated -= UpdateShippable;
		// Purge contents
		PurgeContents();
	}

	protected abstract void PurgeContents();

	private void LidExit(BoxScript<Contained> subject)
	{
		// If the thing exiting the lid is in the body, it is fully in the box
		if (body.Has(subject.gameObject))
		{
			//containing.Add(subject);
			// subject.transform.SetParent(transform);
			Box.AddToBox(subject.contained);
			contained.Add(subject.gameObject);
			subject.OnAddedToBox();
			// box.ShowBoxContents();
			OnObjectAdded();
		}
		UpdateShippable();
	}

	// If something intersects with the Lid, it is not completely in the box anymore
	private void LidEnter(BoxScript<Contained> subject)
	{
		if (body.Has(subject.gameObject))
		{
			// subject.transform.parent = null;
			Box.RemoveFromBox(subject.contained);
			contained.Remove(subject.gameObject);
			// TODO call functions in child classes
			// box.ShowBoxContents();
			OnObjectRemoved();
		}
		UpdateShippable();
	}

	protected virtual void OnObjectAdded() { }
	protected virtual void OnObjectRemoved() { }
}
