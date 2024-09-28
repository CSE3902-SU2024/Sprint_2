using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Sprint0.Player
{
    internal class LinkFacingDown : ILinkState
    {
        private Link _link;
        private int frame;
        private int remainingFrames;

        public LinkFacingDown(Link link)
        {
            _link = link;
            frame = 0;
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

            _link._position.Y += _link.speed;
            if (--remainingFrames <= 0)
            {
                if (frame == 0)
                {
                    frame = 1;
                }
                else if (frame == 1)
                {
                    frame = 0;
                }
                remainingFrames = _link.framesPerStep;
            }
        }
        public void MoveUp()
        {
            _link.currentState = new LinkFacingUp(_link);
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
