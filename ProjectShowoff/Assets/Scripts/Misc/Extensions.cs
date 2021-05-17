using UnityEngine;
using System;

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

	public static bool Contains(this LayerMask mask, int layer)
	{
		return (mask == (mask | (1 << layer)));
	}
}