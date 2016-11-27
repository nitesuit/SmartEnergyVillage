using UnityEngine;
using System.Collections;

public class CloudLerp : MonoBehaviour {
	public bool ShouldMove { get; set; }

	void Update() {
		if (ShouldMove) {
			return;
		}
		float finalSpeed = Time.deltaTime * 5f;
		transform.position = new Vector3 (transform.position.x - finalSpeed, transform.position.y, transform.position.z);
	}
}