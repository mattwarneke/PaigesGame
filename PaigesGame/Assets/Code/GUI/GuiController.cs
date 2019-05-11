using Assets.Code.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.GUI
{
    public class GuiController : MonoBehaviour
    {
        public GameObject Jojo;
        public GameObject Matt;
        public GameObject Paige;

        public PlayerJojo PlayerJojo;
        public MattScript MattScript;
        public PaigeScript PaigeScript;

        public GameObject DialogCanvasGO;
        
        void Start()
        {
            PlayerJojo = Jojo.GetComponent<PlayerJojo>();
            MattScript = Matt.GetComponent<MattScript>();
            PaigeScript = Paige.GetComponent<PaigeScript>();
        }

        public void SetMattFollowJojo()
        {
            MattScript.ActivateFollow(Jojo.transform);
        }

        public void MattSpeak(List<Speech> speech)
        {
            MattScript.Speak(speech);
        }

        public void DoActionAfterXTime(float waitTime, Action callback)
        {
            StartCoroutine(RunCallbackAfterWait(waitTime, callback));
        }
        
        private IEnumerator RunCallbackAfterWait(float waitTime, Action callback)
        {
            yield return new WaitForSeconds(waitTime);
            callback();
        }
        
        public void PauseJojoMovement(float timePaused)
        {
            PlayerJojo.PauseWalking(timePaused);
        }

        public void JoJoSwipAnimation()
        {
            PlayerJojo.PlaySwipAnimation();
        }
        
        public GameObject BedroomDoor;
        public void RemoveBedroomDoor()
        {
            BedroomDoor.SetActive(false);
        }

        public void EnterBedroom()
        {
            PlayerJojo.StartPlayBedroomEnter(JarContainer.transform);
        }

        public void NearJar()
        {
            PlayerJojo.PlaySwipAnimation();
        }

        public void ShowRingAnimation()
        {
            MattScript.ShowRing();
        }

        public void ShowMarryMeCanvasDialog()
        {
            DialogCanvasGO.SetActive(true);
        }

        public void MarryMeYes()
        {
            DialogCanvasGO.SetActive(false);
            GameService.Instance().SheSaidYesFuckYeah();
        }

        public GameObject JarContainer;
        public void RemoveJar()
        {
            JarContainer.SetActive(false);
            PaigeScript.Speak(SpeechRepository.PaigeFreedom());
            // stop paige animating and kiss???
        }
    }
}
