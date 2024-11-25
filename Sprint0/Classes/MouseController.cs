﻿using Microsoft.Xna.Framework.Input;
using Sprint0.Player;
using Sprint2.Map;

namespace Sprint0.Classes
{
    public class MouseController
    {
        private MouseState previousState;
        public Link _link;
        private StageManager _StageManager;
        public MouseController(Link link, StageManager stageManager)
        {
            _link = link;
            _StageManager = stageManager;
        }

        public void Update()
        {
            MouseState currentState = Mouse.GetState();

            if (currentState.LeftButton == ButtonState.Pressed && previousState.LeftButton == ButtonState.Released)
            {
                _StageManager.NextStage();
            }

            previousState = currentState;
        }
    }

}
