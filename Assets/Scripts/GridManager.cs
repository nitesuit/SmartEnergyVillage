using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour {

	public static GridManager instance;
	public Light[] Lights;
	public GameObject[] GridObjects;
	private bool _shouldBlink = false;
	private int _maxIntensity = 8;
	private int _minIntensity = 1;

	void Awake() {
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
				l.intensity = ((int)Random.Range (_minIntensity, _maxIntensity));
			}
		}
	}

	public void Blink() {
		_shouldBlink = true;
	}

	public void TurnOn() {
		_shouldBlink = false;
		foreach (Light l in Lights) {
			if (l.type != LightType.Point)  {
				continue;
			}
			l.intensity = (_minIntensity);
		}
	}

	public void TurnOff() {
		_shouldBlink = false;
		foreach (Light l in Lights) {
			if (l.type != LightType.Point)  {
				continue;
			}
			l.intensity = (_maxIntensity);
		}
	}
}
