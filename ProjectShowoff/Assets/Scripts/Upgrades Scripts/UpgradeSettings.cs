using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade Settings", menuName = "Upgrades/Upgrade Settings", order = 0)]
public class UpgradeSettings : ScriptableObject
{
	[System.Serializable]
	public class UpgradeLevel
	{
		public bool buyable;
		public Sprite upgradeImage;
		public int price;
	}

	public List<UpgradeLevel> conveyorUpgrades;
	public List<UpgradeLevel> stopButtonLevels;
}
