using UnityEngine;
using System.Collections;

public class DiggingParticles : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Particles";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
