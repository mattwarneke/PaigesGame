namespace Assets.Code.Logic
{
    using Assets.Code.GUI;
    using System;
    using System.Collections;
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
                    GuiController.PauseJojoMovement(4f);
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
                    GuiController.PauseJojoMovement(3f);
                    GuiController.MattSpeak(SpeechRepository.GetEnterBedroomSpeech());
                    GuiController.EnterBedroom();
                    break;
                case (EventEnum.DemonKilled):
                    if (CollectableCount > 0)
                        GuiController.MattSpeak(SpeechRepository.GetDemonDied(CollectableCount));
                    else
                        GuiController.MattSpeak(SpeechRepository.GetNoMoreDemons());
                    break;
                case (EventEnum.NearJarTrigger):
                    GuiController.JoJoSwipAnimation();
                    GuiController.DoActionAfterXTime(0.5f, () =>
                    {
                        GuiController.JoJoSwipAnimation();
                    });
                    GuiController.PauseJojoMovement(4f);
                    GuiController.DoActionAfterXTime(1.5f, () =>
                    {
                        GuiController.MattSpeak(SpeechRepository.GetJoJoBreakJarFailedSpeech());
                        GuiController.DoActionAfterXTime(4, () =>
                        {
                            GuiController.ShowRingAnimation();
                            GuiController.DoActionAfterXTime(4, () =>
                            {
                                GuiController.ShowMarryMeCanvasDialog();
                            });
                            // another chained call paige is free! and Huh is that a ring?
                            // will you marry me paige - speech.
                            // could probably do something more elegant callbacks on animations or something.. but w.e
                        });
                    });
                    break;
                default:
                    return;
            }
        }

        public void SheSaidYesFuckYeah()
        {
            GuiController.RemoveJar();
            GuiController.MattSpeak(SpeechRepository.SheSaidYesFuckYeah());
        }
    }
}
