using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseGameMode : BaseGame
{
	[SerializeField] private Transform referenceContainerPos;
	

	protected override void Start()
	{
		base.Start();
	}

	protected override void OnDestroyCallback()
	{
		base.OnDestroyCallback();
	}

	protected override void OnBoxDelivered(Event e)
	{
		base.OnBoxDelivered(e);
	}

	private void OnContainerDelivered(float value)
	{}

	
}
