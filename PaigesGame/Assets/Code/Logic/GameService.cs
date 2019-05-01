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
            CollectableCount--;
            if (CollectableCount == 0)
            {
                //GuiController.RemoveJar();
                //GuiController.RemoveBedroomDoor();
            }
        }
        
        // should check out bevwizz event handling and see if can reproduce that, unity might be abke to choose the event class....
        // should not be a string.. enum is better but w.e for now..
        public void HandleEvent(string eventString)
        {
            switch (eventString)
            {
                case ("MattFreeFromJar"):
                    FreeMattFromJar();
                    break;
                case ("MattFollowJoJo"):
                    GuiController.SetMattFollowJojo();
                    break;
                default:
                    return;
            }
        }

        public void FreeMattFromJar()
        {
            GuiController.RemoveJar();
        }
    }
}
