using UnityEngine;
using System.Collections;

public class SmartObject : MonoBehaviour {

	void OnMouseDown() {
		GameObject gameManagerObject = GameObject.FindGameObjectWithTag (GameManager.ManagerTag);
		GameManager gameManager = gameManagerObject.GetComponent<GameManager> ();
		gameManager.Select (gameObject);
	}
}
