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
			case UpgradeType.BoxSizeIncrease:
				upgrade = new BoxSizeIncrease(100, sprite);
				upgradeText.text = "Bigger Box";
				break;
			case UpgradeType.ConveyorStop:
				upgrade = new ConveyorStop(100, sprite);
				upgradeText.text = "Conveyor Stop";
				break;
			case UpgradeType.DeliveryShipSpeed:
				upgrade = new DeliveryShipSpeed(100, sprite);
				upgradeText.text = "Delivery Speed";
				break;
			case UpgradeType.FasterDelivery:
				upgrade = new FasterDelivery(100, sprite);
				upgradeText.text = "Faster Delivery";
				break;
			default:
				break;
		}
		image.sprite = upgrade.Sprite;
		levelText.text = upgrade.Level.ToString();
		costText.text = upgrade.Cost.ToString();
	}

	public void UpdateUpgrade() //such name very read
	{
		upgrade.ApplyUpgrade();
		upgrade.IncreaseLevel();
		levelText.text = upgrade.Level.ToString();
		costText.text = upgrade.Cost.ToString();
		EventScript.Instance.EventQueue.AddEvent(new ManageUpgradeEvent(upgrade));
	}

}
