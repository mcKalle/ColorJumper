﻿using UnityEngine;

namespace Assets.Scripts
{

	[RequireComponent(typeof(Controller2D))]
	public class PlayerController : MonoBehaviour
	{




		//old impl

		//public float moveSpeed;
		//public float jumpVelocity;

		//public float fallMultiplier = 2.5f;
		//public float lowJumpMultiplier = 2f;

		//public Camera MainCamera;

		//Rigidbody2D rigidbody;

		//void Awake()
		//{
		//	rigidbody = GetComponent<Rigidbody2D>();
		//}

		//// Use this for initialization
		//void Start()
		//{

		//}

		//// Update is called once per frame
		//void FixedUpdate()
		//{
		//	HandlingJump();

		//	HandlingMovement();

		//}

		//void OnTriggerEnter2D(Collider2D col)
		//{
		//	if (col.gameObject.tag == ColorJumperConstants.COLOR_CHANGER_TAG) 
		//	{
		//		Debug.Log("Entering color changer.");


		//		if (MainCamera != null)
		//		{
		//			MainCamera.backgroundColor = Color.blue;
		//		}
		//		else
		//		{
		//			Debug.Log("Cam is null.");
		//		}
		//	}
		//}

		//#region Movement and Jumping

		//void HandlingJump()
		//{
		//	if (Input.GetKeyDown(KeyCode.Space))
		//	{
		//		rigidbody.velocity = Vector2.up * jumpVelocity;
		//	}

		//	// if falling downwards
		//	if (rigidbody.velocity.y < 0)
		//	{
		//		// - 1 because unity physics already adds one time of the gravitx
		//		// Time.deltaTime to match it to the frames

		//		rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		//	}
		//	// if jumping upward and not holding down the jump key
		//	else if (rigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
		//	{
		//		rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		//	}
		//}

		//void HandlingMovement()
		//{
		//	float horizontal = Input.GetAxis("Horizontal");

		//	rigidbody.velocity = new Vector2(horizontal * moveSpeed, rigidbody.velocity.y);
		//}

		//#endregion
	}
}
