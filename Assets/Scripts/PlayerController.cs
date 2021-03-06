﻿using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Controller2D))]
    public class PlayerController : MonoBehaviour
	{
        #region equations

        /*
                        2 * jumpHeight
            gravity = -------------------
                        timeToJumpApex²

            jumpVelocity = gravity * timeToJumpApex;
        */

        #endregion

        public float maxJumpHeight = 4;
        public float minJumpHeight = 1;
        public float timeToJumpApex = .4f;
        float accelerationTimeAirborne = .2f;
        float accelerationTimeGrounded = .1f;
        float moveSpeed = 6;

        float gravity;
        Vector3 velocity;
        float velocityXSmoothing;

        float maxJumpVelocity;
        float minJumpVelocity;


        Controller2D controller;

        void Start()
        {
            controller = GetComponent<Controller2D>();

            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
            maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
            minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        }

        void Update()
        {

            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
            {
                velocity.y = maxJumpVelocity;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (velocity.y > minJumpVelocity)
                {
                    velocity.y = minJumpVelocity;
                }
            }

            float targetVelocityX = input.x * moveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime, input);


            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0;
            }
        }

        void OnTriggerEnter2D(Collider2D col)
		{
			if (col.gameObject.tag == ColorJumperConstants.COLOR_CHANGER_YELLOW)
			{
				GameMaster.Instance.ChangeColor(ColorEnum.Yellow);
			}
			else if (col.gameObject.tag == ColorJumperConstants.COLOR_CHANGER_BLUE)
			{
				GameMaster.Instance.ChangeColor(ColorEnum.Blue);
			}
		}
	}
}
