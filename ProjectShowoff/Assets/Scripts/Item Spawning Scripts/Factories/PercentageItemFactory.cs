using UnityEngine;

public class PercentageItemFactory : MonoBehaviour
{
	[PlayModeReadOnly, SerializeField] private ItemSpawningSettings settings;
	private ItemSpawningSettings settingsCopy;

	private void Awake()
	{
		settingsCopy = Instantiate(settings);
	}

	public GameObject CreateRandomItem()
	{
		float randVal = Random.Range(0f, settings.GetWholePercentage());
		float running = 0f;
		foreach (var set in settings.AllSettings())
		{
			if (set.chance + running >= randVal) return set.GetRandom();
			running += set.chance;
		}
		// Weird case, shouldn't happen
		// I like food, so in this edge case, just return a random food item
		return settings.FoodSettings.GetRandom();
	}
}
