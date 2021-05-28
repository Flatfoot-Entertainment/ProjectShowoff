using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadonlyHack : UnityEditor.AssetModificationProcessor
{

	static string[] OnWillSaveAssets(string[] paths)
	{
		List<string> saveable = new List<string>();
		List<string> unsaveable = new List<string>();
		foreach (string path in paths)
		{
			FileInfo info = new FileInfo(path);
			// If the file exists, has a forbidden extension and is readonly, it is unsaveable
			if (!(info.Exists && GitSettings.IsLockableExtension(info.Extension) && info.IsReadOnly))
				saveable.Add(path);
			else
				unsaveable.Add(path);
		}
		foreach (string path in unsaveable)
		{
			Debug.LogWarning($"{path} is marked readonly, so probably locked by Git LFS. Skipped saving it.");
		}
		return saveable.ToArray();
	}
}
