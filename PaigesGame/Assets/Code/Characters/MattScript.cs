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

        string repeatedSpeechText = "Jojo, Jojo!" + Environment.NewLine + "Where are you ?";
        List<Speech> startSpeech = new List<Speech>();
        startSpeech.Add(new Speech(repeatedSpeechText, 2));
        startSpeech.Add(new Speech(null, 1));
        startSpeech.Add(new Speech(repeatedSpeechText, 2));
        Speak(startSpeech);
    }

    Queue<Vector3> followingPositions = new Queue<Vector3>();
    Vector3 currentTarget;
    float minDistanceY = 1.5f;
    float minDistanceX = 2f;
    void Update ()
    {
        if (!IsFollowing || this.transformFollowing == null)
            return;
        
        bool isMoreThanMinDistanceFromFollowing =
            Math.Abs(this.transform.position.x - this.transformFollowing.position.x) > minDistanceX
            || Math.Abs(this.transform.position.y - this.transformFollowing.position.y) > minDistanceY;
        if (isMoreThanMinDistanceFromFollowing)
        {
            animator.SetBool("IsWalking", true);
            animator.SetFloat("WalkSpeed", 0.75f);
            this.transform.position = Vector2.Lerp(this.transform.position, this.transformFollowing.position, Time.deltaTime*2);
        }
        else
        {
            animator.SetFloat("WalkSpeed", 0);
            animator.SetBool("IsWalking", false);
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
        animator.SetFloat("WalkSpeed", 0.75f);
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
        animator.SetFloat("WalkSpeed", 0);
        animator.SetBool("IsWalking", false);
        speechBubble.AddToSpeechQueue(speech);
    }

    public void ShowRing()
    {
        animator.SetTrigger("Ring");
    }
}
