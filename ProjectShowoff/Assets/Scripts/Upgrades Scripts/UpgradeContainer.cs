using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeContainer : MonoBehaviour
{
	[SerializeField] private UpgradeType upgradeType;
	[SerializeField] private Sprite sprite;
	[SerializeField] private Image image;
	[SerializeField] private TextMeshProUGUI upgradeText;
	[SerializeField] private TextMeshProUGUI levelText;
	[SerializeField] private TextMeshProUGUI costText;
	[SerializeField] private Button buyButton;

	private Upgrade upgrade;
	// Start is called before the first frame update
	void Awake()
	{
		// eww
		switch (upgradeType)
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
				upgrade = new AddShip(100, sprite);
				upgradeText.text = "Add Ship";
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
		image.sprite = upgrade.Sprite;
		levelText.text = upgrade.Level.ToString();
		costText.text = upgrade.Cost.ToString();
	}

	private void Update()
	{
		// Yes, I know this isn't the best in Update, but it's the fastest to do, so... :^)
		buyButton.interactable = GameHandler.Instance.Money < upgrade.Cost;
	}

	public void UpdateUpgrade() //such name very read
	{
		if (GameHandler.Instance.Money < upgrade.Cost) return;
		upgrade.IncreaseLevel();
		upgrade.ApplyUpgrade();
		EventScript.Handler.BroadcastEvent(new ManageUpgradeEvent(upgrade));
		levelText.text = upgrade.Level.ToString();
		costText.text = upgrade.Cost.ToString();
	}
}
