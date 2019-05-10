using Assets.Code.Logic;
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
        
        void Start()
        {
            PlayerJojo = Jojo.GetComponent<PlayerJojo>();
            MattScript = Matt.GetComponent<MattScript>();
        }

        public void SetMattFollowJojo()
        {
            MattScript.ActivateFollow(Jojo.transform);
        }

        public void MattSpeak(List<Speech> speech)
        {
            MattScript.Speak(speech);
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

        public void ShowRingAnimation()
        {
            MattScript.ShowRing();
        }

        public GameObject JarContainer;
        public void RemoveJar()
        {
            JarContainer.SetActive(false);
        }
    }
}
