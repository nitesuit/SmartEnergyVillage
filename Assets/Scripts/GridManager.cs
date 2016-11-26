using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour {

	public enum GridState { On, Off, Blink }
	public GridState State {
		set {
			State = value;
			if (State == GridState.On) {
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Blink() {
		
	}

	void TurnOn() {
		
	}

	void TurnOff() {
		
	}
}
