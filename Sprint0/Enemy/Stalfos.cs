using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Classes;
using System;
using Microsoft.Xna.Framework.Audio;

namespace Sprint2.Enemy
{
    public class Stalfos : IEnemy
    {
        private Texture2D spriteSheet;
        private Rectangle[] sourceRectangles;
        private Vector2 position;
        private Vector2 initialPosition;
        private int currentFrame;
        private bool movingRight = true;
        private float movementRange = 500f;
        private float distanceMovedInDirection = 0f;
        private float timePerFrame = 0.1f;
        private float timeElapsed;
        private Color currentColor = Color.White;
        private float damageColorTimer = 0f;
        private const float DAMAGE_COLOR_DURATION = 0.5f;
        private int healthCount;
        private bool isFliped = false;

        private Vector2 speed;
        private Vector2 _scale;
        private Random random;
        private Boolean alive;
        private Direction currentDirection;
        private int randCount;

        public SpriteBatch spriteBatch;
        public Texture2D enemyDeath;
        private bool isDying;
        private float deathAnimationTimer = 0f;
        private const float DEATH_ANIMATION_DURATION = 0.5f;
        public SoundEffect deathSound;
        private int immunityDuration = 25;
        private int remainingImmunityFrames = 0;
        private bool isImmune;

        private int currentDeathFrame = 0;
        private float deathFrameTime = 0.1f; // Time each death frame is displayed
        private float deathFrameElapsed = 0f;
        private Rectangle[] deathSourceRectangles = { new Rectangle(0, 0, 15, 15), new Rectangle(16, 0, 15, 15), new Rectangle(32, 0, 15, 15), new Rectangle(48, 0, 15, 15)
        };

        public Vector2 Position { get => position; set => position = value; }
        public int Width { get; } = 16;
        public int Height { get; } = 16;



        public Stalfos(Vector2 startPosition)
        {
            position = startPosition;
            initialPosition = startPosition;
            alive = true;
            random = new Random();
            SetNewRandomDirection();
           
        }
        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

        public void LoadContent(ContentManager content, string texturePath, GraphicsDevice graphicsdevice, Vector2 scale)
        {
            healthCount = 3;
            spriteSheet = content.Load<Texture2D>(texturePath);
            sourceRectangles = SpriteSheetHelper.CreateStalfosFrames();
            _scale = scale;
            speed = Vector2.One;

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
            else if (alive)
            {   
                randCount++;
                timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                damageColorTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(randCount > 60 * random.Next(1, 3))
                {
                    SetNewRandomDirection();
                    randCount = 0;
                }
                MoveStalfos();

                if (timeElapsed > 0.1f)
                {
                    isFliped = !isFliped;
                    timeElapsed = 0f;
                }
                // Reset color after damage timer expires
                if (damageColorTimer <= 0)
                {
                    currentColor = Color.White;
                }

                if (isImmune)
                {
                    remainingImmunityFrames--;

                    if (remainingImmunityFrames <= 0)
                    {
                        isImmune = false;
                    }
                }
            }
        }

        private void MoveStalfos()
        {
            if (alive)
            {
                float moveDistance = speed.X;
                switch (currentDirection)
                {

                    case Direction.Left:
                        if (position.X - moveDistance > 32 * _scale.X)
                        {
                            position.X -= moveDistance;
                        }
                        break;
                    case Direction.Right:
                        if (position.X + moveDistance < 208 * _scale.X)
                        {
                            position.X += moveDistance;
                        }
                        break;
                    case Direction.Up:
                        if (position.Y - moveDistance > 87 * _scale.Y)
                        {
                            position.Y -= moveDistance;
                        }
                        break;
                    case Direction.Down:
                        if (position.Y + moveDistance < 143 * _scale.Y)
                        {
                            position.Y += moveDistance;
                        }

                        break;
                }
                distanceMovedInDirection += moveDistance;
                if (distanceMovedInDirection >= movementRange)
                {
                    SetNewRandomDirection();
                    distanceMovedInDirection = 0f;  
                }

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
                SpriteEffects spriteEffect = isFliped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                spriteBatch.Draw(spriteSheet, position, sourceRectangles[currentFrame], currentColor, 0f, Vector2.Zero, _scale, spriteEffect, 0f);
            }
        }

       
        public void TakeDamage()
        {
            currentColor = Color.Red;
            damageColorTimer = DAMAGE_COLOR_DURATION;

            if (!isImmune)
            {
                healthCount = Math.Max(0, healthCount - 1);
                remainingImmunityFrames = immunityDuration;
                isImmune = true;
            }


            if (healthCount <= 0 && alive)
            {
                alive = false;
                isDying = true;
                deathAnimationTimer = DEATH_ANIMATION_DURATION;
                deathSound.Play();
            }
        }


        public void Reset()
        {
            position = initialPosition;
            movingRight = true;
            currentFrame = 0;
            timeElapsed = 0f;
            damageColorTimer = 0f;
            currentColor = Color.White;
        }

        public void SetNewRandomDirection()
        {
            Direction newDirection;
            do
            {
                newDirection = (Direction)random.Next(0, 4); 
            } while (newDirection == currentDirection); 

            currentDirection = newDirection;
        }


        public Boolean GetState()
        {
            return alive;
        }

    }
}

