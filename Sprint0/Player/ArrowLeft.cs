﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Sprint0.Player
{
    internal class ArrowLeft : ILinkState
    {
        private Link _link;
        private int linkFrame;
        private int weaponFrame;
        private int remainingFrames;
        public Vector2 _weaponPosition;
        private bool _arrowFlying;
        private float _arrowSpeed;

        public ArrowLeft(Link link)
        {
            _link = link;
            linkFrame = 9;
            weaponFrame = 19;
            remainingFrames = _link.framesPerSword;
            _weaponPosition.X = _link._position.X - 13 * _link._scale.X;
            _weaponPosition.Y = _link._position.Y + 6 * _link._scale.Y;
            _arrowFlying = false;
            _arrowSpeed = 10f;
        }

        void ILinkState.Draw(SpriteBatch _spriteBatch)
        {
            _link.DrawSprite(_spriteBatch, linkFrame, true);

            _link.DrawWeapon(_spriteBatch, weaponFrame, true, false, _weaponPosition);


        }
        public void Update()
        {

            if (weaponFrame == 19)
            {
                _arrowFlying = true;
            }
            if (_arrowFlying)
            {
                _weaponPosition.X -= _arrowSpeed;
                if (_weaponPosition.X < -100)
                {
                    _arrowFlying = false;
                    linkFrame = 9;
                    _link.currentState = new LinkLeft(_link);
                }
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