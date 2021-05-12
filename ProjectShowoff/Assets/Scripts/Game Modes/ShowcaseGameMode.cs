using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseGameMode : BaseGame
{
	[SerializeField] private Transform referenceContainerPos;

	protected override void Start()
	{
		base.Start();
		// ItemBoxController.OnBoxDelivered += OnBoxDelivered;
		// ContainerController.OnBoxDelivered += OnContainerDelivered;
		// OnBoxDelivered(float.MinValue); // To lazy to copy paste
		// OnContainerDelivered(float.MinValue);
	}

	protected override void OnDestroyCallback()
	{
		// ItemBoxController.OnBoxDelivered -= OnBoxDelivered;
		// ContainerController.OnBoxDelivered -= OnContainerDelivered;
	}

	private void OnBoxDelivered(float value)
	{
		BoxCreator.Instance.Create<ItemBoxData>(
			new Vector3(0f, 0.5f, -4f),
			new Vector3(
				Random.Range(1f, 2f),
				Random.Range(0.75f, 1.25f),
				Random.Range(1f, 2f)
			),
			null
		);
	}

	private void OnContainerDelivered(float value)
	{
		BoxCreator.Instance.Create<ContainerData>(
			referenceContainerPos.position,
			new Vector3(
				Random.Range(2.5f, 3f),
				Random.Range(2f, 3f),
				Random.Range(2f, 3f)
			),
			null
		);
	}
}
