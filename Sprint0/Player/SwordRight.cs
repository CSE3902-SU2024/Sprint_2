﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Sprint0.Player
{
    internal class SwordRight : ILinkState
    {
        private Link _link;
        private int frame;
        private int remainingFrames;

        public SwordRight  (Link link)
        {
            _link = link;
            frame = 8;
            remainingFrames = _link.framesPerSword;
        }

        void ILinkState.Draw(SpriteBatch _spriteBatch)
        {
            _link.DrawSprite(_spriteBatch, frame, false);
        }
        public void Update()
        {
            if (--remainingFrames <= 0)
            {
                if (frame == 8)
                {
                    frame = 9;
                }
                else if (frame == 9)
                {
                    frame = 10;
                }
                else if (frame == 10)
                {
                    frame = 11;
                }
                else if (frame == 11)
                {
                    frame = 2;
                    _link.currentState = new LinkRight(_link);
                }
                remainingFrames = _link.framesPerSword;
            }
        }
        public void MoveDown()
        {
        }
        public void MoveUp()
        {
        }
        public void MoveRight()
        {
        }
        public void MoveLeft()
        {
        }
        public void UseSword()
        {

        }


    }
}