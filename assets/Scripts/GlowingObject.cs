using UnityEngine;

public class GlowingObject : MonoBehaviour {
	public bool ShouldGlow { get; set; }
	public GameObject spotlight;

	void Update() {
		if (!ShouldGlow) {
			return;
		}

		float emission = Mathf.PingPong (Time.time * 6, 3.0f) + 1;
		spotlight.GetComponent<Light> ().intensity = emission;
	}

}