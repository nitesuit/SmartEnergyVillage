using UnityEngine;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {

	enum GameScore { Win = 100, Lose = -100, Start = 0};

	public int Score { get; set; }
	public List<GameObject> Objects;
	private List<GameObject> _connectedObjects;
	private GameObject _nextGameObject;
	private int _currentLevel = 0;
	public static string ManagerTag = "GameManager";

	void Update() {
		checkGameStatus ();
	}

	void Awake() {
		_connectedObjects = new List<GameObject> ();
	}


	public GameObject NextObject
	{
		get
		{
			if (Objects.Count > _currentLevel) {
				return Objects[_currentLevel];
			} else {
				Debug.Log ("No more game objects. Resetting level to 0");
				_currentLevel = 0;
				return Objects[_currentLevel];
			}
		}
	}

	public void CompleteLevel() {
		_currentLevel++;
	}


	public void startGame() {
		Score = 0;
		// do something when game started
	}

	// Connecting objects 

	public void Select(GameObject gameObject) {
		if (_connectedObjects.Count == 2) {
			LineHelper.Connect (_connectedObjects [0], _connectedObjects [1]);
			_connectedObjects = new List<GameObject> ();
			return;
		}
		_connectedObjects.Add (gameObject);
	}

	//Private functions

	private void checkGameStatus() {
		if (Score == (int)GameScore.Lose) {
			gameLost ();
		}

		if (Score == (int)GameScore.Win) {
			gameWon ();
		}
	}

	private void gameLost() {
		Debug.Log ("Game Lost");
	}

	private void gameWon() {
		Debug.Log ("Game Won");
	}
	
}
