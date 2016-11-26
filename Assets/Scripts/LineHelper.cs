using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineHelper : MonoBehaviour {

	static int maxPointCount = 32;
	
	public static void Connect(Transform firstObject, Transform secondObject)
	{
		if (firstObject == secondObject)
			return;
	
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

		firstObject = firstObject.GetComponent<SmartObject>().wireConnector;
		secondObject = secondObject.GetComponent<SmartObject>().wireConnector;
		
		firstObject.gameObject.AddComponent<LineRenderer> ();
		var lr = firstObject.GetComponent<LineRenderer> ();

		lr.material = GameManager.instance.WireMaterial;

		lr.SetWidth (0.1f, 0.1f);
		lr.SetColors (Color.green, Color.green);

		//Generate points & shit:

		var dist =  secondObject.transform.position - firstObject.transform.position ;
		var points = new List<Vector3>();
	
		var startPoint = firstObject.transform.position;
				
		for (var i = 0; i < maxPointCount; i++)
		{

			float yMod;
			if (i < maxPointCount / 2)
			{
				yMod = (1f / maxPointCount) * (maxPointCount - i);
			}
			else 
			{
				yMod = (1f / maxPointCount) * i;
			}
			yMod = yMod / 2f + 0.5f; 
			
			Vector3 mod = dist * ((1f / maxPointCount) * i);
			
			Debug.Log(yMod);
			
			var point = startPoint + mod;
			point = new Vector3(point.x, point.y * yMod , point.z);
			points.Add(point);
		}

		//Debug.Log("First " + firstObject.transform.position.ToString() + " Second " + secondObject.transform.position.ToString());
		//Debug.Log(dist.ToString());
		//Debug.Log(points.Count);


		lr.SetVertexCount(maxPointCount);
		lr.SetPositions(points.ToArray());
	}
}
