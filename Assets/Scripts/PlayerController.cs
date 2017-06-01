using UnityEngine;

namespace Assets.Scripts
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class PlayerController : MonoBehaviour
	{

		public float moveSpeed = 6;

		public float jumpHeight = 4;

		public float timeToJumpApex = 0.4f;

		float accelerationTimeAirBorne = 0.2f;
		float accelerationTimeAirGrounded = 0.1f;

		float jumpVelocity;
		float gravity;

		float velocityXSmoothing;

		Vector3 velocity;
		bool collidingAbove, collidingBelow, collidingRight, collidingLeft;

		BoxCollider2D boxCollider;

		#region equations

		/*
                        2 * jumpHeight
            gravity = -------------------
                        timeToJumpApex²

            jumpVelocity = gravity * timeToJumpApex;
        */

		#endregion

		// Use this for initialization
		void Start()
		{
			gravity = (2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
			jumpVelocity = gravity * timeToJumpApex;

			// use negativ because gravity is a downwards acceleration
			Physics2D.gravity = new Vector2(0, -gravity);

			collidingBelow = collidingAbove = collidingLeft = collidingRight = false;
		}

		void FixedUpdate()
		{
			if (collidingAbove || collidingBelow)
			{
				velocity.y = 0;
			}

			if (collidingLeft || collidingRight)
			{
				velocity.x = 0;
			}

			Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

			if (Input.GetKeyDown(KeyCode.Space) && collidingBelow)
			{
				velocity.y += jumpVelocity;
			}

			float targetVelocityX = input.x * moveSpeed;

			// TODO: change time for smoothing based on info if in air or not
			velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, accelerationTimeAirGrounded);

			Move();
		}

		void Move()
		{
			transform.Translate(velocity * Time.deltaTime);
		}

		void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.tag == ColorJumperConstants.OBSTACLE)
			{
				switch (collision.otherCollider.gameObject.tag)
				{
					case ColorJumperConstants.COLLISION_TOP:
						collidingAbove = true;
						break;
					case ColorJumperConstants.COLLISION_BOTTOM:
						collidingBelow = true;
						break;
					case ColorJumperConstants.COLLISION_LEFT:
						collidingLeft = true;
						break;
					case ColorJumperConstants.COLLISION_RIGHT:
						collidingRight = true;
						break;
				}
			}
		}

		void OnCollisionExit2D(Collision2D collision)
		{
			if (collision.gameObject.tag == ColorJumperConstants.OBSTACLE)
			{
				switch (collision.otherCollider.gameObject.tag)
				{
					case ColorJumperConstants.COLLISION_TOP:
						collidingAbove = false;
						break;
					case ColorJumperConstants.COLLISION_BOTTOM:
						collidingBelow = false;
						break;
					case ColorJumperConstants.COLLISION_LEFT:
						collidingLeft = false;
						break;
					case ColorJumperConstants.COLLISION_RIGHT:
						collidingRight = false;
						break;
				}
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
