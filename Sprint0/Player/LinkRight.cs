using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Sprint0.Player
{
    internal class LinkRight : ILinkState
    {
        private Link _link;
        private int frame;
        private int remainingFrames;

        public LinkRight(Link link)
        {
            _link = link;
            frame = 2;
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
            _link.currentState = new LinkUp(_link);
        }
        public void MoveRight()
        {
            _link._position.X += _link.speed;
            if (--remainingFrames <= 0)
            {
                if (frame == 2)
                {
                    frame = 3;
                }
                else if (frame == 3)
                {
                    frame = 2;
                }
                remainingFrames = _link.framesPerStep;
            }
        }
        public void MoveLeft()
        {
          _link.currentState = new LinkLeft(_link);
            
        }

        public void UseSword()
        {
            _link.currentState = new SwordRight(_link);
        }

        public void IsDamaged()
        {
            _link.Damaged = true;
        }
    }
}