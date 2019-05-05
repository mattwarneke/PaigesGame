namespace Assets.Code.Logic
{
    using Assets.Code.GUI;
    using UnityEngine;

    public class GameService
    {
        private static GameService _instance;
        public static GameService Instance()
        {
            if (_instance == null)
                _instance = new GameService();
            return _instance;
        }

        private GameService()
        {
            CollectableCount = 0;
            GuiController = GameObject.Find("GuiController").GetComponent<GuiController>();
        }

        public int CollectableCount { get; private set; }
        public GuiController GuiController { get; private set; }

        public void AddCollectable()
        {
            CollectableCount++;
        }

        public void RemoveCollectable()
        {
            GuiController.JoJoSwipAnimation();
            CollectableCount--;
            if (CollectableCount == 0)
            {
                //GuiController.RemoveJar();
                GuiController.RemoveBedroomDoor();
            }
        }
        
        public void HandleEvent(EventEnum eventTriggered)
        {
            switch (eventTriggered)
            {
                case (EventEnum.MattFollowJoJo):
                    GuiController.SetMattFollowJojo();
                    GuiController.MattSpeak(SpeechRepository.GetMattFollowJoJoSpeech());
                    break;
                case (EventEnum.NearDoor):
                    if (CollectableCount > 1)
                        GuiController.MattSpeak(SpeechRepository.GetNearDoorSpeechNoneCollected());
                    else if (CollectableCount == 1)
                        GuiController.MattSpeak(SpeechRepository.GetNearDoorSpeechOneMore());
                    else
                        return;
                    break;
                case (EventEnum.EnterBedroom):
                    GuiController.MattSpeak(SpeechRepository.GetEnterBedroomSpeech());
                    GuiController.EnterBedroom();
                    break;
                default:
                    return;
            }
        }
    }
}
