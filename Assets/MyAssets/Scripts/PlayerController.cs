using Assets.MyAssets.Scripts;
using Assets.MyAssets.Scripts.PowerUps;
using UnityEngine;

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

        [Header("Jumping")]
        public float maxJumpHeight = 4;
        public float minJumpHeight = 1;
        public float timeToJumpApex = .4f;

        [Header("Moving")]
        public float moveSpeed = 6;

        float accelerationTimeAirborne = .2f;
        float accelerationTimeGrounded = .1f;


        float gravity;

        [HideInInspector]
        public Vector3 velocity;

        float velocityXSmoothing;

        [HideInInspector]
        public float maxJumpVelocity;

        float minJumpVelocity;

        [HideInInspector]
        public int jumpCount = 0;

        // used for the animation of the split power up
        // the controls will be disabled, to just have the gravity
        bool controlsEnabled = true;


        // used for tutorial
        [HideInInspector]
        public bool moveSidewaysAllowed = true;

        [HideInInspector]
        public Controller2D controller;

        [System.NonSerialized]
        public SplitPowerUp splitPowerUp;

        public float ShrinkAmount = 0.8f;

        void Start()
        {
            controller = GetComponent<Controller2D>();

            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
            maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
            minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

            splitPowerUp = new SplitPowerUp();
        }

        void Update()
        {
            #region MovingAndJumping

            Vector2 input = Vector2.zero;

            if (controlsEnabled)
            {
                input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

                if (jumpCount == 0)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
                    {
                        velocity.y = maxJumpVelocity;
                        jumpCount++;
                    }
                }
                else if (jumpCount == 1)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        velocity.y = maxJumpVelocity;
                        jumpCount++;

                        // player is currently in double jump
                        // the player can use the split power up now

                        // trigger it if the user hits the power-up key (enter key)
                        splitPowerUp.CanBeUsed = true;
                    }

                }

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    if (velocity.y > minJumpVelocity)
                    {
                        velocity.y = minJumpVelocity;
                    }
                }
            }

            if (moveSidewaysAllowed)
            {
                float targetVelocityX = input.x * moveSpeed;
                velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime, input);

            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0;
                jumpCount = 0;

                splitPowerUp.CanBeUsed = false;

                if (!controlsEnabled)
                {
                    // this is an old player prefab, which should be destroyed once hitting a collider
                    Destroy(gameObject);
                }
            }

            #endregion

            #region PowerUpHandling

            if (Input.GetKeyDown(KeyCode.Return) && controlsEnabled)
            {
                if (splitPowerUp.Count > 0 && splitPowerUp.CanBeUsed)
                {
                    TriggerSplitPowerUp();
                }
            }

            #endregion
        }

        private void TriggerSplitPowerUp()
        {
            // reduce size of the player by 1/3
            gameObject.transform.localScale *= ShrinkAmount;

            // create new player game object the simulate destroying of old one
            GameObject oldPlayer = (GameObject)Instantiate(Resources.Load("Player"));
            oldPlayer.transform.localScale *= ShrinkAmount;

            // set poistion to current player pos
            oldPlayer.transform.SetPositionAndRotation(transform.position, transform.rotation);
            // apply current color
            oldPlayer.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
            // disable controls
            oldPlayer.GetComponent<PlayerController>().controlsEnabled = false;

            // apply the extra jump
            velocity.y = maxJumpVelocity;

            splitPowerUp.Count = splitPowerUp.Count - 1;

            UIManager.Instance.UpdateSplitPowerUpCount(splitPowerUp.Count);
        }


        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == ColorJumperConstants.COLOR_CHANGER_LIGHT)
            {
                GameManager.Instance.ChangeColor(2);
            }
            else if (col.gameObject.tag == ColorJumperConstants.COLOR_CHANGER_DARK)
            {
                GameManager.Instance.ChangeColor(1);
            }
            else if (col.gameObject.tag == ColorJumperConstants.DEATH_TRIGGER)
            {
                GameManager.Instance.ApplyPlayerDeath();
                splitPowerUp.Count = 0;
            }
            else if (col.gameObject.tag == ColorJumperConstants.FINISH)
            {
                GameManager.Instance.Finish();
            }
            else if (col.gameObject.tag == ColorJumperConstants.MOVING_PLATFORM)
            {
                transform.SetParent(col.transform);
            }
            else if (col.gameObject.CompareTag(ColorJumperConstants.POWER_UP))
            {
                IPowerUp powerUp = GameManager.Instance.TakePowerUp(col.gameObject);

                if (powerUp is SplitPowerUp)
                {
                    splitPowerUp = powerUp as SplitPowerUp;
                }
            }
            else if (col.name == ColorJumperConstants.TUT_GOAL_COLLIDER)
            {
                FindObjectOfType<TutorialScript>().tutorialGoalColliderHit = true;
            }

            else if (col.gameObject.CompareTag(ColorJumperConstants.ENEMY))
            {
                GameManager.Instance.ApplyPlayerDeath();
                splitPowerUp.Count = 0;
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.tag == ColorJumperConstants.MOVING_PLATFORM)
            {
                transform.SetParent(null);
            }
        }
    }
}
