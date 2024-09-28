using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Sprint0.Player
{
    internal class LinkFacingUp : ILinkState
    {
        private Link _link;
        private int frame;
        private int remainingFrames;

        public LinkFacingUp(Link link)
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
        }
        public void MoveDown()
        {

            _link.currentState = new LinkFacingDown(_link);
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
            _link.currentState = new LinkFacingRight(_link);
        }
        public void MoveLeft()
        {
            _link.currentState = new LinkFacingLeft(_link);
        }

      
    }
}