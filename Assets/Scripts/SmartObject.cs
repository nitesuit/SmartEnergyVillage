using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]
public class SmartObject : MonoBehaviour {

	public Transform wireConnector;
	
	void OnMouseDown() {
		GameManager gameManager = GameManager.instance;
		gameManager.Select (gameObject);
	}
}
