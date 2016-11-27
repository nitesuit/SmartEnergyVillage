using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraLerp : MonoBehaviour {

	public bool moveToStation;
	public bool isMoving;

	public bool isInStart = false;

	public float checkPointTime = 2f;
	public float speed = 1f;
	
	public List<CameraCheckpoint> moveMoveToStart = new List<CameraCheckpoint>();
	public List<CameraCheckpoint> moveMoveToStationCheckPoints = new List<CameraCheckpoint>();

	//
	float startTime;
	float journeyLength;

	Vector3 startMarker;
	Vector3 endMarker;
	
	float startSize;
	float endSize;
	
	int currentMarker = -1;

	bool notStartGame = true;

	//

	public CloudLerp CloudPark;
	public CloudLerp CloudStation;
	public CloudLerp Ballon;

	IEnumerator Start()
	{
		CloudPark.ShouldMove = true;
		yield return new WaitForSeconds(1.0f);
		notStartGame = false;
		
		StartCoroutine(QuestPark());

		while (!ParkQuestDone)
		{

			yield return new WaitForSeconds(0.5f);
		}
		CloudStation.ShouldMove = true;
		Debug.Log("ParkQuestDone");
		moveToStation = true;

		StartCoroutine(QuestionStation());
		yield return new WaitForSeconds(0.5f);
		Ballon.ShouldMove = true;
		
		while (!stationQuestIsDone)
		{
		
			yield return new WaitForSeconds(0.25f);
		}
		
		Debug.Log("stationQuestIsDone !!!!!!!!!");

		float blinkTime = 1.75f;
		float counter = 0f;

		while (counter < blinkTime)
		{
			StationTurbine.GridManager.DoTrainOn(trainAnimator.gameObject.transform, (int)Random.Range (1f, 4f));
			counter += Time.deltaTime;
			yield return null;
		}
	
		StationTurbine.GridManager.DoTrainOn(trainAnimator.gameObject.transform);		
		yield return new WaitForSeconds(0.55f);
		trainAnimator.enabled = true;

	}

	void Update()
	{
		List<CameraCheckpoint> currentList;

		if (notStartGame)
			return;
		
		bool shouldMove;
		if (isInStart)
		{
			currentList = moveMoveToStationCheckPoints;
			shouldMove = moveToStation; 
		}
		else
		{
			currentList = moveMoveToStart;
			shouldMove = true;
		}
	
		if (shouldMove && !isMoving && currentMarker < (currentList.Count - 1))
		{
			Debug.Log("next: " + currentList[currentMarker + 1].name);
			
			if (currentMarker == -1)
			{
				startMarker = transform.position;
				endMarker = currentList[0].transform.position;
				speed = currentList[0].Speed;

				startSize = GetComponent<Camera>().orthographicSize;
				endSize = currentList[0].OrthographicSize;
			}
			else
			{
				startMarker = currentList[currentMarker].transform.position;
				endMarker = currentList[currentMarker + 1].transform.position;
				speed = currentList[currentMarker + 1].Speed;
				
				startSize = currentList[currentMarker].OrthographicSize;
				endSize = currentList[currentMarker + 1].OrthographicSize;
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
				if (!isInStart && !dummy)
				{
					isInStart = true;
					currentMarker = -1;
					dummy = true;
				}
			}			
		}

	}

	bool dummy = false;


	public bool ParkQuestDone;
	public List<TurbineManager> turbinesQuestPark;

	IEnumerator QuestPark()
	{
		while (true)
		{
			bool isDone = true;
			foreach (var t in turbinesQuestPark)
			{
				if (t.IsPowered == false)
					isDone = false;
			}

//			Debug.Log("IsPowered check: " + isDone);

			yield return new WaitForSeconds(0.5f);

			if (isDone)
			{
				yield return new WaitForSeconds(1.5f);
				break;
			}
		}

		ParkQuestDone = true;
		
	}
	
	public bool stationQuestIsDone;
	public TurbineManager StationTurbine;
	public Animator trainAnimator;

	IEnumerator QuestionStation()
	{
		while (true)
		{
			var isDone = StationTurbine.IsPowered;

			if(isDone)
			{
				Debug.Log("QuestionStation::isDone");
				yield return new WaitForSeconds(0.5f);
				break;
			}
			yield return new WaitForSeconds(0.1f);
		}

		stationQuestIsDone = true;

	}

	
}
