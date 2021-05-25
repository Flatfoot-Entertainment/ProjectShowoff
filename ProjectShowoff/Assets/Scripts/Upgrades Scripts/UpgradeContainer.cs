using System.Collections;
using System.Collections.Generic;
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
                upgrade = new AddShip(100, sprite);
                upgradeText.text = "Add Ship";
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
        EventScript.Handler.BroadcastEvent(new ManageUpgradeEvent(upgrade));
		levelText.text = upgrade.Level.ToString();
		costText.text = upgrade.Cost.ToString();
	}

    }

}
