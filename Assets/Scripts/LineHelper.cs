using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineHelper : MonoBehaviour {

	static int maxPointCount = 8;
	
	public static void Connect(GameObject firstObject, GameObject secondObject)
	{
		if (firstObject.gameObject.GetComponent<LineRenderer>() != null)
		{
			if (secondObject.gameObject.GetComponent<LineRenderer>() != null)
			{
				return;
			}
			else
			{
				var temp = firstObject;
				firstObject = secondObject;
				secondObject = firstObject;
			}
		}
		
		firstObject.AddComponent<LineRenderer> ();
		var lr = firstObject.GetComponent<LineRenderer> ();

		lr.material = GameManager.instance.WireMaterial;

		lr.SetWidth (0.1f, 0.1f);
		lr.SetColors (Color.white, Color.white);

		//Generate points & shit:

		var distance = secondObject.transform.position - firstObject.transform.position;
		var points = new List<Vector3>();

		var startPoint = firstObject.transform.position;
			
		for (var i = 0; i < maxPointCount; i++)
		{
			var y = startPoint.y - (1 / maxPointCount * (i - maxPointCount / 2)) * 2f;
			var point = new Vector3(startPoint.x + distance.x * (1 / maxPointCount) * i, y, startPoint.x + distance.z * (1 / maxPointCount) * i);
			points.Add(point);
		}

		for (var i = 0; i < points.Count; i++)
		{
			lr.SetPosition(i, points[i]);
		}
	}
}
