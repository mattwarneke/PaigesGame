using Assets.Code.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MattScript : MonoBehaviour
{
    private Animator animator;
    public SpeechBubble speechBubble;
    public bool IsFollowing { get; private set; }
    public Transform transformFollowing;
	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
    }

    Queue<Vector3> followingPositions = new Queue<Vector3>();
    Vector3 currentTarget;
    float minDistance = 1.5f;
    void Update ()
    {
        if (!IsFollowing || this.transformFollowing == null)
            return;
        
        bool isMoreThanMinDistanceFromFollowing =
            Math.Abs(this.transform.position.x - this.transformFollowing.position.x) > minDistance
            || Math.Abs(this.transform.position.y - this.transformFollowing.position.y) > minDistance;
        if (isMoreThanMinDistanceFromFollowing)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, this.transformFollowing.position, Time.deltaTime*2);
        }

        // does not work well with rotation etc but for now it's kinda cool.
        //this.transform.LookAt(this.transformFollowing);
	}
    
    //TODO:
    //public void FreeFromJar()
    //{
    //    speechBubble.Speak("Well done Jojo! I didn't think you would listen", 2);
    //    StartCoroutine(speechBubble.WaitUntilSpeechIsOver());
    //}

    public void ActivateFollow(Transform transformToFollow)
    {
        FollowTransform(transformToFollow);
        speechBubble.EmptySpeechQueue();
        animator.SetBool("IsWalking", true);
    }

    private void FollowTransform(Transform transformToFollow)
    {
        this.IsFollowing = true;
        this.transformFollowing = transformToFollow;
        this.currentTarget = transformToFollow.position;
    }
    
    public void Speak(List<Speech> speech)
    {
        speechBubble.AddToSpeechQueue(speech);
    }

    public void ShowRing()
    {
        animator.SetTrigger("Ring");
    }
}
