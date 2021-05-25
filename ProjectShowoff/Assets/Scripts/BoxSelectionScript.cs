using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxSelectionScript : MonoBehaviour
{
    [SerializeField] private Sprite[] boxImages;
    [SerializeField] private GameObject[] boxPrefabs;
    [SerializeField] private FulfillmentCenter fulfillmentCenter;
    [SerializeField] private Image placeholder;
    private int imageIndex;

    private void Start()
    {
        //EventScript.Instance.EventManager.Subscribe(EventType.ManageBoxSelect, ConfirmBox);
        fulfillmentCenter = FindObjectOfType<FulfillmentCenter>();
        imageIndex = 0;
        placeholder.sprite = boxImages[imageIndex];
    }

    public void ChangeImage(int direction)
    {
        imageIndex += direction;
        if (imageIndex < 0) imageIndex = boxImages.Length - 1;
        else if (imageIndex >= boxImages.Length) imageIndex = 0;
        placeholder.sprite = boxImages[imageIndex];
    }

    public void ManageBoxConfirmation()
    {
    }

    private void ConfirmBox()
    {
        //TODO finish
        //ManageBoxSelectEvent boxSelectEvent = e as ManageBoxSelectEvent;
        //check if anything in threshold, if yes dont confirm, if no confirm
        GameObject box = boxPrefabs[imageIndex];
        if (!box)
        {
            Debug.LogError("Yo this box don't exist");
            return;
        }
        //rly not the best way but eh, probably turn to an event later on
        fulfillmentCenter.SpawnBox(box);
    }


}
