using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxController : BoxController<ItemBoxData, Item>
{
	[SerializeField] private ShippableItemBox shippableBoxPrefab;
	[SerializeField] private AnimationClip closingAnimation;

	private ItemBoxData box;
	protected override void OnAwake()
	{
		base.OnAwake();
		box = new ItemBoxData(BoxType.Type1); // TODO multiple types
	}

	public override ItemBoxData Box
	{
		get => box;
		protected set => box = value;

	}

	protected override void PurgeContents()
	{
		foreach (GameObject gO in contained) Lean.Pool.LeanPool.Despawn(gO);
	}

	private void Ship()
	{
		// Create a new shippable variant
		var shippable = Instantiate(shippableBoxPrefab, transform.position, transform.rotation, transform.parent);

		// Move box to shippable instance
		shippable.Init(Box);
		Box = null;

		// Enable shippable and destroy self
		shippable.gameObject.SetActive(true);
		Destroy(gameObject);
	}

	public void Close()
	{
		StartCoroutine(CloseBox());
	}

	private IEnumerator CloseBox()
	{
		Animator boxAnimator = GetComponentInChildren<Animator>();
		if (boxAnimator == null) Debug.LogError("BoxAnimator not found.");
		else
		{
			boxAnimator.SetBool("isClosing", true);
			yield return new WaitForSeconds(closingAnimation.length);
			Ship();
		}
	}
}
