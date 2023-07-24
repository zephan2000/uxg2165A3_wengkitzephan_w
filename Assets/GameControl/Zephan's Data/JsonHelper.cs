using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JsonHelper
{
	public static T[] FromJson<T>(string json)
	{
		Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
		return wrapper.session;
	}

	//public static string ToJson<T>(T array) //array takes in whatever you want and converts it to type wrapper, Wrapper gives the array a key
	//{
	//	Debug.Log($"converting session to JSON, {array[0]}");
	//	//session session = new session();
	//	return JsonUtility.ToJson(wrapper);
	//}

	[Serializable]
	private class Wrapper<T> 
	{
		public T[] session;
	}
}
