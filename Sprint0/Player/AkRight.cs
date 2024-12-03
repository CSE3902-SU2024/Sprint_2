using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Classes;
using System;
using System.Collections.Generic;


namespace Sprint0.Player
{
    internal class AkRight : ILinkState
    {
        private Link _link;
        private int linkFrame;
        private int weaponFrame;
        private int remainingFrames;
        public Vector2 _weaponPosition;
        private bool _arrowFlying;
        private float _arrowSpeed;
        private int arrowTimes = 0;
        private int _currentFrame;
        private int _totalFrames;

        private BulletManager _bulletManager;

        private bool isShooting;
        private float animationTimer;
        private const float FIRE_RATE = 0.15f; // Time in seconds between shots
        private float _timeSinceLastShot = 0f;
        private Random _random = new Random();
        private float overheatTimer;
        private Boolean overheating = false;
        private float overheatMult;
        Vector2 bulletStartPosition;



        private Texture2D _bulletTexture;

        public AkRight(Link link)//, BulletManager bulletManager)
        {
            _link = link;
            _weaponPosition.X = _link._position.X + 13 * _link._scale.X;
            _weaponPosition.Y = _link._position.Y + 6 * _link._scale.Y;

            linkFrame = 27;
            _totalFrames = 3;  //3 ak animation frames

          
            LoadBulletTexture();

        }

        void ILinkState.Draw(SpriteBatch _spriteBatch)
        {
            _link.DrawSprite(_spriteBatch, linkFrame, false);

            _bulletManager.Draw(_spriteBatch);

        }
        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            overheatTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (animationTimer >= FIRE_RATE)
            {
                // Cycle through animation frames
                if (linkFrame == 27) linkFrame = 28;
                else if (linkFrame == 28) linkFrame = 29;
                else linkFrame = 27;

                animationTimer = 0f; // Reset the timer
            }
            if (overheatTimer >= 1f) //overheats after 1 seconds
            {
                overheating = true;
            }

            _bulletManager.Update(gameTime);

            if (keyboardState.IsKeyDown(Keys.D4)) // D4 corresponds to the "4" key on the keyboard
            {
                _timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (isShooting && _timeSinceLastShot >= FIRE_RATE)
                {
                    Fire(new Vector2(1, 0)); // Fire bullets to the right
                    _link.bowAttackSound.Play();
                    _timeSinceLastShot = 0f; // Reset the timer
                }
            }
            else
            {

                _timeSinceLastShot = FIRE_RATE;
                _link.currentState = new LinkRight(_link);

            }

            _bulletManager.Update(gameTime);    


            if (_link.Damaged)
            {
                if (--_link.RemainingDamagedFrames <= 0)
                {
                    _link.Damaged = false;
                    _link.RemainingDamagedFrames = _link.framesPerDamage;
                }
            }

            
        }
        public void LoadBulletTexture()
        {
            Texture2D bulletTexture = _link._texture;
            Rectangle bulletSourceRectangle = _link._sourceRectangles[31];

            // Initialize BulletManager with the existing texture and rectangle
            _bulletManager = new BulletManager(
                bulletTexture,
                bulletSourceRectangle,
                _link._scale,
                2f,   // Bullet speed
                3f     // Bullet lifetime in seconds
            );

        }
        public void Fire(Vector2 direction)
        {
            Console.WriteLine("Bullet fired!");

            bulletStartPosition.X = _link._position.X + 30 * _link._scale.X;
            bulletStartPosition.Y = _link._position.Y + 6 * _link._scale.Y;

            bulletStartPosition = RandomizeBullet(bulletStartPosition);
            _bulletManager.SpawnBullet(bulletStartPosition, direction);
        }
        private Vector2 RandomizeBullet(Vector2 basePosition)
        {
            if (overheating)
            {
                overheatMult = (90f-70f); //between 20 and -70
                basePosition.Y = _link._position.Y +4 * _link._scale.Y;
            }
            else {
                overheatMult= (25f - 20f); //between 5 and -20
            }

            float offset = (float)_random.NextDouble() * overheatMult;
            basePosition.Y += offset;
            return basePosition;
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

        public void UseAk()
        {
            isShooting = true; // Enable shooting mode

        }
    }
}