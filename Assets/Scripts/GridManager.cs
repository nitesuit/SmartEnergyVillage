using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour {

	public static GridManager instance;
	public Light[] Lights;
	public List<GameObject> GridObjects;
	private bool _shouldBlink = false;
	public float MaxIntensity = 4.0f;
	public float MinIntensity = 1.0f;

	void Awake() {
		if (GridObjects == null) {
			GridObjects = new List<GameObject>();
		}
		Lights = FindObjectsOfType<Light> (); 
		if (instance == null)
		{ instance = this; }
		else
		{ Destroy(instance); }
	}

	void Update() {
		if (_shouldBlink) {
			foreach (Light l in Lights) {
				if (l.type != LightType.Point) {
					continue;
				}
				setEmission (l.transform.parent.gameObject, (float)Random.Range (MinIntensity, MaxIntensity - 1.5f));
				l.intensity = ((float)Random.Range (MinIntensity, MaxIntensity - 1.5f));
			}
			foreach (GameObject go in GridObjects) {
				setEmission (go, (int)Random.Range (MinIntensity, MaxIntensity));
			}
		}
	}

	int GetEnergy() {
		return GridObjects.Count * 25;
	}


	//	public void Add(GameObject gameObjext) 
	//		GridObjects.Add(gameObject);
	//	}

	public void Blink() {
		_shouldBlink = true;
	}

	public void TurnOn() {
		_shouldBlink = false;
		foreach (Light l in Lights) {
			if (l.type != LightType.Point)  {
				continue;
			}
			setEmission (l.transform.parent.gameObject, MaxIntensity);
			l.intensity = (MaxIntensity);
		}
		foreach (GameObject go in GridObjects) {
			setEmission (go, MaxIntensity);
		}
	}

	public void TurnOff() {
		_shouldBlink = false;
		foreach (Light l in Lights) {
			if (l.type != LightType.Point)  {
				continue;
			}
			setEmission (l.transform.parent.gameObject, MinIntensity);
			l.intensity = (MinIntensity);

		}
		foreach (GameObject go in GridObjects) {
			setEmission (go, MinIntensity);
		}
	}

	private void setEmission(GameObject go, float intensity) {
		Color baseColor = Color.white; //Replace this with whatever you want for your base color at emission level '1'
		Material mat = go.GetComponent<Renderer>().material;
		Color finalColor = baseColor * Mathf.LinearToGammaSpace (intensity * 100);
		if (intensity == MinIntensity) {
			finalColor = baseColor;
		}
		mat.EnableKeyword ("_EMISSION");
		mat.SetColor ("_EmissionColor", finalColor);
	}
}
