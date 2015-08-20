using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public float speed = 5f;
	public float jumpForce = 1000f;
	private Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start () 
	{
		rigidbody2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		float moveX = Input.GetAxis ("Horizontal");

		rigidbody2D.velocity = new Vector2 (moveX * speed, rigidbody2D.velocity.y);
	}

	void Update()
	{
		bool jump = Input.GetButtonDown("Jump");
		if (jump) 
		{
			rigidbody2D.AddForce( new Vector2( 0f, jumpForce ) );
		}

	}
}
