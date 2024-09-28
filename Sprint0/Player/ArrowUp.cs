using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Sprint0.Player
{
    public class ArrowUp : ILinkState
    {
        private Link _link;
        private int frame;
        private int remainingFrames;

        public ArrowUp(Link link)
        {
            _link = link;
            frame = 18;
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
                if (frame == 12)
                {
                    frame = 13;
                }
                else if (frame == 13)
                {
                    frame = 14;
                }
                else if (frame == 14)
                {
                    frame = 15;
                }
                else if (frame == 15)
                {
                    frame = 4;
                    _link.currentState = new LinkUp(_link);
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
        public void IsDamaged()
        {
        }

    }
}