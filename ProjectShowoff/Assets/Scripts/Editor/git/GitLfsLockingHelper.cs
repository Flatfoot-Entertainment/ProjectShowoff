#if UNITY_EDITOR_WIN
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using System.IO;
using Debug = UnityEngine.Debug;

public class GitLfsLockingHelper
{
	private enum OperationMode
	{
		Lock,
		Unlock,
		ForceUnlock
	}

	private static string OperationFancyName(OperationMode mode)
	{
		return mode switch
		{
			OperationMode.Lock => "Lock",
			OperationMode.Unlock => "Unlock",
			OperationMode.ForceUnlock => "Force Unlock",
			_ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
		};
	}

	private enum OperationResult
	{
		Ok,
		DirectoriesNotSupported,
		NotLockable,
		ProcessFailedToStart,
		Failed
	}

	private static string GetCommand(OperationMode mode, string path)
	{
		return mode switch
		{
			OperationMode.Lock => $"/c git lfs lock \"{path}\"",
			OperationMode.Unlock => $"/c git lfs unlock \"{path}\"",
			OperationMode.ForceUnlock => $"/c git lfs unlock \"{path}\" --force",
			_ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
		};
	}
	
	[MenuItem("Assets/Git LFS/Lock")]
	private static void LockFile()
	{
		CommandDispatcher(OperationMode.Lock);
	}

	[MenuItem("Assets/Git LFS/Unlock")]
	private static void UnlockFile()
	{
		CommandDispatcher(OperationMode.Unlock);
	}

	[MenuItem("Assets/Git LFS/Force Unlock")]
	private static void ForceUnlockFile()
	{
		CommandDispatcher(OperationMode.ForceUnlock);
	}

	private static void CommandDispatcher(OperationMode mode)
	{
		Dictionary<OperationResult, List<string>> results = new Dictionary<OperationResult, List<string>>();
		string[] paths = GetSelectedAssetPaths();
		foreach (string path in paths)
		{
			OperationResult res = OperateOnFile(path, mode);
			if (!results.ContainsKey(res)) results[res] = new List<string>();
			results[res].Add(path);
		}
		EditorUtility.DisplayDialog(
			"Git LFS Operation Results",
			$"Operation: {OperationFancyName(mode)}\n" +
			$"Successful: {results.LengthOf(OperationResult.Ok)}\n" +
			$"Failed: {results.LengthOf(OperationResult.Failed)}\n" +
			$"Not lockable: {results.LengthOf(OperationResult.NotLockable)}\n" +
			$"Directory (not supported): {results.LengthOf(OperationResult.DirectoriesNotSupported)}\n" +
			$"Process failed to start: {results.LengthOf(OperationResult.ProcessFailedToStart)}\n\n" +
			"For more information, view the console.", 
			"Ok"
		);

		if (results.LengthOf(OperationResult.Ok) > 0)
		{
			string msg = $"Successfully locked/unlocked {results.LengthOf(OperationResult.Ok)} files";
			foreach (string path in results[OperationResult.Ok])
			{
				msg += $"\n{path}";
			}
			Debug.Log(msg);
		}

		if (results.LengthOf(OperationResult.NotLockable) > 0)
		{
			string msg = $"{results.LengthOf(OperationResult.NotLockable)} files are not lockable (per extension)";
			foreach (string path in results[OperationResult.NotLockable])
			{
				msg += $"\n{path}";
			}

			Debug.LogWarning(msg);
		}

		if (results.LengthOf(OperationResult.DirectoriesNotSupported) > 0)
		{
			string msg = $"Tried to lock/unlock {results.LengthOf(OperationResult.DirectoriesNotSupported)} directories, which are not supported";
			foreach (string path in results[OperationResult.DirectoriesNotSupported])
			{
				msg += $"\n{path}";
			}

			Debug.LogWarning(msg);
		}

		if (results.LengthOf(OperationResult.Failed) > 0)
		{
			string msg = $"Failed to lock/unlock {results.LengthOf(OperationResult.Failed)} files. See previous output for more information";
			foreach (string path in results[OperationResult.Failed])
			{
				msg += $"\n{path}";
			}

			Debug.LogError(msg);
		}

		if (results.LengthOf(OperationResult.ProcessFailedToStart) > 0)
		{
			string msg =
				$"Failed to start \"git lfs\" process for {results.LengthOf(OperationResult.ProcessFailedToStart)} files.";
			foreach (string path in results[OperationResult.ProcessFailedToStart])
			{
				msg += $"\n{path}";
			}

			Debug.LogError(msg);
		}
	}

	private static OperationResult OperateOnFile(string path, OperationMode mode)
	{
		if (PathIsDirectory(GetFullAssetPath(path)))
			return OperationResult.DirectoriesNotSupported;
		if (!GitSettings.IsLockableExtension(Path.GetExtension(path)))
			return OperationResult.NotLockable;
		
		var gitProc = new ProcessStartInfo
		{
			UseShellExecute = false,
			WorkingDirectory = Application.dataPath + "/../",
			FileName = @"C:\Windows\System32\cmd.exe",
			Arguments = GetCommand(mode, path),
			WindowStyle = ProcessWindowStyle.Hidden,
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			CreateNoWindow = true
		};

		var proc = Process.Start(gitProc);
		if (proc == null)
		{
			return OperationResult.ProcessFailedToStart;
		}

		proc.WaitForExit();
		if (proc.ExitCode != 0)
		{
			
			string output = proc.StandardOutput.ReadToEnd();
			string error = proc.StandardError.ReadToEnd();
			string msg = "" + (error.Length > 0 ? $"Standard Error:\n{error}\n" : "")
			                   + (output.Length > 0 ? $"Standard Output:\n{output}" : "");
			Debug.LogWarning($"Failed to lock {path}\nOutput:\n{msg}");
			return OperationResult.Failed;
		}

		Debug.Log($"(Un-)Locked {path} successfully");
		return OperationResult.Ok;
	}

	private static string[] GetSelectedAssetPaths()
	{
		string[] guids = Selection.assetGUIDs;
		string[] ret = new string[guids.Length];
		for (var i = 0; i < guids.Length; i++)
		{
			ret[i] = AssetDatabase.GUIDToAssetPath(guids[i]);
		}

		return ret;
	}

	private static string GetFullAssetPath(string path)
	{
		var fullPath = Directory.GetParent(Application.dataPath)?.FullName;
		return fullPath != null ? Path.Combine(fullPath, path) : null;
	}

	private static bool PathIsDirectory(string path)
	{
		return (File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory;
	}
}
#else
public class GitLfsLockingHelper {}
#endif
