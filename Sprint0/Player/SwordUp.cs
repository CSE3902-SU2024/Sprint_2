﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Sprint0.Player
{
    internal class SwordUp : ILinkState
    {
        private Link _link;
        private int linkFrame;
        private int weaponFrame;
        private int remainingFrames;
        private Vector2 _weaponPosition;


        public SwordUp(Link link)
        {
            _link = link;
            linkFrame = 11;
            weaponFrame = 11;
            remainingFrames = _link.framesPerSword;
            _weaponPosition.X = _link._position.X + 3 * _link._scale.X;
            _weaponPosition.Y = _link._position.Y - 13 * _link._scale.Y;
        }

        void ILinkState.Draw(SpriteBatch _spriteBatch)
        {
            _link.DrawSprite(_spriteBatch, linkFrame, false);
           if (weaponFrame == 12)
            {
                _link.DrawWeapon(_spriteBatch, weaponFrame, false, false, _weaponPosition);
            }
            else if (weaponFrame == 13)
            {
                _weaponPosition.Y = _link._position.Y - 11 * _link._scale.Y;
                _link.DrawWeapon(_spriteBatch, weaponFrame, false, false, _weaponPosition);
            }
            else if (weaponFrame == 14)
            {
                _weaponPosition.Y = _link._position.Y - 5 * _link._scale.Y;
                _link.DrawWeapon(_spriteBatch, weaponFrame, false, false, _weaponPosition);
            }
        }
        public void Update()
        {
            if (--remainingFrames <= 0)
            {
                //Sound effect
                _link.SwordAttackSound.Play();
                if (weaponFrame == 11)
                {
                    weaponFrame = 12;
                }
                else if (weaponFrame == 12)
                {
                    weaponFrame = 13;
                }
                else if (weaponFrame == 13)
                {
                    weaponFrame = 14;
                }
                else if (weaponFrame == 14)
                {
                    linkFrame = 4;
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