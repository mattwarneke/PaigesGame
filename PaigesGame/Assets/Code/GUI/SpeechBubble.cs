using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour {

    public Text speechBubbleText;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Speak(string text, int seconds)
    {
        speechBubbleText.text = text;
        speakingInProgress = true;
        StartCoroutine(DisableAfterSeconds(seconds));
    }

    bool speakingInProgress = false;
    IEnumerator DisableAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.gameObject.SetActive(false);
        speakingInProgress = false;
    }

    public IEnumerator WaitUntilSpeechIsOver()
    {
        yield return new WaitWhile(() => speakingInProgress == true);
    }
}
