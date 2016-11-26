using UnityEngine;

public class GlowingObject : MonoBehaviour {
	public bool ShouldGlow { get; set; }
	public GameObject spotlight;

	void Update() {
		if (!ShouldGlow) {
			return;
		}

//		Renderer renderer = GetComponent<Renderer> ();
//		Material mat = renderer.material;
//
		float emission = Mathf.PingPong (Time.time * 6, 7.0f);
//		Color baseColor = Color.white; //Replace this with whatever you want for your base color at emission level '1'
//
//		Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
//		mat.EnableKeyword ("_EMISSION");
//		mat.SetColor ("_EmissionColor", finalColor);
//		DynamicGI.SetEmissive(renderer, finalColor);
		spotlight.GetComponent<Light> ().intensity = emission;
	}

}