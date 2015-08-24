using UnityEngine;
using System.Collections;

public class PlayerControlls : MonoBehaviour {
	
	public GameObject PlayerObject;

	private Movement movementComponent;

	// Use this for initialization
	void Start () {
		if (PlayerObject != null)
			movementComponent = PlayerObject.GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () {
		if (movementComponent != null) {
			movementComponent.moveX = Input.GetAxis ("Horizontal");
			movementComponent.jump = Input.GetButtonDown("Jump");
		}
	}
}
