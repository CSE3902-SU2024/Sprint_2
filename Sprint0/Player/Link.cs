using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using static System.Net.Mime.MediaTypeNames;


namespace Sprint0.Player
{
    public class Link
    {

        public ILinkState currentState;
        public Rectangle[] _sourceRectangles;
        public Vector2 _position;
        public Vector2 _previousPosition;
        public Texture2D _texture;
        public Vector2 _scale;
        public Color _color;
        public float speed;
        public float boomerangSpeed;
        public int framesPerStep;
        public int framesPerSword;
        public int framesPerDamage;
        public int framesPerBoomerang;
        public int RemainingDamagedFrames;
        public Boolean Damaged;
        private int immunityDuration;
        private int remainingImmunityFrames;
        private bool isImmune;


        private SpriteEffects spriteEffects;

        //for hud:
        public int Health { get; set; } = 6; // each heart = 2 hp

        public SoundEffect SwordAttackSound { get; private set; }
        public SoundEffect bowAttackSound { get; private set; }
        public SoundEffect bombExplosion { get; private set; }
        public SoundEffect BoomerangSound { get; private set; }



        public Link(Rectangle[] sourceRectangles, Texture2D texture, GraphicsDevice graphicsDevice, Vector2 scale, SoundEffect swordSound, SoundEffect bowSound, SoundEffect bombSound, SoundEffect boomerangSound)
        {
            currentState = new LinkDown(this);
            _sourceRectangles = sourceRectangles;
            _position = new Vector2(500.0f, 500.0f);
            _scale = scale;
            _texture = texture;
            speed = 2.0f;
            boomerangSpeed = 10.0f;
            spriteEffects = SpriteEffects.None;
            framesPerStep = 8;
            framesPerSword = 4;
            framesPerDamage = 50;
            framesPerBoomerang = 3;
            RemainingDamagedFrames = framesPerDamage;
            Damaged = false;
            _color = Color.White;
            _previousPosition = new Vector2(200.0f, 200.0f);
            immunityDuration = 50;
            remainingImmunityFrames = 0;


            SwordAttackSound = swordSound;
            bowAttackSound = bowSound;
            bombExplosion = bombSound;
            BoomerangSound = boomerangSound;
        }

        public void MoveDown()
        {
            currentState.MoveDown();
        }

        public void MoveUp()
        {
            currentState.MoveUp();
        }

        public void MoveLeft()
        {
            currentState.MoveLeft();
        }

        public void MoveRight()
        {
            currentState.MoveRight();
        }

        public void SwordAttack()
        {
            currentState.UseSword();
        }

        public void ArrowAttack()
        {
            currentState.UseArrow();
        }
        public void UseBoomerang()
        {
            currentState.UseBoomerang();
        }
        public void UseBomb()
        {
            currentState.UseBomb();
        }
        public void TakeDamage()
        {
            currentState.IsDamaged();

            if (!isImmune) 
            {
                Health = Math.Max(0, Health - 1);
                remainingImmunityFrames = immunityDuration;
                isImmune = true; 
            }

        }
        public void Update()
        {
            currentState.Update();

            if (isImmune)
            {
                remainingImmunityFrames--;

                if (remainingImmunityFrames <= 0)
                {
                    isImmune = false;
                }
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            currentState.Draw(_spriteBatch);
        }

        public void DrawSprite(SpriteBatch _spriteBatch, int frame, Boolean flipped)
        {
            if (flipped)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                spriteEffects = SpriteEffects.None;
            }

            if (Damaged)
            {
                _color = Color.Red;
            }
            else
            {
                _color = Color.White;
            }
            _spriteBatch.Draw(_texture, _position, _sourceRectangles[frame], _color, 0f, Vector2.Zero, _scale, spriteEffects, 0f);
        }

        public void DrawWeapon(SpriteBatch _spriteBatch, int frame, Boolean flippedHorizontal, Boolean flippedVertical, Vector2 _weaponPosition)
        {
            if (flippedHorizontal)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else if (flippedVertical)
            {
                spriteEffects = SpriteEffects.FlipVertically;
            }
            else
            {
                spriteEffects = SpriteEffects.None;
            }
            _spriteBatch.Draw(_texture, _weaponPosition, _sourceRectangles[frame], Color.White, 0f, Vector2.Zero, _scale, spriteEffects, 0f);
        }

    }
}
