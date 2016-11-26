using UnityEngine;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {

	enum GameScore { Win = 100, Lose = -100, Start = 0};

	public int Score { get; set; }
	public List<GameObject> Objects;
	private GameObject _nextGameObject;
	private int _currentLevel = 0;

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
		
	void Start() {
		GlowingObject glowingObject = NextObject.GetComponent<GlowingObject>();
		glowingObject.ShouldGlow = true;
	}

	void Update() {
		checkGameStatus ();
	}


	public void CompleteLevel() {
		_currentLevel++;
	}


	public void startGame() {
		Score = 0;
		// do something when game started
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
