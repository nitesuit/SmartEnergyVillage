using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraLerp : MonoBehaviour {

	public bool moveToStation;
	public bool isMoving;

	public float checkPointTime = 2f;
	public float speed = 1f;

	public List<CameraCheckpoint> moveMoveToStationCheckPoints = new List<CameraCheckpoint>();

	//
	float startTime;
	float journeyLength;

	Vector3 startMarker;
	Vector3 endMarker;
	
	float startSize;
	float endSize;
	
	int currentMarker = -1;

	void Update()
	{
		if (moveToStation && !isMoving && currentMarker < (moveMoveToStationCheckPoints.Count - 1))
		{
			Debug.Log("next: " + moveMoveToStationCheckPoints[currentMarker + 1].name);
			if (currentMarker == -1)
			{
				startMarker = transform.position;
				endMarker = moveMoveToStationCheckPoints[0].transform.position;

				startSize = GetComponent<Camera>().orthographicSize;
				endSize = moveMoveToStationCheckPoints[0].OrthographicSize;
			}
			else
			{
				startMarker = moveMoveToStationCheckPoints[currentMarker].transform.position;
				endMarker = moveMoveToStationCheckPoints[currentMarker + 1].transform.position;
				
				startSize = moveMoveToStationCheckPoints[currentMarker].OrthographicSize;
				endSize = moveMoveToStationCheckPoints[currentMarker + 1].OrthographicSize;
			}

			startMarker = new Vector3(startMarker.x, transform.position.y, startMarker.z);
			endMarker = new Vector3(endMarker.x, transform.position.y, endMarker.z);
			
		
			startTime = Time.time;
			journeyLength = Vector3.Distance(startMarker, endMarker);
			isMoving = true;

			currentMarker++;
		}
		else if (isMoving)
		{
        	float distCovered = (Time.time - startTime) * speed;
        	float fracJourney = distCovered / journeyLength;
        	transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);

			GetComponent<Camera>().orthographicSize = Mathf.Lerp(startSize, endSize, fracJourney);

			if (fracJourney >= 1f)
			{
				isMoving = false;
			}

		}

	}

	
}
