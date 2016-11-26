using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]
public class SmartObject : MonoBehaviour {

	void OnMouseDown() {
		GameManager gameManager = GameManager.instance;
		gameManager.Select (gameObject);
	}
}
