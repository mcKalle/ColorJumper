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

            boxCollider = GetComponent<BoxCollider2D>();
        }

        void Update()
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (Input.GetKeyDown(KeyCode.Space) && collidingBelow)
            {
                velocity.y = jumpVelocity;
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
               // var relativePosition = transform.InverseTransformPoint(collision.transform.position);

                collidingBelow = collidingAbove = collidingLeft = collidingRight = false;

                //if (relativePosition.x > 0)
                //{
                //    collidingRight = true;
                //}
                //else
                //{
                //    collidingLeft = true;
                //}
                //if (relativePosition.y > 0)
                //{
                //    collidingAbove = true;
                //}
                //else
                //{
                //    collidingBelow = true;
                //}

                //Debug.Log(string.Format("left: {0}, right: {1}, top: {2}, bottom: {3}", collidingLeft, collidingRight, collidingAbove, collidingBelow));
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == ColorJumperConstants.OBSTACLE)
            {
                //var relativePosition = transform.InverseTransformPoint(collision.transform.position);

                collidingBelow = collidingAbove = collidingLeft = collidingRight = false;

                //if (relativePosition.x > 0)
                //{
                //    collidingRight = false;
                //}
                //else
                //{
                //    collidingLeft = false;
                //}
                //if (relativePosition.y > 0)
                //{
                //    collidingAbove = false;
                //}
                //else
                //{
                //    collidingBelow = false;
                //}

               // Debug.Log(string.Format("left: {0}, right: {1}, top: {2}, bottom: {3}", collidingLeft, collidingRight, collidingAbove, collidingBelow));
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
