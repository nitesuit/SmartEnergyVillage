using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GridsManager : MonoBehaviour {

	private List<GridsManager> grids;
	// Use this for initialization
	void Awake () {
		var gridManagers = GameObject.FindObjectsOfType<GridManager> ();
		var lights = FindObjectsOfType<Light> (); 
		int splitNumber = (int)Mathf.Ceil (lights.Count () / gridManagers.Count ());
		var arrayOfLightArrays = lights.Split (splitNumber).ToArray();
		for (int i = 0; i < gridManagers.Count(); i++) {
			gridManagers[i].Lights = arrayOfLightArrays.ElementAt (i).ToArray();
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}


}

public static class ArraySplitter : System.Object {
	public static IEnumerable<IEnumerable<T>> Split<T>(this T[] array, int size)
	{
		for (var i = 0; i < (float)array.Length / size; i++)
		{
			yield return array.Skip(i * size).Take(size);
		}
	}
}