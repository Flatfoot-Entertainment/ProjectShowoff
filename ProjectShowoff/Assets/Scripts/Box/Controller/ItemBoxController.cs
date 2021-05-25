using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxController : BoxController<ItemBoxData, Item>
{
    public AnimationClip ClosingAnimation => closingAnimation;

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

	public ShippableItemBox Ship()
	{
		// TODO variable cost
		EventScript.Handler.BroadcastEvent(new ManageMoneyEvent(-50));

		// Create a new shippable variant
		var shippable = Instantiate<ShippableItemBox>(shippableBoxPrefab, transform.position, transform.rotation, transform.parent);

		// Move box to shippable instance
		shippable.Init(Box);
		Box = null;

		// Enable shippable and destroy self
		shippable.gameObject.SetActive(true);
		Destroy(gameObject);
		return shippable;
	}
}
