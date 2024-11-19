﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Sprint0.Player
{
    internal class ArrowDown : ILinkState
    {
        private Link _link;
        private int linkFrame;
        private int weaponFrame;
        private int remainingFrames;
        public Vector2 _weaponPosition;
        private bool _arrowFlying;
        private float _arrowSpeed;
        private int arrowTimes = 0;

        public ArrowDown(Link link)
        {
            _link = link;
            linkFrame = 8;
            weaponFrame = 18;
            remainingFrames = _link.framesPerSword;
            _weaponPosition.X = _link._position.X + 5 * _link._scale.X;
            _weaponPosition.Y = _link._position.Y + 14 * _link._scale.Y;
            _arrowFlying = false;
            _arrowSpeed = 10f;
        }

        void ILinkState.Draw(SpriteBatch _spriteBatch)
        {
            _link.DrawSprite(_spriteBatch, linkFrame, false);

            _link.DrawWeapon(_spriteBatch, weaponFrame, false, true, _weaponPosition);


        }
        public void Update()
        {

            if (weaponFrame == 18)
            {
                _arrowFlying = true;
            }
            if (_arrowFlying)
            {
                if (arrowTimes == 0)
                {
                    //SoundEffect
                    _link.bowAttackSound.Play();
                    arrowTimes = 1;
                }
                _weaponPosition.Y += _arrowSpeed;
                if (_weaponPosition.Y > 1000)
                {
                    _arrowFlying = false;
                    linkFrame = 8;
                    _link.currentState = new LinkDown(_link);
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