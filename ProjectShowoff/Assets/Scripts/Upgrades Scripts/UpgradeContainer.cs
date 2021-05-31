using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeContainer : MonoBehaviour
{
	[SerializeField] private UpgradeSettings settings;
	[SerializeField] private UpgradeType upgradeType;
	[SerializeField] private Image image;
	[SerializeField] private TextMeshProUGUI levelText;
	[SerializeField] private TextMeshProUGUI costText;
	[SerializeField] private Button buyButton;

	private bool doneUpgrading;

	private Upgrade upgrade;
	// Start is called before the first frame update
	void Awake()
	{
		// eww
		// Still eww after this refactor xD
		upgrade = upgradeType switch
		{
			UpgradeType.ConveyorUpgrade => new ConveyorPartUpgrade(100),
			UpgradeType.ConveyorStop => new ConveyorStop(100),
			UpgradeType.ShipQuantity => new AddShip(100),
			_ => throw new ArgumentOutOfRangeException()
		};

		image.sprite = OwnUpgrades()[0].upgradeImage;
		levelText.text = upgrade.Level.ToString();
		costText.text = upgrade.Cost.ToString();
		buyButton.interactable = true;
	}

	private void Update()
	{
		// Yes, I know this isn't the best in Update, but it's the fastest to do, so... :^)
		buyButton.interactable = !doneUpgrading && GameHandler.Instance.Money >= upgrade.Cost;
	}

	public void UpdateUpgrade() //such name very read
	{
		if (doneUpgrading || !(upgrade.Level < OwnUpgrades().Count - 1))
		{
			doneUpgrading = true;
			buyButton.interactable = false;
			return;
		}

		if (GameHandler.Instance.Money < upgrade.Cost) return;
		
		var newLevel = OwnUpgrades()[upgrade.Level + 1];
		if (!newLevel.buyable)
		{
			doneUpgrading = true;
			buyButton.interactable = false;
		}

		// This doesn't have to be a ManageUpgradeEvent -> This is clearer imo
		EventScript.Handler.BroadcastEvent(new ManageMoneyEvent(-upgrade.Cost));
		upgrade.IncreaseLevel(newLevel.price);
		upgrade.ApplyUpgrade();
		
		levelText.text = $"Lv {upgrade.Level.ToString()}";
		costText.text = doneUpgrading ? "DONE" : upgrade.Cost.ToString();
		image.sprite = newLevel.upgradeImage;
	}

	private List<UpgradeSettings.UpgradeLevel> OwnUpgrades()
	{
		// WTF is this unholy construct???
		return upgradeType switch
		{
			UpgradeType.ConveyorStop => settings.stopButtonLevels,
			UpgradeType.ConveyorUpgrade => settings.conveyorUpgrades,
			_ => null
		};
	}
}
