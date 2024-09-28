using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Sprint0.Player
{
    internal class BoomerangRight : ILinkState
    {
        private Link _link;
        private int linkFrame;
        private int weaponFrame;
        private int remainingFrames;
        private int rotations;
        private Vector2 _weaponPosition;

        public BoomerangRight(Link link)
        {
            _link = link;
            linkFrame = 9;
            weaponFrame = 20;
            rotations = 0;
            remainingFrames = _link.framesPerBoomerang;
            _weaponPosition.X = _link._position.X + 13 * _link._scale.X;
            _weaponPosition.Y = _link._position.Y + 6 * _link._scale.Y;
        }

        void ILinkState.Draw(SpriteBatch _spriteBatch)
        {
            _link.DrawSprite(_spriteBatch, linkFrame, false);

            _link.DrawWeapon(_spriteBatch, weaponFrame, false, false, _weaponPosition);

        }
        public void Update()
        {
            if (--remainingFrames <= 0)
            {
                if (weaponFrame == 20)
                {
                    weaponFrame = 21;
                }
                else if (weaponFrame == 21)
                {
                    weaponFrame = 22;
                }
                else if (weaponFrame == 22)
                {
                    weaponFrame = 20;
                    linkFrame = 2;
                    _link.currentState = new LinkRight(_link);
                }
                remainingFrames = _link.framesPerBoomerang;
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