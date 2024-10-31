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
        private float movementRange = 100f;
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

        public Vector2 Position { get => position; set => position = value; }
        public int Width { get; } = 16;
        public int Height { get; } = 16;



        public Stalfos(Vector2 startPosition)
        {
            position = startPosition;
            initialPosition = startPosition;
            alive = true;
            random = new Random();
            SetRandomDirection();

            //this.spriteBatch = spriteBatch;
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
            healthCount = 20;
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
                deathAnimationTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (deathAnimationTimer <= 0)
                {
                    
                    isDying = false;
                    alive = false;
                    position = new Vector2(20000, 20000); 
                }
            }
            else if (alive)
            {   
                randCount++;
                timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                damageColorTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(randCount > 60 * random.Next(1, 3))
                {
                    SetRandomDirection();
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
            }
        }

        private void MoveStalfos()
        {
            if (alive)
            {
                switch (currentDirection)
                {
                    case (Direction.Left):
                        if (position.X - speed.X > 32 * _scale.X)
                        {
                            position.X -= speed.X;
                        }
                        else
                        {
                            currentDirection = Direction.Right;
                        }
                        break;
                    case (Direction.Right):
                        if (position.X + speed.X < 208 * _scale.X)
                        {
                            position.X += speed.X;
                        }
                        else
                        {
                            currentDirection = Direction.Left;
                        }
                        break;
                    case (Direction.Up):
                        if (position.Y - speed.Y > 87 * _scale.Y)
                        {
                            position.Y -= speed.Y;
                        }
                        else
                        {
                            currentDirection = Direction.Down;
                        }
                        break;
                    case (Direction.Down):
                        if (position.Y + speed.Y < 143 * _scale.Y)
                        {
                            position.Y += speed.Y;
                        }
                        else
                        {
                            currentDirection = Direction.Up;
                        }

                        break;
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isDying)
            {
                Rectangle deathSourceRectangle1 = new Rectangle(0, 0, 15, 15);
                Rectangle deathSourceRectangle2 = new Rectangle(16, 0, 15, 15);
                Rectangle deathSourceRectangle3 = new Rectangle(32, 0, 15, 15);
                Rectangle deathSourceRectangle4 = new Rectangle(48, 0, 15, 15);
                spriteBatch.Draw(enemyDeath, position, deathSourceRectangle1, Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
                for (int i = 0; i < 4; i++)
                {

                }
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
            healthCount -= 1;


            if (healthCount <= 0 && alive)
            {
                alive = false;
                isDying = true;
                deathAnimationTimer = DEATH_ANIMATION_DURATION;
                deathSound.Play();
            }
        }

        //public void Death()
        //{
        //    Rectangle sourceRectangle = new Rectangle(0, 0, 245, 225);
        //    Vector2 position = new Vector2(0, 0);
        //    //Vector2 scale = new Vector2(3.26f, 2.15f);

        //    spriteBatch.Draw(enemyDeath, position, sourceRectangle, Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
        //}


        public void Reset()
        {
            position = initialPosition;
            movingRight = true;
            currentFrame = 0;
            timeElapsed = 0f;
            damageColorTimer = 0f;
            currentColor = Color.White;
        }

        private void SetRandomDirection()
        {
            currentDirection = (Direction)random.Next(0, 4);

        }
    }
}

