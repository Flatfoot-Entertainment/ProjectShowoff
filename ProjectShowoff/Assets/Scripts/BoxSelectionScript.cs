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
	}

	[SerializeField] private BoxSize[] boxSettings;
	private int boxSelectionIndex;

	[SerializeField] private Material previewMaterial;
	[SerializeField] private FulfillmentCenter fulfillmentCenter;

	[SerializeField] private Button confirmButton;
	[SerializeField] private List<Button> selectionButtons = new List<Button>();

	[SerializeField] private TextMeshProUGUI boxCostText;

	private bool CanBuyBox => CurrentBox() != null && GameHandler.Instance.Money >= CurrentBox().price;
	private Color PreviewColor => CanBuyBox ? validPreview : invalidPreview;

	private void Start()
	{
		foreach (BoxSize box in boxSettings)
		{
			box.preview.GetComponentInChildren<MeshRenderer>().material = previewMaterial;
		}
		
		fulfillmentCenter = FindObjectOfType<FulfillmentCenter>();
		boxSelectionIndex = 0;
		CurrentBox().preview.SetActive(true);
		EventScript.Handler.Subscribe(EventType.BoxConveyorPlace, _ =>
		{
			CurrentBox().preview.SetActive(true);
			SetSelectionButtonsInteractable(true);
			confirmButton.interactable = CanBuyBox;
			previewMaterial.color = PreviewColor;
		});
		EventScript.Handler.Subscribe(EventType.ManageMoney, _ => OnMoneyChange());
		OnMoneyChange();
		boxCostText.text = CurrentBox().price.ToString();
	}

	public void ChangeImage(int direction)
	{
		int previousIndex = boxSelectionIndex;
		boxSelectionIndex += direction;
		boxSelectionIndex = boxSelectionIndex < 0 ? boxSettings.Length - 1 : boxSelectionIndex;
		boxSelectionIndex = boxSelectionIndex >= boxSettings.Length ? 0 : boxSelectionIndex;

		if (previousIndex != boxSelectionIndex)
		{
			boxSettings[previousIndex].preview.SetActive(false);
		}

		boxSettings[boxSelectionIndex].preview.SetActive(true);
		OnMoneyChange();
		boxCostText.text = CurrentBox().price.ToString();
	}

	private void OnMoneyChange()
	{
		confirmButton.interactable = CanBuyBox;
		previewMaterial.color = PreviewColor;
	}

	public void ConfirmBox()
	{
		if (!CanBuyBox) return;
		
		CurrentBox().preview.SetActive(false);
		
		fulfillmentCenter.SpawnBox(CurrentBox().prefab);
		
		EventScript.Handler.BroadcastEvent(new ManageMoneyEvent(-CurrentBox().price));
		confirmButton.interactable = false;
		SetSelectionButtonsInteractable(false);
	}

	private void SetSelectionButtonsInteractable(bool val)
	{
		foreach (Button button in selectionButtons) button.interactable = val;
	}

	private BoxSize CurrentBox()
	{
		return boxSettings[boxSelectionIndex];
	}
}
