using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BoxDeliveredCallback(float value);
public delegate void BoxSentCallback(float value);
public class BoxContainer : MonoBehaviour
{
	// TODO maybe in the future have a class that handles shipments.
	// That way, we don't have a static event, which might mess with lifetimes, etc.
	public static event BoxDeliveredCallback OnBoxDelivered;
	public static event BoxSentCallback OnBoxSent;
	//private List<GameObject> containing = new List<GameObject>();
	private BoxLid lid;
	private BoxBody body;
	private Box box;


	[SerializeField] private bool isMoving;
	[SerializeField] private float moveSpeed = 1.5f;
	[SerializeField] private float finalPositionThreshold = 0.1f;
	[SerializeField] private float sampleBoxCost = 50.0f;

	private Vector3 samplePositionToMoveBoxToToSimulateMovingBox; //only for testing stuff, pls remove this later on

	private void Awake()
	{
		samplePositionToMoveBoxToToSimulateMovingBox = new Vector3(8.0f, transform.position.y, transform.position.z);
		lid = GetComponentInChildren<BoxLid>();
		body = GetComponentInChildren<BoxBody>();
		box = new Box(BoxType.Type1);
	}

	private void Start()
	{
		lid.OnExitCallback += LidExit;
		lid.OnEnterCallback += LidEnter;
		OnBoxDelivered += DestroyBox;
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
			isMoving = true;	//eww
			OnBoxSent?.Invoke(sampleBoxCost);
        }
        if (isMoving)
        {
			transform.position = Vector3.Lerp(transform.position, samplePositionToMoveBoxToToSimulateMovingBox, moveSpeed * Time.deltaTime);
            if (GetFinalPositionDifference(samplePositionToMoveBoxToToSimulateMovingBox) < finalPositionThreshold)
            {
				OnBoxDelivered?.Invoke(box.GetBoxContentsValue());
				isMoving = false;
            }
        }
	}

	private void OnDestroy()
	{
		lid.OnExitCallback -= LidExit;
		lid.OnEnterCallback -= LidEnter;
		OnBoxDelivered -= DestroyBox;
	}

	private void DestroyBox(float value)
    {
		Debug.Log("Contents sent...");
		box.ShowBoxContents();
		Destroy(gameObject);
	}

	private void LidExit(ItemScript subject)
	{
		// If the thing exiting the lid is in the body, it is fully in the box
		if (body.Has(subject.gameObject))
		{
			//containing.Add(subject);
			subject.transform.SetParent(transform);
			box.AddItemToBox(subject.Item);
			box.ShowBoxContents();
		}
	}

	// If something intersects with the Lid, it is not completely in the box anymore
	private void LidEnter(ItemScript subject)
	{
		if (body.Has(subject.gameObject))
		{
			subject.transform.parent = null;
			box.RemoveItemFromBox(subject.Item);
			box.ShowBoxContents();
		}
	}

	private float GetFinalPositionDifference(Vector3 endPoint)
    {
		float magnitudeDiff = transform.position.magnitude - endPoint.magnitude;
		Debug.Log("Magnitude diff: " + magnitudeDiff);
		return Mathf.Abs(magnitudeDiff);
    }

}
