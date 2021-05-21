using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public delegate void UpgradeBoughtCallback(float amount);
public class UpgradeContainer : MonoBehaviour
{
	public static event UpgradeBoughtCallback OnUpgradeBought;
	[SerializeField] private UpgradeType upgradeType;
	[SerializeField] private Sprite sprite;
	[SerializeField] private Image image;
	[SerializeField] private TextMeshProUGUI upgradeText;
	[SerializeField] private TextMeshProUGUI levelText;
	[SerializeField] private TextMeshProUGUI costText;

	private Upgrade upgrade;
	// Start is called before the first frame update
	void Awake()
	{
		switch (upgradeType)        //eww
		{
			case UpgradeType.ConveyorUpgrade:
				upgrade = new ConveyorPartUpgrade(100, sprite);
				upgradeText.text = "Conveyor Part Upgrade";
				break;
			case UpgradeType.ConveyorStop:
				upgrade = new ConveyorStop(100, sprite);
				upgradeText.text = "Conveyor Stop";
				break;
			case UpgradeType.ShipQuantity:
				upgrade = new ShipQuantity(100, sprite);
				upgradeText.text = "Add Ship";
				break;
			case UpgradeType.FasterDelivery:
				upgrade = new FasterDelivery(100, sprite);
				upgradeText.text = "Faster Delivery";
				break;
		}
		image.sprite = upgrade.Sprite;
		levelText.text = upgrade.Level.ToString();
		costText.text = upgrade.Cost.ToString();
	}

	public void UpdateUpgrade() //such name very read
	{
		upgrade.IncreaseLevel();
		upgrade.ApplyUpgrade();
		levelText.text = upgrade.Level.ToString();
		costText.text = upgrade.Cost.ToString();
		EventScript.Instance.EventQueue.AddEvent(new ManageUpgradeEvent(upgrade));
	}

}
