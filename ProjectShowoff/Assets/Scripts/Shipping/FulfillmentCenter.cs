using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FulfillmentCenter : MonoBehaviour
{
    [System.Serializable]
    public class DockingSpace
    {
        public Transform location;
        public Button closeButton;
        public bool isUnlocked = false;

        //added a lid prefab for the time being
        public GameObject lid;
        [HideInInspector] public bool free = false;
        [HideInInspector] public ContainerController container;
        [HideInInspector] public int index;
        [HideInInspector] public bool readyForShipment = false;
    }

    // Make it readonly, because we need to preprocess it with indices, etc.
    [PlayModeReadOnly, SerializeField] private List<DockingSpace> dockingSpaces;

    [SerializeField] private Transform boxPos;

    [SerializeField] private PlanetaryShipmentCenter planetaryShipment;

    [SerializeField] private GameObject lidPrefab;

    [SerializeField] private Transform shipParent;
    private ItemBoxController fillableBox;
    // private ShippableItemBox shippedBox;
    private ContainerController fillableContainer;
    private ShippableContainer shippedContainer;

    private void Start()
    {
        // Preprocess List
        for (int i = 0; i < dockingSpaces.Count; i++)
        {
            Debug.Log($"Index A {i}");
            dockingSpaces[i].index = i;
            int val = i; // Clone i, because captures suck
            dockingSpaces[i].closeButton.onClick.AddListener(() =>
            {
                CloseContainer(val);
            });
            dockingSpaces[i].free = true;
            if (dockingSpaces[i].isUnlocked) SpawnContainer(dockingSpaces[i]);
            else dockingSpaces[i].closeButton.gameObject.SetActive(false);
        }
        //SpawnBox();
    }

    public bool CanShipBox()
    {
        return fillableBox && fillableBox.Shippable;
    }

    public bool CanShipContainer()
    {
        return fillableContainer && fillableContainer.Shippable;
    }

    public void OnShipReturn()
    {
        // TODO let specific ship return by moving in the correct place
        SpawnContainer();
    }

    public void OnSendShip(int dockIndex)
    {
        // TODO validate index
        Destroy(dockingSpaces[dockIndex].container.gameObject);
        dockingSpaces[dockIndex].container = null;
        dockingSpaces[dockIndex].free = true;
        dockingSpaces[dockIndex].readyForShipment = false;
        dockingSpaces[dockIndex].closeButton.gameObject.SetActive(false);
    }

    public void CloseContainer(int index)
    {
        if (dockingSpaces[index].readyForShipment) return;
        // TODO ship the container, if money is available (and if theres a non shipped container)
        // TODO maybe destroy the box???
        planetaryShipment.ReadyForShipment(dockingSpaces[index].container.Box, index);
        dockingSpaces[index].readyForShipment = true;
        dockingSpaces[index].lid.SetActive(true);
        dockingSpaces[index].closeButton.enabled = false;
    }

    public void CloseBox()
    {
        StartCoroutine(StartBoxClosing());
    }
    public void SpawnBox(GameObject boxPrefab)
    {
        //tobias help
        if (fillableBox) return;
        fillableBox = Instantiate(boxPrefab, boxPos.position, Quaternion.identity).GetComponent<ItemBoxController>();
    }

    /// Spawn a container in the first free space
    /// Returns if there was a free space (so if a ship could be spawned successfully)
    private bool SpawnContainer(DockingSpace dockSpace = null)
    {
        // TODO move ships in and out of dock
        if (dockSpace == null) dockSpace = dockingSpaces.Where((DockingSpace s) => s.free && s.isUnlocked).FirstOrDefault();
        if (dockSpace == null) return false;
        int index = dockingSpaces.IndexOf(dockSpace);
        dockSpace.container = BoxCreator.Instance.Create<ContainerData>(
            dockSpace.location.position,
            new Vector3(
                10f,
                4f,
                6f
            ),
            shipParent
        ).GetComponent<ContainerController>();

        dockSpace.lid = Instantiate(lidPrefab, dockSpace.location.position + Vector3.up * 4f, Quaternion.identity, dockSpace.container.transform);
        dockSpace.lid.transform.localScale = new Vector3(10f, 0.2f, 6f);
        dockSpace.lid.SetActive(false);

        dockSpace.free = false;
        dockSpace.readyForShipment = false;
        dockSpace.closeButton.gameObject.SetActive(true);
        return true;
    }

    private IEnumerator StartBoxClosing()
    {
        Animator boxAnimator = fillableBox.GetComponentInChildren<Animator>();
        if (boxAnimator == null) Debug.LogError("BoxAnimator not found.");
        else
        {
            boxAnimator.SetBool("isClosing", true);
            yield return new WaitForSeconds(fillableBox.ClosingAnimation.length);
            FinalizeBoxClosing();
        }
    }

    private void FinalizeBoxClosing()
    {
        // TODO ship the box, if money is available (and if theres a non shipped box)
        fillableBox.Ship();
    }

    public void AddShip()
    {
        var empty = dockingSpaces.Where((DockingSpace s) => !s.isUnlocked).FirstOrDefault();
        if (empty == null)
        {
            Debug.LogError("No more ships to spawn, bruh");
            return;
        }

        empty.isUnlocked = true;
        SpawnContainer(empty);

    }

    // OTHER THINGS
    // void SpawnBox() -> don't spawn box automatically
}
