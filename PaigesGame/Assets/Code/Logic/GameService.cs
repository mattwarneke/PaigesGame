using Assets.Code.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Logic
{
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
                // do something.. free the jar.
            }
        }
    }
}
