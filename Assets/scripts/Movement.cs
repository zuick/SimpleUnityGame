﻿using UnityEngine;
using System.Collections;
using System.Timers;

public class Movement : MonoBehaviour {
	public float speed = 3f;
	public float jumpForce = 1000f;
	public float groundCheckRadious = 0.05f;
	public GameObject groundCheck;
	public LayerMask groundLayer;
	public Animator anim;
	
	[HideInInspector] public bool grounded = false;
	[HideInInspector] public float moveX;
	[HideInInspector] public bool jump;	
	[HideInInspector] public bool isWayBlocked = false;

	private bool groundedPrevious = false;
	
	private Rigidbody2D rigidbody2D;
	// Use this for initialization
	void Start (){

		rigidbody2D = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate (){

		//float moveX = Input.GetAxis ("Horizontal");
		if (isWayBlocked) moveX = 0f;

		rigidbody2D.velocity = new Vector2 (moveX * speed, rigidbody2D.velocity.y);

		grounded = Physics2D.OverlapCircle (groundCheck.transform.position, groundCheckRadious, groundLayer);

		if (!groundedPrevious && grounded) {// grounded changed to true
			anim.SetBool ("landing", true);
		} else {
			anim.SetBool("landing", false );
		}

		groundedPrevious = grounded;

		//AttemptDigging (moveX);
	}

	void Update(){

		//bool jump = Input.GetButtonDown("Jump");

		if (jump && grounded) {
			rigidbody2D.AddForce (new Vector2 (0f, jumpForce));
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			anim.SetBool ("talking", true);
		}

		if (Input.GetKeyUp (KeyCode.E)) {
			anim.SetBool ("talking", false);
		}		
	}
}
