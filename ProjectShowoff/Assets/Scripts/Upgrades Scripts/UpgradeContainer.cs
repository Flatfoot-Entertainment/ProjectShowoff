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

	[SerializeField] private string levelFormat = "Lv {0}";
	[SerializeField] private string costFormat = "{0}";
	[SerializeField] private string allStagesBoughtText = "DONE";

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

		// TODO pretty crusty to rely on an array with more than one element
		
		UpdateUI(OwnUpgrades()[0].upgradeImage);
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
		
		UpdateUI(newLevel.upgradeImage);
	}

	private void UpdateUI(Sprite img)
	{
		levelText.text = string.Format(levelFormat, upgrade.Level.ToString());
		costText.text = doneUpgrading ? allStagesBoughtText : string.Format(costFormat, upgrade.Cost.ToString());
		image.sprite = img;
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
