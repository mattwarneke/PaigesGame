using UnityEngine;

namespace Assets.Code.GUI
{
    public class GuiController : MonoBehaviour
    {
        public GameObject Jojo;
        public GameObject Matt;
        public GameObject Paige;

        public void SetMattFollowJojo()
        {
            Matt.GetComponent<MattScript>().ActivateFollow(Jojo.transform);
        }

        public GameObject JarContainer;
        public void RemoveJar()
        {
            JarContainer.SetActive(false);
        }

        public GameObject BedroomDoor;
        public void RemoveBedroomDoor()
        {
            BedroomDoor.SetActive(false);
        }
    }
}
