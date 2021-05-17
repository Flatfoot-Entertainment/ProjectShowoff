using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxSelectionScript : MonoBehaviour
{
    [SerializeField] private Sprite[] boxImages;
    [SerializeField] private FulfillmentCenter fulfillmentCenter;
    [SerializeField] private Image placeholder;
    private int imageIndex;

    private void Start()
    {
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


    public void ConfirmBox()
    {
        //check if anything in threshold, if yes dont confirm, if no confirm
        Vector3 boxSize = Vector3.zero;
        switch (imageIndex)
        {
            case 0:
                boxSize = new Vector3(1f, 1f, 1f);
                break;
            case 1:
                boxSize = new Vector3(1.5f, 1.5f, 1.5f);
                break;
            case 2:
                boxSize = new Vector3(2f, 2f, 2f);
                break;
            default:
                Debug.LogWarning("Yo bro this image index doesn't exist");
                break;
        }
        //rly not the best way but eh, probably turn to an event later on

        fulfillmentCenter.SpawnBox(boxSize);
    }


}
