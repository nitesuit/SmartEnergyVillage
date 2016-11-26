using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TurbineManager : MonoBehaviour
{

	public Transform rotator;
	public Transform meshObj;
	public Transform particleSys;
	public float volume;
	public float rotatorSpeed;

	public float fixedBuiltTime = 0.75f;
	//
	public float baseVolume;

	private float buildTimer;
	private float maxSpeed = 5f;
	private int SampleSize = 256;
	//
	private bool isBuilt;
	private bool isConnected;

	private float minYPosition = 5.5f;
	//
	
	AudioSource audioSource;

	// Use this for initialization
	void Start()
	{

		audioSource = GetComponent<AudioSource>();
		meshObj.gameObject.SetActive(false);
		StartCoroutine(ShowBuildParticleCorout(false));

	}


	void OnGUI()
	{
		var style = new GUIStyle();
		style.fontSize = 36;

		var strText = string.Format(" Vol : {0} \n bVol : {1} \n speed : {2}", volume, baseVolume, rotatorSpeed);

		GUI.Label(new Rect(400f, 100f, 400f, 400f), strText, style);
	}
	
	public bool isReady;

	void OnMouseDrag()
	{
		if (!isReady)
		{
			StartCoroutine(ShowBuildParticleCorout(true, false, 0f));
			IsBuilt = true;

			buildTimer += Time.deltaTime;
		}

		if (buildTimer > fixedBuiltTime && !isReady)
		{
			StartCoroutine(ShowBuildParticleCorout(false, true, 1.75f));
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
			p.startLifetime = 1.5f;
			p.startSpeed = 4.5f;
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
			isConnected = value;
			StartListenAudio();
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
				acceleration = volume - baseVolume > 0.15f ? 0.05f : 0f;
			}

			rotatorSpeed += acceleration;

			if (rotatorSpeed >= maxSpeed)
			{
				rotatorSpeed = maxSpeed;
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
		audioSource.clip = Microphone.Start(Microphone.devices[0], true, 1, 44100);
		audioSource.loop = true;

		while (!(Microphone.GetPosition(null) > 0)) { }
		audioSource.Play();

		StartCoroutine(SetBaseVolume());
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
