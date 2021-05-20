using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FulfillmentCenterUI : MonoBehaviour
{
	[SerializeField] private Button shipBoxButton;
	[SerializeField] private FulfillmentCenter fulfillmentCenter;

	private void Update()
	{
		shipBoxButton.interactable = fulfillmentCenter.CanShipBox();
	}
}
