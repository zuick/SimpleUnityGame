using UnityEngine;
using System.Collections;

public class Digging : MonoBehaviour {

	public float digTime = 2;
	public GameObject DiggingParticleSystem;
	
	private bool isMovementAttempted = false;
	private GameObject objectToDig;
	private bool grounded;
	private bool isWayBlocked = false; // make use or remove
	private float moveX = 0f;
	private float moveY = 0f;
	private Movement movementComponent;

	void Awake()
	{
		movementComponent = GetComponent<Movement>();
	}

	void FixedUpdate()
	{
		moveX = Input.GetAxis("Horizontal");
		moveY = Input.GetAxis("Vertical");
		isMovementAttempted = !IsAlmostZero(moveX) || !IsAlmostZero(moveY);
		grounded = movementComponent.grounded;
	}

	bool IsAlmostZero(float zeroCandidate)
	{
		const float epsylon = 0.1f;
		return Mathf.Abs(zeroCandidate) < epsylon;
	}

	void StartOrContinueDigging(GameObject newObjectToDig) 
	{
		if (objectToDig != newObjectToDig) {
			isWayBlocked = true;
			objectToDig = newObjectToDig;

			Invoke("FinishedDigging", digTime);

			DiggingParticleSystem.transform.position = newObjectToDig.transform.position;
			DiggingParticleSystem.SetActive(true);
			DiggingParticleSystem.GetComponent<ParticleSystem>().Play();
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
		isWayBlocked = false;
		DiggingParticleSystem.SetActive(false);

		if (objectToDig != null)
			objectToDig = null;
	}
	
	void OnCollisionStay2D(Collision2D collision) 
	{
		//moveX = Input.GetAxis ("Horizontal");
		if (!(IsAlmostZero(moveX)^IsAlmostZero(moveY))) {
			StopDigging();
			return;
		}

		if (grounded && collision.gameObject.tag == "DiggableWall") {
			if (isMovementAttempted) {
				foreach (var contact in collision.contacts) {
					if (moveX == -contact.normal.x && IsAlmostZero(contact.normal.y) || moveY == -contact.normal.y && IsAlmostZero(contact.normal.x)) {
						StartOrContinueDigging(collision.gameObject);
						break;
					}
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
