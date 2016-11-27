using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraLerp : MonoBehaviour {

	public bool moveToStation;
	public bool isMoving;

	public float checkPointTime = 2f;

	public List<Transform> moveMoveToStationCheckPoints = new List<Transform>();

	void Update()
	{
		if (moveToStation && !isMoving)
		{
			StartCoroutine(MoveByPath(moveMoveToStationCheckPoints));
			isMoving = true;
		}

	}
	
	IEnumerator MoveByPath(List<Transform> path)
	{

		foreach (var point in path)
		{
			var timer = 0f;
			while (timer < checkPointTime)
			{
				var target = new Vector3(point.position.x, transform.position.y, point.position.z);
				transform.position = Vector3.Slerp(transform.position, target, checkPointTime);
				
				timer += Time.deltaTime;
				yield return null;
			}
		}
		
	}

	
}
