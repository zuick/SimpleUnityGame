﻿using UnityEngine;
using System.Collections;

public class Digging : MonoBehaviour {

	public float digTime = 2;
	
	private bool isMovementAttempted = false;
	private GameObject objectToDig;
	private bool grounded;
	private float moveX = 0f;

	void FixedUpdate()
	{
		moveX = Input.GetAxis ("Horizontal");
		isMovementAttempted = !IsAlmostZero(moveX);
		grounded = GetComponent<Movement>().grounded;
	}

	bool IsAlmostZero(float zeroCandidate)
	{
		const float epsylon = 0.1f;
		return Mathf.Abs(zeroCandidate) < epsylon;
	}

	void StartOrContinueDigging(GameObject newObjectToDig) 
	{
		if (objectToDig != newObjectToDig) {
			objectToDig = newObjectToDig;
			
			Invoke("FinishedDigging", digTime);
		}
	}
	
	void FinishedDigging()
	{
		Destroy(objectToDig);
		StopDigging();
	}
	
	void StopDigging()
	{
		CancelInvoke("FinishedDigging");
		if (objectToDig != null)
			objectToDig = null;
	}
	
	void OnCollisionStay2D(Collision2D collision) 
	{
		moveX = Input.GetAxis ("Horizontal");
		if (grounded && collision.gameObject.tag == "DiggableWall") {
			if (isMovementAttempted) {
				foreach (var contact in collision.contacts) {
					if (moveX == -contact.normal.x && contact.normal.y == 0) 
						StartOrContinueDigging(collision.gameObject);
				}
			} else {
				StopDigging();
			}
		}
	}
	
	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject == objectToDig) {
			StopDigging();
		}
	}
}