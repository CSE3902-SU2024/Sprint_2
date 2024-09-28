using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Sprint0.Player
{
    internal class SwordDown : ILinkState
    {
        private Link _link;
        private int frame;
        private int remainingFrames;

        public SwordDown(Link link)
        {
            _link = link;
            frame = 16;
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
                if (frame == 16)
                {
                    frame = 17;
                }
                else if (frame == 17)
                {
                    frame = 18;
                }
                else if (frame == 18)
                {
                    frame = 19;
                }
                else if (frame == 19)
                {
                    frame = 0;
                    _link.currentState = new LinkDown(_link);
                }
                remainingFrames = _link.framesPerSword; 
            }

            if (_link.Damaged)
            {
                if (--_link.RemainingDamagedFrames <= 0)
                {
                    _link.Damaged = false;
                    _link.RemainingDamagedFrames = _link.framesPerDamage;
                }
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

        public void UseArrow()
        {

        }
        public void UseBoomerang()
        {

        }
        public void UseBomb()
        {

        }
        public void IsDamaged()
        {
            _link.Damaged = true;
        }


    }
}