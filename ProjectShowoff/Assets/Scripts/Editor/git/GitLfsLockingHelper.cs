using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using Debug = UnityEngine.Debug;

public class GitLfsLockingHelper
{
	[MenuItem("Assets/Git LFS/Lock")]
	private static void LockFile()
	{
		var (fullPath, subPath) = GetSelectedPath();
		if (!PathIsDirectory(fullPath))
		{
			if (!File.Exists(fullPath))
			{
				EditorUtility.DisplayDialog("Does not exist!", "This file does not exist!", "Ok");
			}
			else
			{
				LockFileHelper(subPath);
			}
		}
		else
		{
			EditorUtility.DisplayDialog("Not supported", "Currently locking directories is not supported!", "Ok");
		}
	}

	private static void LockFileHelper(string path)
	{
		var gitProc = new ProcessStartInfo
		{
			UseShellExecute = false,
			WorkingDirectory = Application.dataPath + "/../",
			FileName = @"C:\Windows\System32\cmd.exe",
			Arguments = $"/c git lfs lock \"{path}\"",
			WindowStyle = ProcessWindowStyle.Hidden,
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			CreateNoWindow = true
		};
		// So far only supported on windows

		var proc = Process.Start(gitProc);
		if (proc == null)
		{
			EditorUtility.DisplayDialog("Could not start process!", "", "Ok");
			return;
		}

		proc.WaitForExit();
		if (proc.ExitCode != 0)
		{
			string output = proc.StandardOutput.ReadToEnd();
			string error = proc.StandardError.ReadToEnd();
			string msgBox = "" + (error.Length > 0 ? $"Standard Error:\n{error}\n" : "")
			                   + (output.Length > 0 ? $"Standard Output:\n{output}" : "");
			EditorUtility.DisplayDialog($"Error (Exit code {proc.ExitCode}", msgBox, "Ok");
		}
		else
		{
			Debug.Log($"Locked {path} successfully");
		}
	}

	[MenuItem("Assets/Git LFS/Unlock")]
	private static void UnlockFile()
	{
		var (fullPath, subPath) = GetSelectedPath();
		if (!PathIsDirectory(fullPath))
		{
			if (!File.Exists(fullPath))
			{
				EditorUtility.DisplayDialog("Does not exist!", "This file does not exist!", "Ok");
			}
			else
			{
				UnlockFileHelper(subPath, false);
			}
		}
		else
		{
			EditorUtility.DisplayDialog("Not supported", "Currently locking directories is not supported!", "Ok");
		}
	}

	[MenuItem("Assets/Git LFS/Force Unlock")]
	private static void ForceUnlockFile()
	{
		if (!EditorUtility.DisplayDialog("Dangerous operation", "Forcing a lock open is normally not a good idea! " +
		                                                        "Only do it, if you know exactly what you are doing!", 
			"I know the risks", "Cancel")) return;
		var (fullPath, subPath) = GetSelectedPath();
		if (!PathIsDirectory(fullPath))
		{
			if (!File.Exists(fullPath))
			{
				EditorUtility.DisplayDialog("Does not exist!", "This file does not exist!", "Ok");
			}
			else
			{
				UnlockFileHelper(subPath, true);
			}
		}
		else
		{
			EditorUtility.DisplayDialog("Not supported", "Currently locking directories is not supported!", "Ok");
		}
	}

	private static void UnlockFileHelper(string path, bool force)
	{
		var gitProc = new ProcessStartInfo
		{
			UseShellExecute = false,
			WorkingDirectory = Application.dataPath + "/../",
			FileName = @"C:\Windows\System32\cmd.exe",
			Arguments = $"/c git lfs unlock \"{path}\"" + (force ? " --force" : ""),
			WindowStyle = ProcessWindowStyle.Hidden,
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			CreateNoWindow = true
		};
		// So far only supported on windows

		var proc = Process.Start(gitProc);
		if (proc == null)
		{
			EditorUtility.DisplayDialog("Could not start process!", "", "Ok");
			return;
		}

		proc.WaitForExit();
		if (proc.ExitCode != 0)
		{
			string output = proc.StandardOutput.ReadToEnd();
			string error = proc.StandardError.ReadToEnd();
			string msgBox = "" + (error.Length > 0 ? $"Standard Error:\n{error}\n" : "")
			                   + (output.Length > 0 ? $"Standard Output:\n{output}" : "");
			EditorUtility.DisplayDialog($"Error (Exit code {proc.ExitCode})", msgBox, "Ok");
		}
		else
		{
			Debug.Log($"Unlocked {path} successfully");
		}
	}

	private static (string full, string part) GetSelectedPath()
	{
		var selected = Selection.activeObject;
		var path = AssetDatabase.GetAssetPath(selected);
		var fullPath = Directory.GetParent(Application.dataPath)?.FullName;
		if (fullPath != null) return (Path.Combine(fullPath, path), path);
		Debug.LogError($"{path} has no parent for some reason");
		return (null, null);
	}

	private static bool PathIsDirectory(string path)
	{
		return (File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory;
	}
}