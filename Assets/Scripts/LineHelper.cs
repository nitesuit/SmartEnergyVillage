using UnityEngine;
using System.Collections;

public class LineHelper : MonoBehaviour {

	public static void Connect(GameObject firstObject, GameObject secondObject)
	{
		firstObject.AddComponent<LineRenderer> ();
		var lr = firstObject.GetComponent<LineRenderer> ();

		lr.SetWidth (0.1f, 0.1f);
		lr.SetColors (Color.white, Color.white);
		lr.SetPosition(0, firstObject.transform.position);
		lr.SetPosition(1, secondObject.transform.position);
	}
}
