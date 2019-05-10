using Assets.Code.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    public GameObject speechBubbleContainer;
    public Text speechBubbleText;
	// Use this for initialization
	void Start () {
        string repeatedSpeechText = "Jojo, Jojo!" + Environment.NewLine + "Where are you ?";
        SpeechQueue.Enqueue(new Speech(repeatedSpeechText, 2));
        SpeechQueue.Enqueue(new Speech(null, 1));
        SpeechQueue.Enqueue(new Speech(repeatedSpeechText, 2));
        StartSpeechQueue();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    Queue<Speech> SpeechQueue = new Queue<Speech>();
    public void AddToSpeechQueue(List<Speech> speechs)
    {
        foreach (Speech speech in speechs)
            SpeechQueue.Enqueue(speech);

        StartSpeechQueue();
    }

    public void EmptySpeechQueue()
    {
        speakingInProgress = false;
        speechBubbleContainer.SetActive(false);
        SpeechQueue.Clear();
    }

    public void StartSpeechQueue()
    {
        if (!speakingInProgress)
            StartCoroutine(PlaySpeechQueue());
    }

    IEnumerator PlaySpeechQueue()
    {
        Speech speech = SpeechQueue.Dequeue();
        if (speech != null)
        {
            if (!string.IsNullOrEmpty(speech.SpeechText))
            {
                this.speechBubbleContainer.SetActive(true);
                speechBubbleText.text = speech.SpeechText;
            }
            else
            {   // empty speech is a pause.
                this.speechBubbleContainer.SetActive(false);
            }
            speakingInProgress = true;
            yield return new WaitForSeconds(speech.SpeechTimeSeconds);
        }

        if (SpeechQueue.Count > 0)
        {   // play next speech
            StartCoroutine(PlaySpeechQueue());
        }
        else
        {
            this.speechBubbleContainer.SetActive(false);
            speakingInProgress = false;
        }
    }



    bool speakingInProgress = false;
    IEnumerator DisableAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.speechBubbleContainer.SetActive(false);
        speakingInProgress = false;
    }

    public IEnumerator WaitUntilSpeechIsOver()
    {
        yield return new WaitWhile(() => speakingInProgress == true);
    }
}
