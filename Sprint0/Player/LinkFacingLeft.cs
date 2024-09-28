using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Sprint0.Player
{
    internal class LinkFacingLeft : ILinkState
    {
        private Link _link;
        private int frame;
        private int remainingFrames;

        public LinkFacingLeft(Link link)
        {
            _link = link;
            frame = 2;
            remainingFrames = _link.framesPerStep;
        }

        void ILinkState.Draw(SpriteBatch _spriteBatch)
        {
            _link.DrawSprite(_spriteBatch, frame, true);
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
            _link.currentState = new LinkFacingUp(_link);
        }
        public void MoveRight()
        {
            _link.currentState = new LinkFacingRight(_link);
        }
        public void MoveLeft()
        {
            _link._position.X -= _link.speed;
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
    }
}