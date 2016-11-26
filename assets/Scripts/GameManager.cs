using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	enum GameScore { Win = 100, Lose = -100, Start = 0};

	public int Score { get; set; }
	public List<GameObject> Objects;
	public static string ManagerTag = "GameManager";
	public Material WireMaterial;
	public GameObject selectedEffect;

	private List<GameObject> _selectedObjects;
	private GameObject _nextGameObject;
	private int _currentLevel = 0;


	void Start() {
		startGame ();
	}


	void Update() {
		checkGameStatus ();

	}

	void Awake() {
		if (instance == null)
		{ instance = this; }
		else
		{ Destroy(instance); }
	

		_selectedObjects = new List<GameObject> ();
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
		setGlow (true);
		// do something when game started
	}

	private void setGlow(bool shouldGlow) {
		if (NextObject == null) {
			return;
		}
		GlowingObject glowingObject = NextObject.GetComponent<GlowingObject>();
		glowingObject.ShouldGlow = true;
	}

	// Connecting objects 

	public void Select(GameObject gameObject) {
//		if (_selectedObjects.Count == 1) {
//			if (_selectedObjects.First().Equals(gameObject) ) {
//				Destroy(gameObject.transform.FindChild (selectedEffect.name).transform.gameObject);
//				_selectedObjects = new List<GameObject> ();
//				return;
//			}
//		}
		var particles = Instantiate(selectedEffect, gameObject.transform) as GameObject;
		particles.transform.SetParent (gameObject.transform);
		_selectedObjects.Add (gameObject);


		if (_selectedObjects.Count == 2) {
			removeParticleSystems (_selectedObjects);

			if (_selectedObjects[0] == gameObject) {
				_selectedObjects = new List<GameObject> ();
				return;
			}
			LineHelper.Connect (_selectedObjects [0], _selectedObjects [1]);

			_selectedObjects = new List<GameObject> ();
			return;
		}
	}

	private void removeParticleSystems(List<GameObject> selectedObjects) {
		foreach (var obj in selectedObjects) {
			var particleSystem = obj.GetComponentInChildren<ParticleSystem>();
			if (particleSystem) {
				Destroy(particleSystem.gameObject);
			}
		}
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
