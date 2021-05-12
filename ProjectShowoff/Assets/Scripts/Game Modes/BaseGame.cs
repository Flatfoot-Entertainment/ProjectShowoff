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
	public float Money
	{
		get => money;
		set => money = value;
	}
	//TODO use event queue to decouple game modes from game events

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
}
