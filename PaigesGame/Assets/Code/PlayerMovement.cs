using UnityEngine;
using Assets.Code;
using Assets.Code.Enums;
using System.Collections;

namespace Assets.Code
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Member Variables
        /// <summary>
        /// Player movement speed
        /// </summary>
        private float movementSpeed = 0.25f;

        /// <summary>
        /// Animation state machine local reference
        /// </summary>
        private Animator animator;

        /// <summary>
        /// The last position of the player in previous frame
        /// </summary>
        private Vector3 lastPosition;

        /// <summary>
        /// The last checkpoint position that we have saved
        /// </summary>
        private Vector3 CheckPointPosition;

        /// <summary>
        /// Is the player dead?
        /// </summary>
        private bool isDead = false;
        #endregion

        // Use this for initialization
        void Start()
        {
            // get the local reference
            animator = GetComponent<Animator>();

            // set initial position
            lastPosition = transform.position;
            CheckPointPosition = transform.position;
        }

        /// <summary>
        /// 1 - The speed of the ship
        /// </summary>
        public Vector2 speed = new Vector2(5, 5);

        // 2 - Store the movement
        private Vector2 movement;

        public Rigidbody2D body;

        void OnStart()
        {
        }

        // Update is called once per frame
        void Update()
        {
            // 3 - Retrieve axis information
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");

            // 4 - Movement per direction
            movement = new Vector2(
                speed.x * inputX,
                speed.y * inputY);
            UnityEngine.Debug.Log("movement=" + movement.ToString());
            UnityEngine.Debug.Log("speed.x=" + speed.x);
            UnityEngine.Debug.Log("speed.y=" + speed.y);
            UnityEngine.Debug.Log("inputX=" + inputX);
            UnityEngine.Debug.Log("inputY=" + inputY);

            // horizontal movement, left or right, set animation type and speed 
            if (inputX > 0)
            {
                animator.SetInteger("Direction", (int)MoveAnimDirection.WalkRight);
                animator.speed = movementSpeed;
            }
            else if (inputX < 0)
            {
                animator.SetInteger("Direction", (int)MoveAnimDirection.WalkLeft);
                animator.speed = movementSpeed;
            }

            // vertical movement, up or down, set animation type and speed 
            if (inputY > 0)
            {
                animator.SetInteger("Direction", (int)MoveAnimDirection.WalkUp);
                animator.speed = movementSpeed;
            }
            else if (inputY < 0)
            {
                animator.SetInteger("Direction", (int)MoveAnimDirection.WalkDown);
                animator.speed = movementSpeed;
            }

            UnityEngine.Debug.Log("animator.speed=" + animator.speed);

            body.velocity = movement;
            UnityEngine.Debug.Log("body.velocity=" + body.velocity);

            //compare this position to the last known one, are we moving?
            if (this.transform.position == lastPosition)
            {
                // we aren't moving so make sure we dont animate
                animator.speed = 0.0f;
            }

            // get the last known position
            lastPosition = transform.position;

            // if we are dead do not move anymore
            if (isDead == true)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                animator.speed = 0.0f;
            }
        }

        void FixedUpdate()
        {
            // 5 - Move the game object
            body.velocity = movement;
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "DangerousTile")
            {
                GameObject.Find("FadePanel").GetComponent<FadeScript>().RespawnFade();
                isDead = true;
            }
            else if (collider.gameObject.tag == "LevelChanger")
            {
                GameObject.Find("FadePanel").GetComponent<FadeScript>().FadeOut();
                isDead = true;
            }
        }

        /// <summary>
        /// Respawns the player at checkpoint.
        /// </summary>
        public void RespawnPlayerAtCheckpoint()
        {
            // if we hit a dangerous tile then we are dead so go to the checkpoint position that was last saved
            transform.position = CheckPointPosition;
            isDead = false;
        }
    }
}
