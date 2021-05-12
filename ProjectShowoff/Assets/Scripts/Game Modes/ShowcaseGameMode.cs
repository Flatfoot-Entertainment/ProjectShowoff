using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseGameMode : BaseGame
{
	protected override void Start()
	{
		base.Start();
		BoxCreator.Instance.Create(
			new Vector3(0f, 0.5f, 0f),
			new Vector3(
				Random.Range(0.5f, 3f),
				Random.Range(0.5f, 1.5f),
				Random.Range(0.5f, 3f)
			),
			null
		);
	}

	protected override void OnDestroyCallback()
	{
		base.OnDestroyCallback();
	}

	protected override void OnBoxDelivered(Event e)
	{
		base.OnBoxDelivered(e);
		BoxCreator.Instance.Create(
			new Vector3(0f, 0.5f, 0f),
			new Vector3(
				Random.Range(0.5f, 3f),
				Random.Range(0.5f, 1.5f),
				Random.Range(0.5f, 3f)
			),
			null
		);
	}
}
