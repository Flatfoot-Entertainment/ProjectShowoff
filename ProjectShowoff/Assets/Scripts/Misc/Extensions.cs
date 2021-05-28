using UnityEngine;
using System;
using System.Collections.Generic;

public static class Extensions
{
	public static T RandomEnumValue<T>() where T : Enum
	{
		var v = Enum.GetValues(typeof(T));
		return (T)v.GetValue(UnityEngine.Random.Range(0, v.Length));
	}

	public static T Random<T>(this T[] arr)
	{
		return arr[UnityEngine.Random.Range(0, arr.Length)];
	}

	public static T Random<T>(this List<T> arr)
	{
		return arr[UnityEngine.Random.Range(0, arr.Count)];
	}

	public static bool Contains(this LayerMask mask, int layer)
	{
		return (mask == (mask | (1 << layer)));
	}

	public static string ToBeautifulString<T>(this Dictionary<ItemType, T> val)
	{
		string ret = "";
		foreach (var kvp in val)
		{
			ret += $"{kvp.Key.ToString()}: {kvp.Value}\n";
		}
		return ret;
	}

	public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
	{
		T comp = gameObject.GetComponent<T>();
		return comp ? comp : gameObject.AddComponent<T>();
	}

	public static U ValueOr<T, U>(this Dictionary<T, U> dict, T key, U defaultValue)
	{
		return dict.ContainsKey(key) ? dict[key] : defaultValue;
	}

	public static int LengthOf<T, U>(this Dictionary<T, List<U>> dict, T key)
	{
		return dict.ContainsKey(key) && dict[key] != null ? dict[key].Count : 0;
	}

	public static void SetX(this Vector3 v, float val)
	{
		v.x = val;
	}

	public static void SetY(this Vector3 v, float val)
	{
		v.y = val;
	}

	public static void SetZ(this Vector3 v, float val)
	{
		v.z = val;
	}
}