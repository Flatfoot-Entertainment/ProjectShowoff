using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FulfillmentCenterUI : MonoBehaviour
{
    [SerializeField] private Button shipBoxButton;
    [SerializeField] private FulfillmentCenter fulfillmentCenter;

    private void Start() {
        fulfillmentCenter = FindObjectOfType<FulfillmentCenter>();
    }
    private void Update()
    {
        //change so it doesnt update every frame
        shipBoxButton.interactable = fulfillmentCenter.CanShipBox();
    }

}
