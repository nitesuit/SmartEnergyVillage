using UnityEngine;
using System.Collections;

public class TurbineManager : MonoBehaviour {

	public Transform rotator;
	public Transform meshObj;
	public float volume;
	public float rotatorSpeed;

	

	private int SampleSize = 256;
	//
	private bool isBuilt;
	private bool isConnected;

	AudioSource audioSource;

	// Use this for initialization
	void Start () {

		audioSource = GetComponent<AudioSource>();
		meshObj.gameObject.SetActive(false);
	}
	

	void OnGUI()
	{
		var style = new GUIStyle();
		style.fontSize = 36;
		GUI.Label(new Rect(400f, 100f, 400f, 100f) ,"Dec: " + volume, style );
	}

	void OnMouseDown()
	{
		IsBuilt = true;
		IsConnected = true;
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
		if (volume > 0.008f)
		{
			rotatorSpeed += 1.5f;
		}
		else if (rotatorSpeed > 0f)
		{
			rotatorSpeed -= 0.01f;
		}

		AnalyzeSound(audioSource);

		if (IsBuilt && IsConnected)
		{	
			rotator.Rotate(new Vector3(0f, 0f, rotatorSpeed* Time.deltaTime));
			
		}

	}
	
	void StartListenAudio()
	{
		audioSource.clip = Microphone.Start(Microphone.devices[0], true, 1, 44100);
		audioSource.loop = true;

		while (!(Microphone.GetPosition(null) > 0)) { }
		audioSource.Play();
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
