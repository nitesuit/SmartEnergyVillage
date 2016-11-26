using UnityEngine;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {
	
	public int Score { get; set; }
	public List<GameObject> Objects { get; set; }


	public GameObject GetNextObject () {
		return new GameObject (); 
	}
}
