﻿using Assets.Code.Enums;
using Assets.Code.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        private List<SpriteRenderer> AllChildSprites;
        //public GameObject CharacterBody;

        bool isFacingLeft = true;
        #endregion

        // Use this for initialization
        void Start()
        {
            // get the local reference
            animator = GetComponent<Animator>();

            // set initial position
            lastPosition = transform.position;
            CheckPointPosition = transform.position;

            AllChildSprites = this.GetComponentsInChildren<SpriteRenderer>().ToList();
        }

        /// <summary>
        /// 1 - The speed of the ship
        /// </summary>
        public Vector2 speed = new Vector2(1f, 1f);

        // 2 - Store the movement
        private Vector2 movement;

        public Rigidbody2D body;

        void OnStart()
        {
        }

        private Vector3? GetInputPosition()
        {
            if (Input.touchSupported
                && Input.touchCount > 0
                && Application.platform != RuntimePlatform.WebGLPlayer)
            {
                Touch touch = Input.GetTouch(0);
                //touch.position
                return new Vector3(0, 0, 0);
            }
            else if (Input.GetMouseButton(0))
            {
                return Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            return null;
        }


        private List<MoveInputDirection> getDirectionToInput(Vector3 inputPosition)
        {
            List<MoveInputDirection> inputDirections = new List<MoveInputDirection>();
            // is input a tiny bit away from character - stops jittering.
            if (Math.Abs(inputPosition.x - this.transform.position.x) > 0.25f)
            {   // x increases to the right
                if (inputPosition.x < this.transform.position.x)
                    inputDirections.Add(MoveInputDirection.WalkLeft);
                else if (inputPosition.x > this.transform.position.x)
                    inputDirections.Add(MoveInputDirection.WalkRight);
            }
            if (inputDirections.Count == 0)
                inputDirections.Add(MoveInputDirection.NoMovement);

            if (Math.Abs(inputPosition.y - this.transform.position.y) > 0.25f)
            {
                if (inputPosition.y > this.transform.position.y)
                    inputDirections.Add(MoveInputDirection.WalkUp);
                else if (inputPosition.y < this.transform.position.y)
                    inputDirections.Add(MoveInputDirection.WalkDown);
            }

            if (inputDirections.Count <= 1)
                inputDirections.Add(MoveInputDirection.NoMovement);

            return inputDirections;
        }

        // Update is called once per frame
        void Update()
        {
            MoveInputDirection directionX;
            MoveInputDirection directionY;
            Vector3? inputPosition = GetInputPosition();
            if (inputPosition.HasValue)
            {
                List<MoveInputDirection> inputDirections = getDirectionToInput(inputPosition.Value);
                directionX = inputDirections[0];
                directionY = inputDirections[1];

            }
            else
            {
                directionX = InputCalculator.MovementInputX(Input.GetAxis("Horizontal"));
                directionY = InputCalculator.MovementInputY(Input.GetAxis("Vertical"));
            }

            float xMovement = (speed.x * (float)directionX) * 5f;
            float yMovement = (speed.y * (float)directionY) * 5f;

            // 4 - Movement per direction
            movement = new Vector2(xMovement, yMovement);
                        
            //if (directionX == MoveInputDirection.WalkRight)
            //{
                //animator.SetInteger("Direction", (int)MoveAnimDirection.WalkRight);
                //animator.speed = 0.5f;
            //}
            //else if (directionX == MoveInputDirection.WalkLeft)
            //{
                //animator.SetInteger("Direction", (int)MoveAnimDirection.WalkLeft);
                //animator.speed = 0.5f;
            //}
            
            //if (directionY == MoveInputDirection.WalkUp)
            //{
                //animator.SetInteger("Direction", (int)MoveAnimDirection.WalkUp);
                //animator.speed = 0.35f;
            //}
            //else if (directionY == MoveInputDirection.WalkDown)
            //{
                //animator.SetInteger("Direction", (int)MoveAnimDirection.WalkDown);
                //animator.speed = 0.35f;
            //}

            body.velocity = movement;

            if (directionX == MoveInputDirection.WalkRight && isFacingLeft)
            {
                FlipRight();
            }
            else if (directionX == MoveInputDirection.WalkLeft && !isFacingLeft)
            {
                FlipLeft();
            }

            if (directionX == MoveInputDirection.NoMovement
                && directionY == MoveInputDirection.NoMovement)
            {
                // we aren't moving so make sure we dont animate
                animator.speed = 0.0f;
            }
            else
            {
                animator.speed = 2f;
            }

            // if we are dead do not move anymore
            if (isDead == true)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                animator.speed = 0.0f;
            }
        }

        public void FlipRight()
        {
            isFacingLeft = false;
            //CharacterBody.transform.rotation.z = 180;
            this.transform.Rotate(0, 180, 0);
            //foreach (SpriteRenderer sprite in AllChildSprites)
            //    sprite.flipY = true;
            //float newX = CharacterBody.transform.localPosition.x * -1;
            //CharacterBody.transform.localPosition =
            //    new Vector3(newX,
            //        CharacterBody.transform.localPosition.y,
            //        CharacterBody.transform.localPosition.z);
            //Vector3 eulerAngles = CharacterBody.transform.localEulerAngles;
            //// rotate on y
            //CharacterBody.transform.localEulerAngles =
            //    new Vector3(eulerAngles.x,
            //        (eulerAngles.y == 180 ? 0 : 180),
            //        eulerAngles.z);
        }

        public void FlipLeft()
        {
            isFacingLeft = true;
            this.transform.Rotate(0, 180, 0);
            //foreach (SpriteRenderer sprite in AllChildSprites)
            //    sprite.flipY = false;
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