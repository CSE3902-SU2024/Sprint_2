﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Sprint0.Player
{
    internal class BombLeft : ILinkState
    {
        private Link _link;
        private int linkFrame;
        private int weaponFrame;
        private int remainingFrames;
        public Vector2 _weaponPosition;
        private bool _Explode;
        private float _BombSpeed;
        public int _boomTimer;


        public BombLeft(Link link)
        {
            _link = link;
            linkFrame = 9;
            weaponFrame = 23;
            remainingFrames = _link.framesPerSword;
            _weaponPosition.X = _link._position.X - 5 * _link._scale.X;
            _weaponPosition.Y = _link._position.Y + 6 * _link._scale.Y;
            _Explode = false;
            _BombSpeed = 5f;
            _boomTimer = 30;
        }

        void ILinkState.Draw(SpriteBatch _spriteBatch)
        {
            _link.DrawSprite(_spriteBatch, linkFrame, true);

            _link.DrawWeapon(_spriteBatch, weaponFrame, true, false, _weaponPosition);


        }
        public void Update()
        {
            _boomTimer--;  //timer ticking down

            if (!_Explode)
            {
                _weaponPosition.X -= _BombSpeed;
                if (_boomTimer == 0)
                {
                    _Explode = true;
                    weaponFrame = 23;  // Start explosion animation
                    remainingFrames = _link.framesPerSword;
                }
            }
            else
            {
                if (--remainingFrames <= 0)
                {

                    if (weaponFrame == 23)
                    {
                        weaponFrame = 24;
                    }
                    else if (weaponFrame == 24)
                    {
                        weaponFrame = 25;
                    }
                    else if (weaponFrame == 25)
                    {
                        weaponFrame = 26;
                    }
                    else if (weaponFrame == 26)
                    {
                        linkFrame = 2;
                        _link.currentState = new LinkLeft(_link);
                    }
                    remainingFrames = _link.framesPerSword;
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