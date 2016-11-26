using UnityEngine;
using System.Collections;

public class ExampleManager : MonoBehaviour {

	public GameManager GameManager;

	// Use this for initialization
	void Start () {
		Debug.Log (GameManager.NextObject);
		GameManager.CompleteLevel ();
		Debug.Log (GameManager.NextObject);
		GameManager.CompleteLevel ();
		Debug.Log (GameManager.NextObject);
		GameManager.CompleteLevel ();
	}
	

}
