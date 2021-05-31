using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoxSelectionScript : MonoBehaviour
{
	[SerializeField] private Color validPreview;
	[SerializeField] private Color invalidPreview;
	
	[System.Serializable]
	public class BoxSize
	{
		public GameObject prefab;
		public GameObject preview;
		public int price;
        public GameObject button;
	}

	[SerializeField] private BoxSize[] boxSettings;
    private int boxSelectionIndex;

    [SerializeField] private Material previewMaterial;
    [SerializeField] private FulfillmentCenter fulfillmentCenter;

    [SerializeField] private Button confirmButton;

    [SerializeField] private TextMeshProUGUI boxCostText;
    // private Button[] boxButtons;

    private void Start()
    {
        foreach (BoxSize box in boxSettings)
        {
            box.preview.GetComponentInChildren<MeshRenderer>().material = previewMaterial;
            box.button.SetActive(false);
        }
        fulfillmentCenter = FindObjectOfType<FulfillmentCenter>();
        boxSelectionIndex = 0;
        CurrentBox().preview.SetActive(true);
        CurrentBox().button.SetActive(true);
        EventScript.Handler.Subscribe(EventType.BoxConveyorPlace, _ =>
        {
	        BoxSize box = boxSettings[boxSelectionIndex];
	        box.preview.SetActive(true);
	        EnableBoxSelectionButtons();
        });
        EventScript.Handler.Subscribe(EventType.ManageMoney, _ => OnMoneyChange());
        OnMoneyChange();
        boxCostText.text = CurrentBox().price.ToString();
    }

    public void ChangeBox(int direction)
    {
        int previousIndex = boxSelectionIndex;
        boxSelectionIndex += direction;
        boxSelectionIndex = boxSelectionIndex < 0 ? boxSettings.Length - 1 : boxSelectionIndex;
        boxSelectionIndex = boxSelectionIndex >= boxSettings.Length ? 0 : boxSelectionIndex;

        if (previousIndex != boxSelectionIndex)
        {
	        boxSettings[previousIndex].preview.SetActive(false);
            boxSettings[previousIndex].button.SetActive(false);
        }

        boxSettings[boxSelectionIndex].preview.SetActive(true);
        boxSettings[boxSelectionIndex].button.SetActive(true);
        OnMoneyChange();
        boxCostText.text = CurrentBox().price.ToString();
    }
    private void OnMoneyChange()
    {
	    BoxSize box = boxSettings[boxSelectionIndex];
	    if (box.price > GameHandler.Instance.Money)
	    {
		    previewMaterial.color = invalidPreview;
		    confirmButton.interactable = false;
		    DisableBoxSelectionButtons();
	    }
	    else
	    {
		    EnableBoxSelectionButtons(); // The event is not needed
		    previewMaterial.color = validPreview;
	    }
    }

    public void ConfirmBox()
    {
	    BoxSize box = boxSettings[boxSelectionIndex];
	    if (CurrentBox().price > GameHandler.Instance.Money) return;
        //TODO finish
        //ManageBoxSelectEvent boxSelectEvent = e as ManageBoxSelectEvent;
        //check if anything in threshold, if yes dont confirm, if no confirm
        CurrentBox().preview.SetActive(false);
        //rly not the best way but eh, probably turn to an event later on
        fulfillmentCenter.SpawnBox(box.prefab);
        
        EventScript.Handler.BroadcastEvent(new ManageMoneyEvent(-box.price));
        DisableBoxSelectionButtons();
    }

    private void DisableBoxSelectionButtons()
    {
	    confirmButton.interactable = false;
    }

    private void EnableBoxSelectionButtons()
    {
	    BoxSize box = boxSettings[boxSelectionIndex];
		confirmButton.interactable = GameHandler.Instance.Money >= box.price;
	    // box.preview.SetActive(true);
    }

    private BoxSize CurrentBox()
    {
	    return boxSettings[boxSelectionIndex];
    }
}
