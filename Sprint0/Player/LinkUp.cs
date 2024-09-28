using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Sprint0.Player
{
    internal class LinkUp : ILinkState
    {
        private Link _link;
        private int frame;
        private int remainingFrames;

        public LinkUp(Link link)
        {
            _link = link;
            frame = 4;
            remainingFrames = _link.framesPerStep;
        }

        void ILinkState.Draw(SpriteBatch _spriteBatch)
        {
            _link.DrawSprite(_spriteBatch, frame, false);
        }
        public void Update()
        {
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

            _link.currentState = new LinkDown(_link);
        }
        public void MoveUp()
        {
            _link._position.Y -= _link.speed;
            if (--remainingFrames <= 0)
            {
                if (frame == 4)
                {
                    frame = 5;
                }
                else if (frame == 5)
                {
                    frame = 4;
                }
                remainingFrames = _link.framesPerStep;
            }
        }
        public void MoveRight()
        {
            _link.currentState = new LinkRight(_link);
        }
        public void MoveLeft()
        {
            _link.currentState = new LinkLeft(_link);
        }
        public void UseSword()
        {
            _link.currentState = new SwordUp(_link);
        }
        public void UseArrow()
        {
            _link.currentState = new ArrowUp(_link);
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