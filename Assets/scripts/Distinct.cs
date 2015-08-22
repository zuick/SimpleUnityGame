using UnityEngine;
using System.Collections;

public class Distinct : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D( Collision2D col ){
		if (col.gameObject.tag == "Player") {
			if( Application.loadedLevelName == "scene1" )
				Application.LoadLevel("scene2");
			else
				Application.Quit();
		}
	}
}
