using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Sprint0.Player
{
    internal class ArrowRight : ILinkState
    {
        private Link _link;
        private int linkFrame;
        private int ArrowFrame;
        private int remainingFrames;
        private Vector2 _ArrowPosition;
        private List<Arrow> _activeArrows;
        


        public ArrowRight(Link link)
        {
            _link = link;
            linkFrame = 9;
            ArrowFrame = 19;
            remainingFrames = _link.framesPerSword;
            _activeArrows = new List<Arrow>();

            Vector2 arrowPosition = new Vector2(_link._position.X + 13 * _link._scale.X, _link._position.Y + 6 * _link._scale.Y);
            Vector2 arrowDirection = Vector2.UnitX; // Assuming right direction
            float arrowSpeed = 200f; // Adjust as needed
            _activeArrows.Add(new Arrow(arrowPosition, arrowDirection, arrowSpeed,800,400));
        }

        void ILinkState.Draw(SpriteBatch _spriteBatch)
        {
            _link.DrawSprite(_spriteBatch, linkFrame, false);
            
            // Draw all active arrows
            foreach (var arrow in _activeArrows)
            {
                _link.DrawWeapon(_spriteBatch, ArrowFrame, false,false, _ArrowPosition);
            }
        }
        public void Update()
        {
            if (--remainingFrames <= 0)
            {
                
                {
                    linkFrame = 2;
                    _link.currentState = new LinkRight(_link);
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
            _link.Damaged = true;
        }

    }
}