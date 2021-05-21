using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//the base game class, don't even know what to put in here yet

public enum GameState
{
	PackageView,
	Paused,
	Upgrade,
	PlanetView
}

public abstract class BaseGame : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI moneyText;
	[SerializeField] private float money;
	[SerializeField] private GameState gameState;

	[SerializeField] private GameObject[] conveyorStopButtons;

	[SerializeField] private GameObject[] leftConveyorBelts, rightConveyorBelts;

	//TODO already 3 references of the fulfillment center in the project, make it one
	private FulfillmentCenter fulfillmentCenter;

	public static BaseGame Instance;

	public float Money
	{
		get => money;
		set => money = value;
	}
	//TODO use event queue to decouple game modes from game events

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(Instance);
		}
		fulfillmentCenter = FindObjectOfType<FulfillmentCenter>();
	}

	// Start is called before the first frame update
	protected virtual void Start()
	{
		EventScript.Instance.EventQueue.Subscribe(EventType.ManageMoney, ManageMoney);
		EventScript.Instance.EventQueue.Subscribe(EventType.ManageUpgrade, OnUpgradeBought);
		//EventScript.Instance.EventQueue.Subscribe(EventType.ManageMoney, OnUpgradeBought);
		moneyText.text = "Money: " + money;
	}

	private void OnDestroy()
	{

		//EventScript.Instance.EventQueue.UnSubscribe(EventType.ManageMoney, OnUpgradeBought);
		OnDestroyCallback();
	}

	private void ManageMoney(Event e)
	{
		ManageMoneyEvent manageEvent = e as ManageMoneyEvent;
		money += manageEvent.Amount;
		moneyText.text = "Money: " + money;
	}

	private void ManageUpgrade(Event e)
	{
		ManageUpgradeEvent upgradeEvent = e as ManageUpgradeEvent;
		money -= upgradeEvent.Upgrade.Cost;
		moneyText.text = "Money: " + money;
	}

	protected virtual void OnDestroyCallback()
	{
		EventScript.Instance.EventQueue.UnSubscribe(EventType.ManageMoney, OnBoxSent);
		EventScript.Instance.EventQueue.UnSubscribe(EventType.ManageMoney, OnBoxDelivered);
		EventScript.Instance.EventQueue.UnSubscribe(EventType.ManageUpgrade, OnUpgradeBought);
	}

	protected virtual void OnBoxDelivered(Event e)
	{
		ManageMoney(e);
	}

	protected virtual void OnBoxSent(Event e)
	{
		ManageMoney(e);
	}

	protected virtual void OnUpgradeBought(Event e)
	{
		ManageUpgrade(e);
	}


	//pls refactor into smth else
	public void EnableConveyorButton()
	{
		foreach (GameObject conveyorButton in conveyorStopButtons)
		{
			conveyorButton.SetActive(true);
		}
	}
	public void UpgradeConveyorBelt(int level)
	{
		//TODO into an event, this is horrible
		SpawnerController spawnerController = GetComponent<SpawnerController>();
		if (spawnerController)
		{
			switch (level)
			{
				case 1:
					rightConveyorBelts[0].SetActive(true);
					spawnerController.AddSpawner(rightConveyorBelts[0].GetComponentInChildren<ItemSpawner>());
					spawnerController.AddConveyor(rightConveyorBelts[0].GetComponentInChildren<ConveyorSetupScript>());
					break;
				case 2:
					spawnerController.RemoveConveyorAt(0);
					spawnerController.RemoveSpawnerAt(0);
					leftConveyorBelts[0].SetActive(false);

					leftConveyorBelts[1].SetActive(true);
					spawnerController.AddSpawners(leftConveyorBelts[1].GetComponentsInChildren<ItemSpawner>());
					spawnerController.AddConveyor(leftConveyorBelts[1].GetComponentInChildren<ConveyorSetupScript>());
					break;
				case 3:
					spawnerController.RemoveConveyorAt(0);
					spawnerController.RemoveSpawnerAt(0);
					rightConveyorBelts[0].SetActive(false);

					rightConveyorBelts[1].SetActive(true);
					spawnerController.AddSpawners(rightConveyorBelts[1].GetComponentsInChildren<ItemSpawner>());
					spawnerController.AddConveyor(rightConveyorBelts[1].GetComponentInChildren<ConveyorSetupScript>());
					break;
				default:
					Debug.Log("dis level not exist");
					break;
			}
		}
		else Debug.LogError("SpawnerController not found when upgrading conveyor belts");
	}


	//TODO make into an event
	public void UpgradeShipQuantity()
	{
		fulfillmentCenter.AddShip();
	}
}
