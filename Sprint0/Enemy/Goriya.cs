using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Classes;
using Sprint2.Enemy.Projectiles;
using System;
using System.Collections.Generic;

namespace Sprint2.Enemy
{
    public class Goriya : IEnemy
    {
        private Texture2D spriteSheet;
        private Rectangle[] sourceRectangles;
        private Vector2 position;
        private Vector2 initialPosition;
        private int currentFrame;
        private bool movingRight = true;
        private bool movingUp = false;
        private bool movingLeft = false;
        private bool movingDown = false;
        private float movementRange = 100f;

        private int health;
        private bool hasThrownBoomerang = false;
        private bool waitingForBoomerang = false;
        private float boomerangWaitTime = 1.3f;
        private float boomerangTimer = 0f;

        private float timeElapsed;
        private bool isFlipped = false;
        private bool alive;
        private Color currentColor = Color.White;
        private float damageColorTimer = 0f;
        private const float DAMAGE_COLOR_DURATION = 0.5f;
        private Vector2 _scale;

        public SpriteBatch spriteBatch;
        public Texture2D enemyDeath;
        private bool isDying;
        private float deathAnimationTimer = 0f;
        private const float DEATH_ANIMATION_DURATION = 0.5f;
        public SoundEffect deathSound;
        private int immunityDuration = 10;
        private int remainingImmunityFrames = 0;
        private bool isImmune;

        private int currentDeathFrame = 0;
        private float deathFrameTime = 0.1f; // Time each death frame is displayed
        private float deathFrameElapsed = 0f;
        private Rectangle[] deathSourceRectangles = { new Rectangle(0, 0, 15, 15), new Rectangle(16, 0, 15, 15), new Rectangle(32, 0, 15, 15), new Rectangle(48, 0, 15, 15)
        };

        public List<Boomerang> projectiles { get; private set; }



        public Vector2 Position { get => position; set => position = value; }
        public int Width { get; } = 16;
        public int Height { get; } = 16;


        public Goriya(Vector2 startPosition)
        {
            health = 4;
            alive = true;
            position = startPosition;
            initialPosition = startPosition;
            projectiles = new List<Boomerang>();


        }


        public void LoadContent(ContentManager content, string texturePath, GraphicsDevice graphicsdevice, Vector2 scale)
        {
            spriteSheet = content.Load<Texture2D>(texturePath);
            sourceRectangles = SpriteSheetHelper.CreateGoriyaFrames();
            _scale = scale;

            enemyDeath = content.Load<Texture2D>("EnemyDeath");
            deathSound = content.Load<SoundEffect>("OOT_Enemy_Poof1");
        }


        public void Update(GameTime gameTime)
        {
            if (isDying)
            {

                deathFrameElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (deathFrameElapsed >= deathFrameTime)
                {
                    currentDeathFrame++;

                    deathFrameElapsed = 0f;

                    if (currentDeathFrame >= deathSourceRectangles.Length)
                    {
                        isDying = false;
                        position = new Vector2(20000, 20000); // Move off screen
                    }
                }
            }
            else if (alive) { 
                timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                damageColorTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                MoveGoriya(gameTime);
                if (timeElapsed > 0.1f)
                {
                    if (movingRight)
                    {
                        currentFrame = (currentFrame == 2) ? 3 : 2;
                        isFlipped = false;
                        timeElapsed = 0f;
                    }
                    else if (movingUp)
                    {
                        isFlipped = !isFlipped;
                        currentFrame = 1;
                        timeElapsed = 0f;
                    }
                    else if (movingLeft)
                    {
                        currentFrame = (currentFrame == 2) ? 3 : 2;
                        isFlipped = true;
                        timeElapsed = 0f;
                    }
                    else if (movingDown)
                    {
                        isFlipped = !isFlipped;
                        currentFrame = 0;
                        timeElapsed = 0f;
                    }
                    if (damageColorTimer <= 0)
                    {
                        currentColor = Color.White;
                    }
                }
                if (isImmune)
                {
                    remainingImmunityFrames--;

                    if (remainingImmunityFrames <= 0)
                    {
                        isImmune = false;
                    }
                }


                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Update(gameTime);


                    if (projectiles[i].IsReturned())
                    {
                        projectiles.RemoveAt(i);
                        waitingForBoomerang = false;
                        hasThrownBoomerang = false;
                    }
                }
            }
        }

        private void MoveGoriya(GameTime gameTime)
        {
            if (waitingForBoomerang)
            {
                boomerangTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (boomerangTimer >= boomerangWaitTime)
                {
                    waitingForBoomerang = false;
                    boomerangTimer = 0f;
                }
                return;
            }

            if (movingRight)
            {
                position.X += 1f;

                if (position.X >= initialPosition.X + movementRange)
                {
                    ShootBoomerang();
                    if (waitingForBoomerang == false)
                    {
                        movingRight = false;
                        movingUp = true;
                    }
                }

            }
            else if (movingUp)
            {
                position.Y -= 1f;

                if (position.Y <= initialPosition.Y - movementRange)
                    movingUp = false;
                movingLeft = true;
            }
            else if (movingLeft)
            {
                position.X -= 1f;
                if (position.X <= initialPosition.X - movementRange)
                    movingLeft = false;
                movingDown = true;
            }
            else if (movingDown)
            {
                position.Y += 1f;

                if (position.Y >= initialPosition.Y + movementRange)
                    movingRight = true;
            }


        }



        private void ShootBoomerang()
        {
            if (!hasThrownBoomerang)
            {
                Vector2 projectilePosition = new Vector2(position.X, position.Y);
                projectiles.Add(new Boomerang(spriteSheet, projectilePosition, new Vector2(200, 0), SpriteSheetHelper.CreateBoomerangFrames(), 5.0f));
                hasThrownBoomerang = true;
                waitingForBoomerang = true;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (isDying)
            {
                spriteBatch.Draw(enemyDeath, position, deathSourceRectangles[currentDeathFrame], Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
            }
            else if (alive)
            {
                SpriteEffects spriteEffect = isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                spriteBatch.Draw(spriteSheet, position, sourceRectangles[currentFrame], currentColor, 0f, Vector2.Zero, _scale, spriteEffect, 0f);
                foreach (var projectile in projectiles)
                {
                    projectile.Draw(spriteBatch);
                }
            }

            //SpriteEffects spriteEffect = isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            //spriteBatch.Draw(spriteSheet, position, sourceRectangles[currentFrame], currentColor, 0f, Vector2.Zero, _scale, spriteEffect, 0f);
            //foreach (var projectile in projectiles)
            //{
            //    projectile.Draw(spriteBatch);
            //}
        }


        public void TakeDamage()
        {
            currentColor = Color.Red;
            damageColorTimer = DAMAGE_COLOR_DURATION;
            if (!isImmune)
            {
                health = Math.Max(0, health - 1);
                remainingImmunityFrames = immunityDuration;
                isImmune = true;
            }

            if (health <= 0 && alive)
            {
                alive = false;
                isDying = true;
                deathAnimationTimer = DEATH_ANIMATION_DURATION;
                deathSound.Play();
            }

            //if (health <= 0)
            //{
            //    alive = false;
            //    position.X = 20000;
            //    position.Y = 20000;
            //}
        }



        public void Reset()
        {
            position = initialPosition;
            movingRight = true;
            currentFrame = 0;
            hasThrownBoomerang = false;
            waitingForBoomerang = false;
            boomerangTimer = 0f;
            damageColorTimer = 0f;
            currentColor = Color.White;
            projectiles.Clear();  
        }

        public Boolean GetState()
        {
            return alive;
        }

    }
}

