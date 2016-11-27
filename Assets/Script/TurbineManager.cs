using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TurbineManager : MonoBehaviour
{
	public bool thisIsPrimary = false;

	public Transform rotator;
	public Transform meshObj;
	public Transform particleSys;
	
	public float volume;
	public float rotatorSpeed;

	public float fixedBuiltTime = 0.75f;
	//

	private float buildTimer;
	private float maxSpeed = 7.5f;
	private int SampleSize = 256;
	//
	private bool isBuilt;
	private bool isConnected;
	private bool isPowered;

	private float minYPosition = 9.5f;
	//
	
	static AudioSource audioSource;
	public static float baseVolume;
	public GridManager GridManager;

	// Use this for initialization

	void Awake() {
		GridManager = GetComponent<GridManager> ();
	}

	void Start()
	{

		audioSource = GetComponent<AudioSource>();
		meshObj.gameObject.SetActive(false);
		StartCoroutine(ShowBuildParticleCorout(false));

	}


	void OnGUI()
	{
		//var style = new GUIStyle();
		//style.fontSize = 36;

		//var strText = string.Format(" Vol : {0} \n bVol : {1} \n speed : {2}", volume, baseVolume, rotatorSpeed);

		//GUI.Label(new Rect(400f, 100f, 400f, 400f), strText, style);
	}
	
	public bool isReady;

	void OnMouseDrag()
	{

		if (buildTimer <= 0f)
		{
			StartCoroutine(ShowBuildParticleCorout(true, false, 0f));
		}
		
		if (!isReady)
		{

			IsBuilt = true;

			buildTimer += Time.deltaTime;
		}

		if (buildTimer > fixedBuiltTime && !isReady)
		{
			StartCoroutine(ShowBuildParticleCorout(false, true, 4.5f));
			IsConnected = true;
			isReady = true;
		}
		else if ( meshObj.transform.localPosition.y <= 0 && !isReady)
		{	
			var y = minYPosition * ((1 / fixedBuiltTime) * buildTimer); 
			meshObj.transform.localPosition = new Vector3(meshObj.transform.localPosition.x, (minYPosition * -1) + y, meshObj.transform.localPosition.z);
		}
	}

	IEnumerator ShowBuildParticleCorout(bool status, bool showBlast = false, float delay = 0f)
	{

			
		if (showBlast)
		{
			var p = particleSys.GetComponent<ParticleSystem>();
			p.loop = false;
			p.startLifetime = 0.75f;
			p.startSpeed = 9.5f;
		}

		yield return new WaitForSeconds(delay);
		
		particleSys.gameObject.SetActive(status);
	}
	
	public bool IsBuilt
	{
		get
		{
			return isBuilt;
		}

		set
		{	
			meshObj.gameObject.SetActive(true);
			isBuilt = value;
			StartListenAudio();
		}
	}

	public bool IsConnected
	{
		get
		{
			return isConnected;
		}

		set
		{	
			GridManager.Blink();
			isConnected = value;
			if (value)
			{
				StartCoroutine(ShowBuildParticleCorout(true, true, 2f));
			}
		}
	}

	public bool IsPowered
	{
		get
		{
			return isPowered;
			
		}

		set
		{
			GridManager.TurnOn();
			isPowered = value;
		}
	}

	void Update()
	{
		//
	
		AnalyzeSound(audioSource);

		if (isConnected)
		{
			var acceleration = 0f;

			if (baseVolume > 0f && rotatorSpeed < maxSpeed)
			{
				acceleration = volume - baseVolume > 0.05f ? 0.05f : 0f;
			}

			rotatorSpeed += acceleration;

			if (rotatorSpeed >= maxSpeed)
			{
				rotatorSpeed = maxSpeed;
				IsPowered = true;
			}

			rotator.Rotate(new Vector3(0f, 0f, rotatorSpeed * Time.deltaTime * 22f));
		}

	}



	IEnumerator SetBaseVolume()
	{
		var sampleTime = 3f;
		var sampleTimeCounter = 0f;
		var volList = new List<float>();

		while (sampleTimeCounter < sampleTime)
		{
			volList.Add(volume);
			sampleTimeCounter += Time.deltaTime;
			yield return null;
		}

		baseVolume = volList.Average();
	}
	
	void StartListenAudio()
	{

		if (audioSource.clip == null)
		{
			audioSource.clip = Microphone.Start(Microphone.devices[0], true, 1, 44100);
			audioSource.loop = true;

			while (!(Microphone.GetPosition(null) > 0)) { }
			audioSource.Play();

			StartCoroutine(SetBaseVolume());
			thisIsPrimary = true;
		}
	}

	void AnalyzeSound(AudioSource MusicSource)
	{	
	
		float[] data = new float[SampleSize];
		audioSource.GetOutputData(data, 0);
		//take the median of the recorded samples
		ArrayList s = new ArrayList();
		foreach (float f in data)
		{
			s.Add(Mathf.Abs(f));
		}
		s.Sort();
		volume = (float)s[SampleSize / 2];

	}
}
