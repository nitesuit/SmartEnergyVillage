using UnityEngine;
using System.Collections;

public class CloudLerp : MonoBehaviour {
	public bool ShouldMove { get; set; }
	
	public float speed = 6.5f;
	
	void Update() {
		if (!ShouldMove) {
			return;
		}
		float finalSpeed = Time.deltaTime * speed;
		transform.position = new Vector3 (transform.position.x - finalSpeed, transform.position.y, transform.position.z);
	}
}