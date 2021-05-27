using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxSelectionScript : MonoBehaviour
{
	[SerializeField] private Material transparentColor;

    [SerializeField] private GameObject boxSelectionGroup;
    [SerializeField] private GameObject[] boxSelectionModels;
    [SerializeField] private GameObject[] boxPrefabs;
    [SerializeField] private FulfillmentCenter fulfillmentCenter;
    [SerializeField] private Image placeholder;
    private int boxSelectionIndex;

    private Button[] boxButtons;

    private void Start()
    {
        foreach (GameObject gO in boxSelectionModels)
        {
            gO.GetComponentInChildren<MeshRenderer>().material = transparentColor;
        }
        //EventScript.Instance.EventManager.Subscribe(EventType.ManageBoxSelect, ConfirmBox);
        boxButtons = boxSelectionGroup.GetComponentsInChildren<Button>();
        fulfillmentCenter = FindObjectOfType<FulfillmentCenter>();
        boxSelectionIndex = 0;
        boxSelectionModels[boxSelectionIndex].SetActive(true);
        EventScript.Handler.Subscribe(EventType.BoxConveyorPlace, EnableBoxSelectionButtons);
    }

    public void ChangeImage(int direction)
    {
        int previousIndex = boxSelectionIndex;
        boxSelectionIndex += direction;
        boxSelectionIndex = boxSelectionIndex < 0 ? boxSelectionModels.Length - 1 : boxSelectionIndex;
        boxSelectionIndex = boxSelectionIndex >= boxSelectionModels.Length ? 0 : boxSelectionIndex;

        if (previousIndex != boxSelectionIndex)
        {
            boxSelectionModels[previousIndex].SetActive(false);
        }
        boxSelectionModels[boxSelectionIndex].SetActive(true);
    }

    public void ManageBoxConfirmation()
    {
    }

    private void ConfirmBox()
    {
        //TODO finish
        //ManageBoxSelectEvent boxSelectEvent = e as ManageBoxSelectEvent;
        //check if anything in threshold, if yes dont confirm, if no confirm
        GameObject box = boxPrefabs[boxSelectionIndex];
        boxSelectionModels[boxSelectionIndex].SetActive(false);
        if (!box)
        {
            Debug.LogError("Yo this box don't exist");
            return;
        }
        //rly not the best way but eh, probably turn to an event later on
        fulfillmentCenter.SpawnBox(box);
        DisableBoxSelectionButtons();
    }

    private void DisableBoxSelectionButtons()
    {
        foreach (Button boxButton in boxButtons) boxButton.interactable = false;
    }

    private void EnableBoxSelectionButtons(Event e)
    {
        foreach (Button boxButton in boxButtons) boxButton.interactable = true;
        boxSelectionModels[boxSelectionIndex].SetActive(true);
    }

}
